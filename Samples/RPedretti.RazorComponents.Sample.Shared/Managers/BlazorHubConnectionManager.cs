using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
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

#if DEBUG
        private readonly string baseUrl = "https://localhost:4001";
#else
        private readonly string baseUrl = "https://blazorsignalr.azurewebsites.net";
#endif
        private HubConnection? connection;
        private readonly HttpClient HttpClient;
        private TokenModel? Jwt;
        private NotificationManager NotificationManager;

        #endregion Fields

        #region Constructors

        public BlazorHubConnectionManager(HttpClient httpClient, NotificationManager notificationManager)
        {
            HttpClient = httpClient;
            NotificationManager = notificationManager;
        }

        #endregion Constructors

        #region Properties

        public bool IsConnected => connection != null;

        #endregion Properties

        #region Methods

        public async Task<bool> CloseConnectionAsync()
        {

            if (IsConnected)
            {
                await connection!.StopAsync();
            }

            connection = null;
            Jwt = null;
            return true;
        }

        public async Task<HubConnection?> ConnectAsync(string username, string password)
        {
            await CloseConnectionAsync();

            HttpResponseMessage response;

            if (Jwt?.IsExpired ?? false)
            {
                Console.WriteLine("token expired");
                var refreshContent = new StringContent(Convert.ToBase64String(Encoding.UTF8.GetBytes(Jwt.RefreshToken)), Encoding.UTF8, "application/json");
                response = await HttpClient.PostAsync($"{baseUrl}/{Jwt.RefreshUrl}", refreshContent);
                if (response.IsSuccessStatusCode)
                {
                    var serializedJwt = await response.Content.ReadAsStringAsync();
                    var jwt = JsonSerializer.Deserialize<SecureJwtModel>(serializedJwt);
                    Jwt = jwt.TokenModel;
                }
            }
            else
            {
                var userModel = new UserAuthenticationModel { Username = username, Password = password };
                var content = new StringContent(JsonSerializer.Serialize(userModel), Encoding.UTF8, "application/json");
                response = await HttpClient.PostAsync($"{baseUrl}/jwt/requestjwt", content);

                if (response.IsSuccessStatusCode)
                {
                    var serializedJwt = await response.Content.ReadAsStringAsync();
                    var jwt = JsonSerializer.Deserialize<SecureJwtModel>(serializedJwt);
                    Jwt = jwt.TokenModel;
                    connection = new HubConnectionBuilder()
                        .WithUrl($"{baseUrl}/blazorhub?access_token={Jwt.Token}", opt =>
                        {
                            opt.SkipNegotiation = true;
                            opt.Transports = HttpTransportType.WebSockets;
                        })
                        .Build();

                    connection.On<string>("GuestEntered", id =>
                    {
                        NotificationManager.ShowNotificationMessage($"User {id} just entered", "New User");
                        return Task.CompletedTask;
                    });

                    connection.On<string>("GuestLeft", id =>
                    {
                        NotificationManager.ShowNotificationMessage($"User {id} just left", "New User");
                        return Task.CompletedTask;
                    });

                    await connection.StartAsync();
                }
            }

            return connection;
        }

        public async void Dispose()
        {
            await CloseConnectionAsync();
        }

        #endregion Methods
    }
}
