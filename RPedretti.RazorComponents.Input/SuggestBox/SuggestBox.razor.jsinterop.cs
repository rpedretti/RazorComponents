using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace RPedretti.RazorComponents.Input.SuggestBox
{
    public class SuggestBoxBaseJSInterop
    {
        public event EventHandler ClearSelectionEvent;

        [JSInvokable]
        public void ClearSelection()
        {
            ClearSelectionEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}