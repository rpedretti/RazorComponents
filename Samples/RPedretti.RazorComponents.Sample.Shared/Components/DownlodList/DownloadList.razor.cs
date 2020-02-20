using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Shared.Domain;
using RPedretti.RazorComponents.Sample.Shared.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Components.DownlodList
{
    public partial class DownloadList : IDisposable
    {
        #region Methods

        private void RemoveDownload(object sender, DownloadRemovedArgs e)
        {
            var download = AvailableDownloads.FirstOrDefault(d => d.Id == e.DownloadId);
            if (download != null)
            {
                AvailableDownloads.Remove(download);
                StateHasChanged();
            }
        }

        private void ShowDownloads(object sender, NewDownloadAvailableArgs e)
        {
            var result = e.DownloadResult;
            result.Description = $"{result.Url} ({result.Id})";
            AvailableDownloads.Add(result);
            StateHasChanged();
        }

        #endregion Methods

        #region Fields

        protected List<DownloadResult> AvailableDownloads = new List<DownloadResult>();

        #endregion Fields

        #region Properties

        [Inject] protected DownloadManager DownloadManager { get; set; }

        protected string FinalPosition => "-" + Position.ToString().ToLower().Replace("_", "-");
        [Parameter] public DownloadListPosition Position { get; set; } = DownloadListPosition.BOTTOM_RIGHT;

        #endregion Properties

        protected override void OnInitialized()
        {
            DownloadManager.NewDownloadAvailable += ShowDownloads;
            DownloadManager.DownloadRemoved += RemoveDownload;
        }

        protected async Task RemoveDownloadFromList(DownloadResult download)
        {
            await DownloadManager.RequestDownloadRemoveAsync(download.Id);
        }

        public void Dispose()
        {
            DownloadManager.NewDownloadAvailable -= ShowDownloads;
        }
    }
}
