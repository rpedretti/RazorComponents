using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.BingMap.Collections;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Polygon;
using RPedretti.RazorComponents.Shared.Operators;
using System;
using System.ComponentModel;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap.Pages.Polygons
{
    public partial class PolygonsPage : IDisposable
    {
        #region Fields

        private Timer _changePolygonTimer;
        private BingMapPolygon _polygon;
        protected RazorComponents.BingMap.BingMap bingMap;
        protected string BingMapId = $"bing-map-polygons";
        protected DebounceDispatcher clickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher doubleClickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher downDispatcher = new DebounceDispatcher();
        protected BingMapEntityList Entities = new BingMapEntityList();
        protected DebounceDispatcher outDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher upDispatcher = new DebounceDispatcher();

        #endregion Fields

        #region Properties

        [Inject] protected ILogger<PolygonsPage> Logger { get; set; }

        protected BingMapConfig MapConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.Road,
            SupportedMapTypes = new string[] {
                BingMapTypes.Aerial,
                BingMapTypes.GrayScale,
                BingMapTypes.Road,
                BingMapTypes.BirdsEyes
            },
            EnableHighDpi = true,
            Zoom = 10,
            ShowTrafficButton = true
        };

        protected BingMapsViewConfig MapViewConfig { get; set; } = new BingMapsViewConfig();
        public bool Click { get; set; }
        public bool DoubleClick { get; set; }
        public bool Loading { get; set; } = true;
        public bool MouseDown { get; set; }
        public bool MouseOut { get; set; }
        public bool MouseOver { get; set; }
        public bool MouseUp { get; set; }

        #endregion Properties

        #region Methods

        private void Polygon_OnClick(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            Click = true;
            clickDispatcher.Debounce(2000, () => { Click = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnDoubleClick(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            DoubleClick = true;
            doubleClickDispatcher.Debounce(2000, () => { DoubleClick = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnMouseDown(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseDown = true;
            downDispatcher.Debounce(2000, () => { MouseDown = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnMouseOut(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseOver = false;
            MouseOut = true;
            outDispatcher.Debounce(2000, () => { MouseOut = false; StateHasChanged(); });

            StateHasChanged();
        }

        private void Polygon_OnMouseOver(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseOver = true;
            MouseOut = false;
            StateHasChanged();
        }

        private void Polygon_OnMouseUp(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseUp = true;
            upDispatcher.Debounce(2000, () => { MouseUp = false; StateHasChanged(); });
            StateHasChanged();
        }

        protected async Task MapLoaded()
        {
            Logger.LogDebug("map loaded");
            Loading = false;

            var bounds = await bingMap.GetBoundsAsync();

            var latitude = bounds.Center.Latitude;
            var longitude = bounds.Center.Longitude;

            _polygon = new BingMapPolygon
            {
                Coordinates = new BindingList<Geocoordinate>
                {
                    new Geocoordinate { Latitude = latitude + 0.1, Longitude = longitude - 0.1 },
                    new Geocoordinate { Latitude = latitude + 0.1, Longitude = longitude + 0.1 },
                    new Geocoordinate { Latitude = latitude - 0.1, Longitude = longitude + 0.1 },
                    new Geocoordinate { Latitude = latitude - 0.1, Longitude = longitude - 0.1 }
                },
                Options = new BingMapPolygonOptions
                {
                    FillColor = Color.FromSystemColor(System.Drawing.Color.FromArgb(0x7F, System.Drawing.Color.Blue)),
                    StrokeThickness = 2,
                    StrokeColor = Color.FromSystemColor(System.Drawing.Color.Red)
                }
            };

            _polygon.OnClick += Polygon_OnClick;
            _polygon.OnDoubleClick += Polygon_OnDoubleClick;
            _polygon.OnMouseDown += Polygon_OnMouseDown;
            _polygon.OnMouseOut += Polygon_OnMouseOut;
            _polygon.OnMouseOver += Polygon_OnMouseOver;
            _polygon.OnMouseUp += Polygon_OnMouseUp;

            var coords = JsonSerializer.Serialize(_polygon.Coordinates);
            var rings = JsonSerializer.Serialize(_polygon.Rings);
            var a = JsonSerializer.Serialize(_polygon.Options.FillColor);
            var b = JsonSerializer.Serialize(_polygon.OptionsSnapshot);
            var c = JsonSerializer.Serialize(_polygon.Metadata);

            Entities.Add(_polygon);

            _changePolygonTimer = new Timer(async o =>
            {
                bounds = await bingMap.GetBoundsAsync();

                latitude = bounds.Center.Latitude;
                longitude = bounds.Center.Longitude;
                _polygon.Rings = new BindingList<Geocoordinate[]>
                {
                    new Geocoordinate[]
                    {
                        new Geocoordinate { Latitude = latitude + 0.1, Longitude = longitude - 0.1 },
                        new Geocoordinate { Latitude = latitude + 0.1, Longitude = longitude + 0.1 },
                        new Geocoordinate { Latitude = latitude - 0.1, Longitude = longitude + 0.1 },
                        new Geocoordinate { Latitude = latitude - 0.1, Longitude = longitude - 0.1 }
                    },
                    new Geocoordinate[]
                    {
                        new Geocoordinate { Latitude = latitude + 0.05, Longitude = longitude - 0.05 },
                        new Geocoordinate { Latitude = latitude - 0.05, Longitude = longitude - 0.05 },
                        new Geocoordinate { Latitude = latitude - 0.05, Longitude = longitude + 0.05 },
                        new Geocoordinate { Latitude = latitude + 0.05, Longitude = longitude + 0.05 }
                    }
                };
                StateHasChanged();
            }, null, 3000, Timeout.Infinite);
        }

        public void Dispose()
        {
            _changePolygonTimer?.Dispose();
            _polygon.OnClick -= Polygon_OnClick;
            _polygon.OnDoubleClick -= Polygon_OnDoubleClick;
            _polygon.OnMouseDown -= Polygon_OnMouseDown;
            _polygon.OnMouseOut -= Polygon_OnMouseOut;
            _polygon.OnMouseOver -= Polygon_OnMouseOver;
            _polygon.OnMouseUp -= Polygon_OnMouseUp;
            _polygon.Dispose();
        }

        #endregion Methods
    }
}
