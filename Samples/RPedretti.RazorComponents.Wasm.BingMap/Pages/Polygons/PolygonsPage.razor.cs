using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.BingMap.Collections;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Polygon;
using RPedretti.RazorComponents.Shared.Components;
using RPedretti.RazorComponents.Shared.Operators;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap.Pages.Polygons
{
    public class PolygonsPageBase : BaseComponent
    {
        #region Fields

        private Timer changePolygonTimer;
        private BingMapPolygon polygon;
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

        [Inject] protected ILogger<PolygonsPageBase> Logger { get; set; }

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
            clickDispatcher.Debounce(2000, (o) => { Click = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnDoubleClick(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            DoubleClick = true;
            doubleClickDispatcher.Debounce(2000, (o) => { DoubleClick = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnMouseDown(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseDown = true;
            downDispatcher.Debounce(2000, (o) => { MouseDown = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void Polygon_OnMouseOut(object sender, MouseEventArgs<BingMapPolygon> e)
        {
            MouseOver = false;
            MouseOut = true;
            outDispatcher.Debounce(2000, (o) => { MouseOut = false; StateHasChanged(); });

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
            upDispatcher.Debounce(2000, (o) => { MouseUp = false; StateHasChanged(); });
            StateHasChanged();
        }

        protected async Task MapLoaded()
        {
            Logger.LogDebug("map loaded");
            Loading = false;

            var bounds = await bingMap.GetBoundsAsync();

            var latitude = bounds.Center.Latitude;
            var longitude = bounds.Center.Longitude;

            polygon = new BingMapPolygon
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
                    FillColor = Color.FromArgb(0x7F, Color.Blue),
                    StrokeThickness = 2,
                    StrokeColor = Color.Red
                }
            };

            polygon.OnClick += Polygon_OnClick;
            polygon.OnDoubleClick += Polygon_OnDoubleClick;
            polygon.OnMouseDown += Polygon_OnMouseDown;
            polygon.OnMouseOut += Polygon_OnMouseOut;
            polygon.OnMouseOver += Polygon_OnMouseOver;
            polygon.OnMouseUp += Polygon_OnMouseUp;

            var coords = JsonSerializer.Serialize(polygon.Coordinates);
            var rings = JsonSerializer.Serialize(polygon.Rings);
            var a = JsonSerializer.Serialize(polygon.Options.FillColor);
            var b = JsonSerializer.Serialize(polygon.OptionsSnapshot);
            var c = JsonSerializer.Serialize(polygon.Metadata);

            Entities.Add(polygon);

            changePolygonTimer = new Timer(async o =>
            {
                bounds = await bingMap.GetBoundsAsync();

                latitude = bounds.Center.Latitude;
                longitude = bounds.Center.Longitude;
                polygon.Rings = new BindingList<Geocoordinate[]>
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
            changePolygonTimer?.Dispose();
            polygon.OnClick -= Polygon_OnClick;
            polygon.OnDoubleClick -= Polygon_OnDoubleClick;
            polygon.OnMouseDown -= Polygon_OnMouseDown;
            polygon.OnMouseOut -= Polygon_OnMouseOut;
            polygon.OnMouseOver -= Polygon_OnMouseOver;
            polygon.OnMouseUp -= Polygon_OnMouseUp;
            polygon.Dispose();
        }

        #endregion Methods
    }
}
