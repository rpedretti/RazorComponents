#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPedretti.RazorComponents.Shared
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Events

        #region Methods

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        protected void RaisePropertyChanged([CallerMemberName]string? propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetParameter<T>(ref T prop, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        protected bool SetParameter<T>(ref T prop, T value, Action OnChanged, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            OnChanged?.Invoke();
            RaisePropertyChanged(propertyName);
            return true;
        }

        #endregion Methods
    }
}

#nullable restore
