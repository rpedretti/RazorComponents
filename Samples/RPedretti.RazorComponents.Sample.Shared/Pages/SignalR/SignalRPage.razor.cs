using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Shared.Managers;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.SignalR
{
    public partial class SignalRPage
    {
        #region Properties

        [Inject] private DownloadManager DownloadManager { get; set; }
        [Inject] private BlazorHubConnectionManager HubConnectionManager { get; set; }

        protected bool HasToken { get; set; }
        protected string Password { get; set; } = "bla";
        protected string Username { get; set; } = "rafa";

        #endregion Properties

        #region Methods

        protected async Task LoginAsync()
        {
            var connection = await HubConnectionManager.ConnectAsync(Username, Password);
            HasToken = true;
            DownloadManager.SetConnection(connection);
        }

        protected async Task LogoutAsync()
        {
            await HubConnectionManager.CloseConnectionAsync();
            HasToken = false;
        }

        protected override void OnInitialized()
        {
            HasToken = HubConnectionManager.IsConnected;
            base.OnInitialized();
        }

        protected async Task RequestLongProcessAsync()
        {
            await DownloadManager.RequestLongRunningProcess();
        }

        #endregion Methods
    }
}

