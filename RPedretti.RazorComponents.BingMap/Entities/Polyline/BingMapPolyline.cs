using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RPedretti.RazorComponents.BingMap.Entities.Polyline
{
    public partial class BingMapPolyline : BaseBingMapEntity
    {
        private const string _polylineNamespace = "rpedrettiBlazorComponents.bingMap.map.polyline";
        private const string _polylineGet = _polylineNamespace + ".getPropertie";
        private const string clearEventsFunctionName = _polylineNamespace + ".clearEvents";

        private DotNetObjectReference<BingMapPolyline> thisRef;
        private BindingList<Location> _coordinates = new BindingList<Location>();
        private BingMapPolylineOptions _options;

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

        private void CoordinatesChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        public BingMapPolylineOptions OptionsSnapshot { get; set; } = new BingMapPolylineOptions
        {
            Cursor = "pointer",
            Generalizable = true,
            Visible = true
        };
        public BingMapPolylineOptions Options
        {
            get => _options;
            set => SetParameter(ref _options, value, () => MergeSnapshot(value));
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

        public string Metadata { get; set; }

        public BingMapPolyline()
        {
            thisRef = DotNetObjectReference.Create(this);
            Id ??= "polyline-" + Guid.NewGuid().ToString();
        }

        public override void Dispose()
        {
            try
            {
                JSRuntime.InvokeAsync<object>($"{_polylineNamespace}.remove", Id).AsTask()
                    .ContinueWith(t => thisRef.Dispose());
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        private void AssureThisRef()
        {
            if (thisRef == null)
            {
                thisRef = DotNetObjectReference.Create(this);
            }
        }
    }
}
