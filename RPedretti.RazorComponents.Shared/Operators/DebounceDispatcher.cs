using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace RPedretti.RazorComponents.Shared.Operators
{
    public class DebounceDispatcher
    {
        #region Fields

        private Timer? _timer;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Debounce reset timer and after last item recieved give you last item.
        /// <exception cref="http://demo.nimius.net/debounce_throttle/">See this example for understanding what is RateLimiting and Debounce</exception>
        /// </summary>
        /// <param name="obj">Your object</param>
        /// <param name="interval">Milisecond interval</param>
        /// <param name="debounceAction">Called when last item call this method and after interval was finished</param>
        public void Debounce<T>(int interval, Action<T> action, [AllowNull] T param)
        {
            _timer?.Dispose();
            _timer = new Timer(state =>
            {
                _timer!.Dispose();
                if (_timer != null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    action.Invoke(param);
#pragma warning restore CS8604 // Possible null reference argument.
                }

                _timer = null;
            }, param, interval, interval);
        }

        public void Debounce(int interval, Action action)
        {
            _timer?.Dispose();
            _timer = new Timer(state =>
            {
                _timer!.Dispose();
                if (_timer != null)
                {
                    action.Invoke();
                }

                _timer = null;
            }, null, interval, interval);
        }

        #endregion Methods
    }
}
