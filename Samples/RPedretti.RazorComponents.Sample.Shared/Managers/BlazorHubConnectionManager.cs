using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using RPedretti.RazorComponents.Sample.Shared.Configuration;
using RPedretti.RazorComponents.Sample.Shared.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Managers
{
    public sealed class BlazorHubConnectionManager : IDisposable
    {
        #region Fields

        private readonly string _baseUrl;

        private HubConnection? _connection;
        private TokenModel? _jwt;
        private readonly HttpClient _httpClient;
        private readonly NotificationManager _notificationManager;

        #endregion Fields

        #region Constructors

        public BlazorHubConnectionManager(HttpClient httpClient, NotificationManager notificationManager, HubConfig hubConfig)
        {
            _httpClient = httpClient;
            _notificationManager = notificationManager;
            _baseUrl = hubConfig.Url;
        }

        #endregion Constructors

        #region Properties

        public bool IsConnected => _connection != null;

        #endregion Properties

        #region Methods

        public async Task<bool> CloseConnectionAsync()
        {

            if (IsConnected)
            {
                await _connection!.StopAsync();
            }

            _connection = null;
            _jwt = null;
            return true;
        }

        public async Task<HubConnection?> ConnectAsync(string username, string password)
        {
            await CloseConnectionAsync();

            HttpResponseMessage response;

            if (_jwt?.IsExpired ?? false)
            {
                Console.WriteLine("token expired");
                var refreshContent = new StringContent(Convert.ToBase64String(Encoding.UTF8.GetBytes(_jwt.RefreshToken)), Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync($"{_baseUrl}/{_jwt.RefreshUrl}", refreshContent);
                if (response.IsSuccessStatusCode)
                {
                    var serializedJwt = await response.Content.ReadAsStringAsync();
                    var jwt = JsonSerializer.Deserialize<SecureJwtModel>(serializedJwt);
                    _jwt = jwt.TokenModel;
                }
            }
            else
            {
                var userModel = new UserAuthenticationModel { Username = username, Password = password };
                var content = new StringContent(JsonSerializer.Serialize(userModel), Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync($"{_baseUrl}/jwt/requestjwt", content);

                if (response.IsSuccessStatusCode)
                {
                    var serializedJwt = await response.Content.ReadAsStringAsync();
                    var jwt = JsonSerializer.Deserialize<SecureJwtModel>(serializedJwt);
                    _jwt = jwt.TokenModel;
                    _connection = new HubConnectionBuilder()
                        .WithUrl($"{_baseUrl}/blazorhub?access_token={_jwt.Token}", opt =>
                        {
                            opt.SkipNegotiation = true;
                            opt.Transports = HttpTransportType.WebSockets;
                        })
                        .Build();

                    _connection.On<string>("GuestEntered", id =>
                    {
                        _notificationManager.ShowNotificationMessage($"User {id} just entered", "New User");
                        return Task.CompletedTask;
                    });

                    _connection.On<string>("GuestLeft", id =>
                    {
                        _notificationManager.ShowNotificationMessage($"User {id} just left", "New User");
                        return Task.CompletedTask;
                    });

                    await _connection.StartAsync();
                }
            }

            return _connection;
        }

        public async void Dispose()
        {
            await CloseConnectionAsync();
        }

        #endregion Methods
    }
}
