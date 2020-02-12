﻿using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.Pushpin
{
    public partial class BingMapPushpin : BaseBingMapEntity
    {
        #region Fields

        private const string _pushpinGet = _pushpinNamespace + ".getPropertie";
        private const string _pushpinNamespace = "rpedrettiBlazorComponents.bingMap.map.pushpin";
        private const string _pushpinRemove = _pushpinNamespace + ".remove";
        private Geocoordinate _center = new Geocoordinate();
        private PushpinOptions _options;
        private DotNetObjectReference<BingMapPushpin> thisRef;

        #endregion Fields

        #region Properties

        [JsonPropertyName("center")]
        public Geocoordinate Center { get => _center; set => SetParameter(ref _center, value); }

        [JsonPropertyName("options")]
        public PushpinOptions Options
        {
            get => _options;
            set
            {
                if (SetParameter(ref _options, value))
                {
                    MergeSnapshot(value);

                    if (_options.Draggable.HasValue && _options.Draggable.Value)
                    {
                        OnDrag += UpdateCenter;
                    }
                    else if (_options.Draggable.HasValue && !_options.Draggable.Value)
                    {
                        OnDrag -= UpdateCenter;
                    }
                }
            }
        }

        public PushpinOptions OptionsSnapshot { get; private set; } = new PushpinOptions
        {
            Cursor = "pointer",
            Draggable = false,
            EnableClickedStyle = false,
            EnableHoverStyle = false,
            RoundClickableArea = false,
            TextOffset = new GeolocatonPoint { X = 0, Y = 0.5 },
            Visible = true
        };

        #endregion Properties

        #region Methods

        private void AssureThisRef()
        {
            if (thisRef == null)
            {
                thisRef = DotNetObjectReference.Create(this);
            }
        }

        private void CheckThisRef()
        {
            if (
                _onClick == null && _onDoubleClick == null &&
                _onDrag == null && _onDragEnd == null && _onDragStart == null &&
                _onMouseDown == null && _onMouseOut == null && _onMouseOver == null && _onMouseUp == null)
            {
                thisRef?.Dispose();
            }
        }

        private void MergeSnapshot(PushpinOptions options)
        {
            OptionsSnapshot.Anchor = options?.Anchor ?? OptionsSnapshot.Anchor;
            OptionsSnapshot.Color = options?.Color ?? OptionsSnapshot.Color;
            OptionsSnapshot.Cursor = options?.Cursor ?? OptionsSnapshot.Cursor;
            OptionsSnapshot.Draggable = options?.Draggable ?? OptionsSnapshot.Draggable;
            OptionsSnapshot.EnableClickedStyle = options?.EnableClickedStyle ?? OptionsSnapshot.EnableClickedStyle;
            OptionsSnapshot.EnableHoverStyle = options?.EnableHoverStyle ?? OptionsSnapshot.EnableHoverStyle;
            OptionsSnapshot.Icon = options?.Icon ?? OptionsSnapshot.Icon;
            OptionsSnapshot.RoundClickableArea = options?.RoundClickableArea ?? OptionsSnapshot.RoundClickableArea;
            OptionsSnapshot.SubTitlte = options?.SubTitlte ?? OptionsSnapshot.SubTitlte;
            OptionsSnapshot.Text = options?.Text ?? OptionsSnapshot.Text;
            OptionsSnapshot.TextOffset = options?.TextOffset ?? OptionsSnapshot.TextOffset;
            OptionsSnapshot.Titlte = options?.Titlte ?? OptionsSnapshot.Titlte;
            OptionsSnapshot.Visible = options?.Visible ?? OptionsSnapshot.Visible;
        }

        private void UpdateCenter(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            Center = e.Target.Center;
        }

        public override void Dispose()
        {
            OnDragEnd -= UpdateCenter;
            JSRuntime.InvokeAsync<object>(_pushpinRemove, Id);
            thisRef?.Dispose();
        }

        #endregion Methods
    }
}
