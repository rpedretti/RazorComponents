using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.BingMap.Collections;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Polyline;
using RPedretti.RazorComponents.Shared.Components;
using RPedretti.RazorComponents.Shared.Operators;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap.Pages.PolyLines
{
    public class PolyLinesPageBase : BaseComponent, IDisposable
    {
        #region Fields

        private Timer changePolylineTimer;
        private BingMapPolyline polyLine;
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

        [Inject] protected ILogger<PolyLinesPageBase> Logger { get; set; }

        protected BingMapConfig MapConfig { get; set; } = new BingMapConfig
        {
            MapTypeId = BingMapTypes.Road,
            SupportedMapTypes = new string[] {
                BingMapTypes.Aerial,
                BingMapTypes.GrayScale,
                BingMapTypes.Road,
                BingMapTypes.BirdsEyes
            },
            EnableHighDpi = true
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

        private void PolyLine_OnClick(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            Click = true;
            clickDispatcher.Debounce(2000, (o) => { Click = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnDoubleClick(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            DoubleClick = true;
            doubleClickDispatcher.Debounce(2000, (o) => { DoubleClick = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnMouseDown(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseDown = true;
            downDispatcher.Debounce(2000, (o) => { MouseDown = false; StateHasChanged(); });
            StateHasChanged();
        }

        private void PolyLine_OnMouseOut(object sender, MouseEventArgs<BingMapPolyline> e)
        {
            MouseOver = false;
            MouseOut = true;
            outDispatcher.Debounce(2000, (o) => { MouseOut = false; StateHasChanged(); });

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
            upDispatcher.Debounce(2000, (o) => { MouseUp = false; StateHasChanged(); });
            StateHasChanged();
        }

        protected async Task MapLoaded()
        {
            Logger.LogDebug("map loaded");
            Loading = false;

            var bounds = await bingMap.GetBoundsAsync();

            polyLine = new BingMapPolyline
            {
                Coordinates = new BindingList<Location>
                {
                    new Location(bounds.Center.Latitude + bounds.Height / 3f, bounds.Center.Longitude - bounds.Width / 3f),
                    new Location(bounds.Center.Latitude, bounds.Center.Longitude),
                    new Location(bounds.Center.Latitude + bounds.Height / 3f, bounds.Center.Longitude + bounds.Width / 3f),
                },
                Options = new BingMapPolylineOptions
                {
                    StrokeColor = Color.Red,
                    StrokeThickness = 3,
                    StrokeDashArray = new int[] { 3, 3, 0, 2 }
                }
            };

            polyLine.OnClick += PolyLine_OnClick;
            polyLine.OnDoubleClick += PolyLine_OnDoubleClick;
            polyLine.OnMouseDown += PolyLine_OnMouseDown;
            polyLine.OnMouseOut += PolyLine_OnMouseOut;
            polyLine.OnMouseOver += PolyLine_OnMouseOver;
            polyLine.OnMouseUp += PolyLine_OnMouseUp;

            Entities.Add(polyLine);

            changePolylineTimer = new Timer(async (o) =>
            {
                var newBounds = await bingMap.GetBoundsAsync();
                var latitude = newBounds.Center.Latitude;
                var longitude = newBounds.Center.Longitude - newBounds.Height / 4f;

                polyLine.Coordinates.Insert(1, new Location(latitude, longitude));
                StateHasChanged();
            }, null, 3000, Timeout.Infinite);

            StateHasChanged();
        }

        public void Dispose()
        {
            if (polyLine != null)
            {
                polyLine.OnClick -= PolyLine_OnClick;
                polyLine.OnDoubleClick -= PolyLine_OnDoubleClick;
                polyLine.OnMouseDown -= PolyLine_OnMouseDown;
                polyLine.OnMouseOut -= PolyLine_OnMouseOut;
                polyLine.OnMouseOver -= PolyLine_OnMouseOver;
                polyLine.OnMouseUp -= PolyLine_OnMouseUp;
                polyLine.Dispose();
            }
            changePolylineTimer?.Dispose();
        }

        #endregion Methods
    }
}
