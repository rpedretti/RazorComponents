﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.Sample.Shared.Managers
{
    public sealed class NotificationManager
    {
        #region Events

        public event EventHandler<NotificationEventArgs> ShowNotification;

        #endregion Events

        #region Methods

        public void ShowNotificationMessage(string message, string title)
        {
            ShowNotification?.Invoke(this, new NotificationEventArgs
            {
                Message = message,
                Title = title
            });
        }

        #endregion Methods

        #region Classes

        public class NotificationEventArgs : EventArgs
        {
            #region Properties

            public string Message { get; set; }
            public string Title { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}
