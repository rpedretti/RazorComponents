using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.BingMap.Entities;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap
{
    public partial class BingMap
    {
        #region Fields

        private const string attachChangeEventFunctionName = _mapNamespace + ".attachChangeEvent";
        private const string attachEventFunctionName = _mapNamespace + ".attachEvent";
        private const string attachThrottleEventFunctionName = _mapNamespace + ".attachThrottleEvent";
        private const string detachEventFunctionName = _mapNamespace + ".detachEvent";
        private Func<MouseEventArgs<BingMap>, Task> _click;
        private Func<MouseEventArgs<BingMap>, Task> _doubleClick;
        private Func<Task> _mapTypeChanged;
        private Func<MouseEventArgs<BingMap>, Task> _mouseDown;
        private Func<MouseEventArgs<BingMap>, Task> _mouseMove;
        private Func<MouseEventArgs<BingMap>, Task> _mouseOut;
        private Func<MouseEventArgs<BingMap>, Task> _mouseOver;
        private Func<MouseEventArgs<BingMap>, Task> _mouseUp;
        private Func<MouseEventArgs<BingMap>, Task> _mouseWheel;
        private Func<MouseEventArgs<BingMap>, Task> _rightClick;
        private Func<Task> _throttleViewChange;
        private Func<Task> _throttleViewChangeEnd;
        private Func<Task> _throttleViewChangeStart;
        private Func<Task> _viewChange;
        private Func<Task> _viewChangeEnd;
        private Func<Task> _viewChangeStart;

        #endregion Fields

        #region Properties

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> Click
        {
            get => _click;
            set
            {
                if (_click == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.Click, nameof(EmitMapEvent));
                }
                else if (_click != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.Click);
                }

                _click = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> DoubleClick
        {
            get => _doubleClick;
            set
            {
                if (_doubleClick == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.DoubleClick, nameof(EmitMapEvent));
                }
                else if (_doubleClick != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.DoubleClick);
                }

                _doubleClick = value;
            }
        }

        [Parameter]
        public Func<Task> MapTypeChanged
        {
            get => _mapTypeChanged;
            set
            {
                if (_mapTypeChanged == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.MapTypeChanged, nameof(EmitMapChangeEvent));
                }
                else if (_mapTypeChanged != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MapTypeChanged);
                }

                _mapTypeChanged = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> MouseDown
        {
            get => _mouseDown;
            set
            {
                if (_mouseDown == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseDown, nameof(EmitMapEvent));
                }
                else if (_mouseDown != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseDown);
                }

                _mouseDown = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> MouseMove
        {
            get => _mouseMove;
            set
            {
                if (_mouseMove == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseMove, nameof(EmitMapEvent));
                }
                else if (_mouseMove != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseMove);
                }

                _mouseMove = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> MouseOut
        {
            get => _mouseOut;
            set
            {
                if (_mouseOut == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseOut, nameof(EmitMapEvent));
                }
                else if (_mouseOut != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseOut);
                }

                _mouseOut = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> MouseOver
        {
            get => _mouseOver;
            set
            {
                if (_mouseOver == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseOver, nameof(EmitMapEvent));
                }
                else if (_mouseOver != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseOver);
                }

                _mouseOver = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> MouseUp
        {
            get => _mouseUp;
            set
            {
                if (_mouseUp == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseUp, nameof(EmitMapEvent));
                }
                else if (_mouseUp != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseUp);
                }

                _mouseUp = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> MouseWheel
        {
            get => _mouseWheel;
            set
            {
                if (_mouseWheel == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.MouseWheel, nameof(EmitMapEvent));
                }
                else if (_mouseWheel != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseWheel);
                }

                _mouseWheel = value;
            }
        }

        [Parameter]
        public Func<MouseEventArgs<BingMap>, Task> RightClick
        {
            get => _rightClick;
            set
            {
                if (_rightClick == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachEventFunctionName, Id, BingMapEvents.RightClick, nameof(EmitMapEvent));
                }
                else if (_rightClick != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.RightClick);
                }

                _rightClick = value;
            }
        }

        [Parameter]
        public Func<Task> ThrottleViewChange
        {
            get => _throttleViewChange;
            set
            {
                if (_throttleViewChange == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachThrottleEventFunctionName, Id, BingMapEvents.ViewChange, nameof(EmitMapChangeEvent));
                }
                else if (_throttleViewChange != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChange);
                }

                _throttleViewChange = value;
            }
        }

        [Parameter]
        public Func<Task> ThrottleViewChangeEnd
        {
            get => _throttleViewChangeEnd;
            set
            {
                if (_throttleViewChangeEnd == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachThrottleEventFunctionName, Id, BingMapEvents.ViewChangeEnd, nameof(EmitMapChangeEvent));
                }
                else if (_throttleViewChangeEnd != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChangeEnd);
                }

                _throttleViewChangeEnd = value;
            }
        }

        [Parameter]
        public Func<Task> ThrottleViewChangeStart
        {
            get => _throttleViewChangeStart;
            set
            {
                if (_throttleViewChangeStart == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachThrottleEventFunctionName, Id, BingMapEvents.ViewChangeStart, nameof(EmitMapChangeEvent));
                }
                else if (_throttleViewChangeStart != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.MouseUp);
                }

                _throttleViewChangeStart = value;
            }
        }

        [Parameter]
        public Func<Task> ViewChange
        {
            get => _viewChange;
            set
            {
                if (_viewChange == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.ViewChange, nameof(EmitMapChangeEvent));
                }
                else if (_viewChange != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChange);
                }

                _viewChange = value;
            }
        }

        [Parameter]
        public Func<Task> ViewChangeEnd
        {
            get => _viewChangeEnd;
            set
            {
                if (_viewChangeEnd == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.ViewChangeEnd, nameof(EmitMapChangeEvent));
                }
                else if (_viewChangeEnd != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChangeEnd);
                }

                _viewChangeEnd = value;
            }
        }

        [Parameter]
        public Func<Task> ViewChangeStart
        {
            get => _viewChangeStart;
            set
            {
                if (_viewChangeStart == null && value != null)
                {
                    _jSRuntime.InvokeAsync<object>(attachChangeEventFunctionName, Id, BingMapEvents.ViewChangeStart, nameof(EmitMapChangeEvent));
                }
                else if (_viewChangeStart != null && value == null)
                {
                    _jSRuntime.InvokeAsync<object>(detachEventFunctionName, Id, BingMapEvents.ViewChangeStart);
                }

                _viewChangeStart = value;
            }
        }

        #endregion Properties

        #region Methods

        [JSInvokable]
        public Task EmitMapChangeEvent(string eventName)
        {
            switch (eventName)
            {
                case BingMapEvents.ViewChangeStart:
                    ViewChangeStart?.Invoke();
                    break;

                case BingMapEvents.ViewChangeEnd:
                    ViewChangeEnd?.Invoke();
                    break;

                case BingMapEvents.ViewChange:
                    ViewChange?.Invoke();
                    break;

                case BingMapEvents.MapTypeChanged:
                    MapTypeChanged?.Invoke();
                    break;

                case "t_" + BingMapEvents.ViewChangeStart:
                    ThrottleViewChangeStart?.Invoke();
                    break;

                case "t_" + BingMapEvents.ViewChangeEnd:
                    ThrottleViewChangeEnd?.Invoke();
                    break;

                case "t_" + BingMapEvents.ViewChange:
                    ThrottleViewChange?.Invoke();
                    break;
            }
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task EmitMapEvent(MouseEventArgs<BingMap> args)
        {
            switch (args.EventName)
            {
                case BingMapEvents.Click:
                    Click?.Invoke(args);
                    break;

                case BingMapEvents.DoubleClick:
                    DoubleClick?.Invoke(args);
                    break;

                case BingMapEvents.RightClick:
                    RightClick?.Invoke(args);
                    break;

                case BingMapEvents.MouseMove:
                    MouseMove?.Invoke(args);
                    break;

                case BingMapEvents.MouseWheel:
                    MouseWheel?.Invoke(args);
                    break;

                case BingMapEvents.MouseDown:
                    MouseDown?.Invoke(args);
                    break;

                case BingMapEvents.MouseOut:
                    MouseOut?.Invoke(args);
                    break;

                case BingMapEvents.MouseOver:
                    MouseOver?.Invoke(args);
                    break;

                case BingMapEvents.MouseUp:
                    MouseUp?.Invoke(args);
                    break;
            }
            return Task.CompletedTask;
        }

        #endregion Methods
    }
}
