using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.BingMap;
using RPedretti.RazorComponents.BingMap.Collections;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.InfoBox;
using RPedretti.RazorComponents.BingMap.Entities.Layer;
using RPedretti.RazorComponents.BingMap.Entities.Pushpin;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Wasm.BingMap.Pages.PushPins
{
    public partial class PushPinsPage
    {
        #region Fields

        private int _index = 0;
        private bool _infoAttached;
        private InfoBox _infobox;
        protected RazorComponents.BingMap.BingMap bingMap;
        protected string BingMapId = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";
        protected BingMapEntityList Entities = new BingMapEntityList();
        protected BingMapLayer layer;

        #endregion Fields

        #region Properties

        [Inject] private DevToolService _devTool { get; set; }

        protected BingMapConfig MapsConfig { get; set; } = new BingMapConfig
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

        protected BingMapsViewConfig MapsViewConfig { get; set; } = new BingMapsViewConfig();
        public bool DisableAddButton { get; set; }
        public int MapClick { get; private set; }
        public int MapRightClick { get; private set; }

        #endregion Properties

        #region Methods

        private void Pushpin_OnDragEnd(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            (sender as BingMapPushpin).Center = e.Target.Center;
            StateHasChanged();
        }

        private async void Pushpin_OnDragStart(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            await _infobox.Options(new InfoboxOptions
            {
                Visible = false
            });
        }

        private async void Pushpin_OnMouseOut(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            await _infobox.Options(new InfoboxOptions
            {
                Visible = false
            });
        }

        private async void Pushpin_OnMouseOver(object sender, MouseEventArgs<BingMapPushpin> e)
        {
            var location = (sender as BingMapPushpin).Center;
            await _infobox.Options(new InfoboxOptions
            {
                Visible = true,
                Description = $"<p>{location.Latitude}</p><p>{location.Longitude}</p>",
                Location = new Location(location.Latitude, location.Longitude),
            });
        }

        protected async Task MapLoaded()
        {
            DisableAddButton = false;

            _infobox = new InfoBox(new Geocoordinate(), new InfoboxOptions()
            {
                Visible = false,
                Title = "Position",
                ShowCloseButton = false,
                Offset = new GeolocatonPoint { X = 0, Y = 30 }
            });

            if (!_infoAttached)
            {
                await _infobox.AttachMap(BingMapId);
                _infoAttached = true;
            }
            StateHasChanged();
        }

        public async Task AddPushpin()
        {
            var bounds = await bingMap.GetBoundsAsync();
            var pushpins = await _devTool.GetPushpins(3, bounds, new PushpinOptions
            {
                Draggable = true,
                Icon = "https://www.bingmapsportal.com/Content/images/poi_custom.png"
            });

            pushpins.ForEach(pushpin =>
            {
                _index++;
                pushpin.Id = _index.ToString();
                pushpin.Options.Text = _index.ToString();
                pushpin.OnMouseOver += Pushpin_OnMouseOver;
                pushpin.OnMouseOut += Pushpin_OnMouseOut;
                pushpin.OnDragEnd += Pushpin_OnDragEnd;
                pushpin.OnDragStart += Pushpin_OnDragStart;
            });

            Entities.AddRange(pushpins);
        }

        public void Dispose()
        {
            foreach (var item in Entities)
            {
                item.Dispose();
            }

            _infobox?.Dispose();
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

        public Task OnMapThrottleViewChangeEnd()
        {
            Console.WriteLine("Throttle View Change End");
            return Task.CompletedTask;
        }

        public void RemovePushpin(BingMapPushpin pushpin)
        {
            pushpin.OnMouseOver -= Pushpin_OnMouseOver;
            pushpin.OnMouseOut -= Pushpin_OnMouseOut;
            pushpin.OnDragEnd -= Pushpin_OnDragEnd;
            pushpin.OnDragStart -= Pushpin_OnDragStart;

            Entities.Remove(pushpin);
        }

        public void ToggleVisibility(BingMapPushpin pushpin)
        {
            var visible = !pushpin.OptionsSnapshot.Visible ?? false;
            pushpin.Options = new PushpinOptions
            {
                Visible = visible
            };

            StateHasChanged();
        }

        #endregion Methods
    }
}
