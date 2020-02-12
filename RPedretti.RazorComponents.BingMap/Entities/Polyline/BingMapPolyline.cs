using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RPedretti.RazorComponents.BingMap.Entities.Polyline
{
    public partial class BingMapPolyline : BaseBingMapEntity
    {
        #region Fields

        private const string _polylineGet = _polylineNamespace + ".getPropertie";
        private const string _polylineNamespace = "rpedrettiBlazorComponents.bingMap.map.polyline";
        private const string clearEventsFunctionName = _polylineNamespace + ".clearEvents";

        private BindingList<Location> _coordinates = new BindingList<Location>();
        private BingMapPolylineOptions _options;
        private DotNetObjectReference<BingMapPolyline> thisRef;

        #endregion Fields

        #region Properties

        [JsonPropertyName("coordinates")]
        public BindingList<Location> Coordinates
        {
            get => _coordinates;
            set
            {
                if (!EqualityComparer<BindingList<Location>>.Default.Equals(_coordinates, value))
                {
                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged -= CoordinatesChanged;
                    }

                    _coordinates = value;

                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged += CoordinatesChanged;
                    }
                }
            }
        }

        [JsonPropertyName("metadata")]
        public string Metadata { get; set; }

        [JsonPropertyName("options")]
        public BingMapPolylineOptions Options
        {
            get => _options;
            set => SetParameter(ref _options, value, () => MergeSnapshot(value));
        }

        [JsonIgnore]
        public BingMapPolylineOptions OptionsSnapshot { get; set; } = new BingMapPolylineOptions
        {
            Cursor = "pointer",
            Generalizable = true,
            Visible = true
        };

        #endregion Properties

        #region Constructors

        public BingMapPolyline()
        {
            thisRef = DotNetObjectReference.Create(this);
            Id ??= "polyline-" + Guid.NewGuid().ToString();
        }

        #endregion Constructors

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
            if (_onClick == null && _onDoubleClick == null && _onMouseDown == null && _onMouseOut == null && _onMouseOver == null && _onMouseUp == null)
            {
                if (thisRef != null)
                {
                    thisRef.Dispose();
                }
            }
        }

        private void CoordinatesChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private void MergeSnapshot(BingMapPolylineOptions value)
        {
            OptionsSnapshot.Cursor = value?.Cursor ?? OptionsSnapshot?.Cursor;
            OptionsSnapshot.Generalizable = value?.Generalizable ?? OptionsSnapshot?.Generalizable;
            OptionsSnapshot.StrokeColor = value?.StrokeColor ?? OptionsSnapshot.StrokeColor;
            OptionsSnapshot.StrokeDashArray = value?.StrokeDashArray ?? OptionsSnapshot?.StrokeDashArray;
            OptionsSnapshot.StrokeThickness = value?.StrokeThickness ?? OptionsSnapshot?.StrokeThickness;
            OptionsSnapshot.Visible = value?.Visible ?? OptionsSnapshot?.Visible;
        }

        public override void Dispose()
        {
            try
            {
                JSRuntime.InvokeAsync<object>($"{_polylineNamespace}.remove", Id).AsTask()
                    .ContinueWith(t => thisRef.Dispose());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion Methods
    }
}
