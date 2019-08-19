using Microsoft.JSInterop;
using System;

namespace RPedretti.RazorComponents.Input.SuggestBox
{
    public class SuggestBoxBaseJSInterop
    {
        #region Events

        public event EventHandler ClearSelectionEvent;

        #endregion Events

        #region Methods

        [JSInvokable]
        public void ClearSelection()
        {
            ClearSelectionEvent?.Invoke(this, EventArgs.Empty);
        }

        #endregion Methods
    }
}
