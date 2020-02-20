using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Shared.Managers;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.SignalR
{
    public partial class SignalRPage
    {
        #region Properties

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Inject] private DownloadManager _downloadManager { get; set; }
        [Inject] private BlazorHubConnectionManager _hubConnectionManager { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected bool HasToken { get; set; }
        protected string Password { get; set; } = "bla";
        protected string Username { get; set; } = "rafa";

        #endregion Properties

        #region Methods

        protected async Task LoginAsync()
        {
            var connection = await _hubConnectionManager.ConnectAsync(Username, Password);
            HasToken = true;
            _downloadManager.SetConnection(connection);
        }

        protected async Task LogoutAsync()
        {
            await _hubConnectionManager.CloseConnectionAsync();
            HasToken = false;
        }

        protected override void OnInitialized()
        {
            HasToken = _hubConnectionManager.IsConnected;
            base.OnInitialized();
        }

        protected async Task RequestLongProcessAsync()
        {
            await _downloadManager.RequestLongRunningProcess();
        }

        #endregion Methods
    }
}

