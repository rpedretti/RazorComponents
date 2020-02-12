using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.BingMap.Collections;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Polyline;
using RPedretti.RazorComponents.Shared.Operators;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap.Pages.Polylines
{
    public partial class PolylinesPage : IDisposable
    {
        #region Fields

        private Timer _changePolylineTimer;
        private BingMapPolyline _polyLine;
        protected RazorComponents.BingMap.BingMap bingMap;
        protected string BingMapId = $"bing-map-polylines";
        protected DebounceDispatcher clickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher doubleClickDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher downDispatcher = new DebounceDispatcher();
        protected BingMapEntityList Entities = new BingMapEntityList();
        protected DebounceDispatcher outDispatcher = new DebounceDispatcher();
        protected DebounceDispatcher upDispatcher = new DebounceDispatcher();

        #endregion Fields

        #region Properties

        [Inject] protected ILogger<PolylinesPage> Logger { get; set; }

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
            Zoom = 10
        };

        protected BingMapsViewConfig MapViewConfig { get; set; } = new BingMapsViewConfig();
        public bool Click { get; set; }
        public bool DoubleClick { get; set; }
        public bool Loading { get; set; } = true;
        public int MapClick { get; private set; }
        public int MapRightClick { get; private set; }
        public bool MouseDown { get; set; }
        public bool MouseOut { get; set; }
        public bool MouseOver { get; set; }
        public bool MouseUp { get; set; }

        #endregion Properties

        #region Methods

        private void PolyLine_OnClick(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            Click = true;
            clickDispatcher.Debounce(2000, () => { Click = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnDoubleClick(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            DoubleClick = true;
            doubleClickDispatcher.Debounce(2000, () => { DoubleClick = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnMouseDown(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseDown = true;
            downDispatcher.Debounce(2000, () => { MouseDown = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnMouseOut(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseOver = false;
            MouseOut = true;
            outDispatcher.Debounce(2000, () => { MouseOut = false; StateHasChanged(); });

            StateHasChanged();
        }

        private void PolyLine_OnMouseOver(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseOver = true;
            MouseOut = false;
            StateHasChanged();
        }

        private void PolyLine_OnMouseUp(object sender, MouseEventArgs<BingMapPolyline> e)
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

            _polyLine = new BingMapPolyline
            {
                Coordinates = new BindingList<Location>
                {
                    new Location(bounds.Center.Latitude + bounds.Height / 3f, bounds.Center.Longitude - bounds.Width / 3f),
                    new Location(bounds.Center.Latitude, bounds.Center.Longitude),
                    new Location(bounds.Center.Latitude + bounds.Height / 3f, bounds.Center.Longitude + bounds.Width / 3f),
                },
                Options = new BingMapPolylineOptions
                {
                    StrokeColor = Color.FromSystemColor(System.Drawing.Color.Red),
                    StrokeThickness = 3,
                    StrokeDashArray = new int[] { 3, 3, 0, 2 }
                }
            };

            _polyLine.OnClick += PolyLine_OnClick;
            _polyLine.OnDoubleClick += PolyLine_OnDoubleClick;
            _polyLine.OnMouseDown += PolyLine_OnMouseDown;
            _polyLine.OnMouseOut += PolyLine_OnMouseOut;
            _polyLine.OnMouseOver += PolyLine_OnMouseOver;
            _polyLine.OnMouseUp += PolyLine_OnMouseUp;

            Entities.Add(_polyLine);

            _changePolylineTimer = new Timer(async (o) =>
            {
                var newBounds = await bingMap.GetBoundsAsync();
                var latitude = newBounds.Center.Latitude;
                var longitude = newBounds.Center.Longitude - newBounds.Height / 4f;

                _polyLine.Coordinates.Insert(1, new Location(latitude, longitude));
                StateHasChanged();
            }, null, 3000, Timeout.Infinite);

            StateHasChanged();
        }

        public void Dispose()
        {
            if (_polyLine != null)
            {
                _polyLine.OnClick -= PolyLine_OnClick;
                _polyLine.OnDoubleClick -= PolyLine_OnDoubleClick;
                _polyLine.OnMouseDown -= PolyLine_OnMouseDown;
                _polyLine.OnMouseOut -= PolyLine_OnMouseOut;
                _polyLine.OnMouseOver -= PolyLine_OnMouseOver;
                _polyLine.OnMouseUp -= PolyLine_OnMouseUp;
                _polyLine.Dispose();
            }
            _changePolylineTimer?.Dispose();
        }

        public Task OnMapClick(MouseEventArgs<RazorComponents.BingMap.BingMap> args)
        {
            MapClick++;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public Task OnMapRightClick(MouseEventArgs<RazorComponents.BingMap.BingMap> args)
        {
            MapRightClick++;
            StateHasChanged();
            return Task.CompletedTask;
        }

        #endregion Methods
    }
}
