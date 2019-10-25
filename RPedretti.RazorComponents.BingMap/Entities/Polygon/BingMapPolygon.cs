using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap.Entities.Polygon
{
    public partial class BingMapPolygon : BaseBingMapEntity
    {
        #region Fields

        private const string _polygonGet = _polygonNamespace + ".getPropertie";
        private const string _polygonNamespace = "rpedrettiBlazorComponents.bingMap.map.polygon";
        private const string clearEventsFunctionName = _polygonNamespace + ".clearEvents";

        private BindingList<Geocoordinate> _coordinates = new BindingList<Geocoordinate>();
        private BingMapPolygonOptions _options;
        private BindingList<Geocoordinate[]> _rings = new BindingList<Geocoordinate[]>();
        private DotNetObjectReference<BingMapPolygon> thisRef;

        #endregion Fields

        #region Properties

        public BindingList<Geocoordinate> Coordinates
        {
            get => _coordinates;
            set
            {
                if (!EqualityComparer<BindingList<Geocoordinate>>.Default.Equals(_coordinates, value))
                {
                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged -= CoordinatesChanged;
                    }

                    SetParameter(ref _coordinates, value);

                    if (_coordinates != null)
                    {
                        _coordinates.ListChanged += CoordinatesChanged;
                    }
                }
            }
        }

        public string Metadata { get; set; }

        public BingMapPolygonOptions Options
        {
            get => _options;
            set => SetParameter(ref _options, value, () => MergeSnapshot(value));
        }

        public BingMapPolygonOptions OptionsSnapshot { get; set; } = new BingMapPolygonOptions
        {
            Cursor = "pointer",
            Generalizable = true,
            Visible = true
        };

        public BindingList<Geocoordinate[]> Rings
        {
            get => _rings;
            set
            {
                if (!EqualityComparer<BindingList<Geocoordinate[]>>.Default.Equals(_rings, value))
                {
                    if (_rings != null)
                    {
                        _rings.ListChanged -= RingsChanged;
                    }

                    SetParameter(ref _rings, value);

                    if (_rings != null)
                    {
                        _rings.ListChanged += RingsChanged;
                    }
                }
            }
        }

        #endregion Properties

        #region Constructors

        public BingMapPolygon()
        {
            thisRef = DotNetObjectReference.Create(this);
            Id ??= "polygon-" + Guid.NewGuid().ToString();
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

        private void CoordinatesChanged(object sender, ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private void MergeSnapshot(BingMapPolygonOptions value)
        {
            OptionsSnapshot.Cursor = value?.Cursor ?? OptionsSnapshot?.Cursor;
            OptionsSnapshot.Generalizable = value?.Generalizable ?? OptionsSnapshot?.Generalizable;
            OptionsSnapshot.StrokeColor = value?.StrokeColor ?? OptionsSnapshot.StrokeColor;
            OptionsSnapshot.FillColor = value?.FillColor ?? OptionsSnapshot.FillColor;
            OptionsSnapshot.StrokeDashArray = value?.StrokeDashArray ?? OptionsSnapshot?.StrokeDashArray;
            OptionsSnapshot.StrokeThickness = value?.StrokeThickness ?? OptionsSnapshot?.StrokeThickness;
            OptionsSnapshot.Visible = value?.Visible ?? OptionsSnapshot?.Visible;
        }

        private void RingsChanged(object sender, ListChangedEventArgs e)
        {
            RaisePropertyChanged();
        }

        private async Task UpdateLocations()
        {
            try
            {
                await JSRuntime.InvokeAsync<object>($"{_polygonNamespace}.update", Id, this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override void Dispose()
        {
            try
            {
                JSRuntime.InvokeAsync<object>($"{_polygonNamespace}.remove", Id)
                    .AsTask().ContinueWith(t => thisRef.Dispose());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion Methods
    }
}
