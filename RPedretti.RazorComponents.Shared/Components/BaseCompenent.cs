using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Shared.Components
{
    public abstract class BaseComponent : ComponentBase
    {
        #region Methods

        protected async Task HandleKeyPress(KeyboardEventArgs args, Func<Task> action)
        {
            if (args.Key == " " || args.Key == "Enter")
            {
                await action?.Invoke();
            }
        }

        public bool SetParameter<T>(ref T prop, T value, Action onChange = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            onChange?.Invoke();

            return true;
        }

        #endregion Methods
    }
}
