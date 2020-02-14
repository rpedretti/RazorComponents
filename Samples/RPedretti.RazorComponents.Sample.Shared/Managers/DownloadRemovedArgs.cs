using System;

namespace RPedretti.RazorComponents.Sample.Shared.Managers
{
    public sealed class DownloadRemovedArgs : EventArgs
    {
        #region Properties

        public string DownloadId { get; set; }

        #endregion Properties
    }
}
