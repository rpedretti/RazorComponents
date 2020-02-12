using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap.Entities.InfoBox
{
    public partial class InfoBox : BaseBingMapEntity
    {
        #region Fields

        private const string _infoboxGet = _infoboxNamespace + ".get";
        private const string _infoboxNamespace = "rpedrettiBlazorComponents.bingMap.map.infobox";
        private const string _infoboxRegister = _infoboxNamespace + ".register";
        private const string _infoboxSet = _infoboxNamespace + ".set";
        private const string _mapsNamespace = "rpedrettiBlazorComponents.bingMap.map";
        private readonly DotNetObjectReference<InfoBox> thisRef;
        private string _htmlContent;
        private InfoboxOptions _options;

        #endregion Fields

        #region Properties

        public Geocoordinate Center { get; set; }

        #endregion Properties

        #region Constructors

        public InfoBox(Geocoordinate center) : this(center, null, Guid.NewGuid().ToString())
        {
        }

        public InfoBox(Geocoordinate center, InfoboxOptions options) : this(center, options, Guid.NewGuid().ToString())
        {
        }

        public InfoBox(Geocoordinate center, InfoboxOptions options, string id)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Center = center;
            JSRuntime.InvokeAsync<object>(_infoboxRegister, Id, center, options);
            thisRef = DotNetObjectReference.Create(this);
        }

        #endregion Constructors

        #region Methods

        public async Task<InfoboxAction> Actions() => await JSRuntime.InvokeAsync<InfoboxAction>(_infoboxGet, Id, nameof(Actions));

        public async Task<GeolocatonPoint> Anchor() => await JSRuntime.InvokeAsync<GeolocatonPoint>(_infoboxGet, Id, nameof(Actions));

        public async Task AttachMap(string mapId) => await JSRuntime.InvokeAsync<bool>(_mapsNamespace + ".attachInfoBox", mapId, Id);

        public async Task<string> Description() => await JSRuntime.InvokeAsync<string>(_infoboxGet, Id, nameof(Actions));

        public override void Dispose()
        {
            thisRef.Dispose();
        }

        public async Task<double> Height() => await JSRuntime.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));

        public async Task<string> HtmlContent() => await JSRuntime.InvokeAsync<string>(_infoboxGet, Id, nameof(Actions));

        public async Task HtmlContent(string content)
        {
            if (SetParameter(ref _htmlContent, content))
            {
                await JSRuntime.InvokeAsync<string>(_infoboxSet + nameof(HtmlContent), Id, content);
            }
        }

        public async Task<Location> Location() => await JSRuntime.InvokeAsync<Location>(_infoboxGet, Id, nameof(Actions));

        public async Task Location(Location location) => await JSRuntime.InvokeAsync<Location>(_infoboxSet + nameof(Location), Id, location);

        public async Task<double> MaxHeight() => await JSRuntime.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));

        public async Task<double> MaxWidth() => await JSRuntime.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));

        public async Task<GeolocatonPoint> Offset() => await JSRuntime.InvokeAsync<GeolocatonPoint>(_infoboxGet, Id, nameof(Actions));

        public async Task<InfoboxOptions> Options() => await JSRuntime.InvokeAsync<InfoboxOptions>(_infoboxGet, Id, nameof(Actions));

        public async Task Options(InfoboxOptions options)
        {
            if (SetParameter(ref _options, options))
            {
                await JSRuntime.InvokeAsync<object>(_infoboxSet + nameof(Options), Id, options);
            }
        }

        public async Task<bool> ShowCloseButton() => await JSRuntime.InvokeAsync<bool>(_infoboxGet, Id, nameof(Actions));

        public async Task<bool> ShowPointer() => await JSRuntime.InvokeAsync<bool>(_infoboxGet, Id, nameof(Actions));

        public async Task<string> Title() => await JSRuntime.InvokeAsync<string>(_infoboxGet, Id, nameof(Actions));

        public async Task<bool> Visible() => await JSRuntime.InvokeAsync<bool>(_infoboxGet, Id, nameof(Actions));

        public async Task<double> Width() => await JSRuntime.InvokeAsync<double>(_infoboxGet, Id, nameof(Actions));

        public async Task<int> ZIndex() => await JSRuntime.InvokeAsync<int>(_infoboxGet, Id, nameof(Actions));

        #endregion Methods
    }
}
