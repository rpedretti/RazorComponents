using System;

namespace RPedretti.RazorComponents.BingMap.Entities.InfoBox
{
    public partial class InfoBox
    {
        #region Events

        private event EventHandler<InfoboxEventArgs> _onClick;

        private event EventHandler<InfoboxEventArgs> _onInfoboxChanged;

        private event EventHandler<InfoboxEventArgs> _onMouseOver;

        private event EventHandler<InfoboxEventArgs> _onMouseUp;

        #endregion Events
    }
}
