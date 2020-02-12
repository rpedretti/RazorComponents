using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using RPedretti.RazorComponents.BingMap.Collections;
using RPedretti.RazorComponents.BingMap.Entities;
using RPedretti.RazorComponents.BingMap.Entities.Layer;
using RPedretti.RazorComponents.BingMap.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap
{
    public partial class BingMap : IDisposable
    {
        #region Fields

        private const string _mapNamespace = "rpedrettiBlazorComponents.bingMap.map";
        private readonly DotNetObjectReference<BingMap> _thisRef;
        private BingMapEntityList _entities;
        private BingMapLayerList _layers;
        private ObservableCollection<IBingMapModule> _modules;
        private bool _shouldRender;
        private BingMapsViewConfig _viewConfig;
        private bool _modulesLoaded;
        protected bool init;

        #endregion Fields

        #region Properties

        [Inject] private IJSRuntime _jSRuntime { get; set; }
        [Inject] private ILogger<BingMap> _logger { get; set; }

        [Parameter] public string ApiKey { get; set; }

        [Parameter]
        public BingMapEntityList Entities
        {
            get => _entities;
            set
            {
                if (!EqualityComparer<BindingList<BaseBingMapEntity>>.Default.Equals(_entities, value))
                {
                    if (_entities != null)
                    {
                        _entities.ListChanged -= EntitiesChanged;
                        _entities.BeforeRemove -= EntitiesRemoved;
                        _entities.BeforeRemoveRange -= BeforeRemoveRange;
                        _entities.ListRangeChanged -= EntitiesRangeChanged;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        ClearItems();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    }

                    _entities = value;

                    if (_entities != null)
                    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        AddItems(0, Entities.Count);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        _entities.ListChanged += EntitiesChanged;
                        _entities.BeforeRemove += EntitiesRemoved;
                        _entities.BeforeRemoveRange += BeforeRemoveRange;
                        _entities.ListRangeChanged += EntitiesRangeChanged;
                    }
                }
            }
        }

        [Parameter] public string Id { get; set; } = $"bing-map-{Guid.NewGuid().ToString().Replace("-", "")}";

        [Parameter]
        public BingMapLayerList Layers
        {
            get => _layers;
            set
            {
                if (_layers != null)
                {
                    _layers.ListChanged -= LayersChanged;
                    _layers.BeforeRemove -= LayerRemoved;
                }

                _layers = value;

                if (_layers != null)
                {
                    _layers.ListChanged += LayersChanged;
                    _layers.BeforeRemove += LayerRemoved;
                }
            }
        }

        [Parameter] public string MapLanguage { get; set; }
        [Parameter] public Func<Task> MapLoaded { get; set; }
        [Parameter] public BingMapConfig MapsConfig { get; set; } = new BingMapConfig();

        [Parameter]
        public ObservableCollection<IBingMapModule> Modules
        {
            get => _modules;
            set
            {
                if (_modules != null)
                {
                    _modules.CollectionChanged -= ModulesChanged;
                }

                _modules = value;

                if (_modules != null)
                {
                    _modules.CollectionChanged += ModulesChanged;
                }
            }
        }

        [Parameter]
        public BingMapsViewConfig ViewConfig
        {
            get => _viewConfig;
            set
            {
                SetParameter(ref _viewConfig, value);
                _shouldRender = true;
            }
        }

        #endregion Properties

        #region Constructors

        public BingMap()
        {
            _thisRef = DotNetObjectReference.Create(this);
        }

        #endregion Constructors

        #region Methods

        private async Task AddItemAsync(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                await _jSRuntime.InvokeAsync<object>($"{_mapNamespace}.addItem", Id, baseBingMapEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task AddItems(int start, int count)
        {
            var entities = Entities.Skip(start).Take(count).Select(e => e as object);
            await _jSRuntime.InvokeAsync<object>($"{_mapNamespace}.addItems", Id, entities);
        }

        private async void BeforeRemoveRange(object sender, IEnumerable<BaseBingMapEntity> e)
        {
            await _jSRuntime.InvokeAsync<object>($"{_mapNamespace}.removeItems", e.Select(_e => _e as object));
        }

        private async Task ClearItems()
        {
            await _jSRuntime.InvokeAsync<object>($"{_mapNamespace}.clearItems");
        }

        private void EntitiesChanged(object sender, ListChangedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    AddItemAsync(_entities[e.NewIndex]);
                    break;

                case ListChangedType.ItemChanged:
                    UpdateItemAsync(_entities[e.NewIndex]);
                    break;
            }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void EntitiesRangeChanged(object sender, RangeChangdEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            switch (e.Type)
            {
                case RangeChangeType.Add:
                    AddItems(e.StartIndex, e.Ammount);
                    break;
            }
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private async void EntitiesRemoved(object sender, BaseBingMapEntity removed)
        {
            try
            {
                await _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.removeItem", Id, removed as object);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void LayerRemoved(object sender, BingMapLayer removed)
        {
            try
            {
                await _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.removeLayer", Id, removed as object);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void LayersChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                switch (e.ListChangedType)
                {
                    case ListChangedType.ItemAdded:
                        await _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.addLayer", Id, _layers[e.NewIndex].Id);
                        break;

                    case ListChangedType.ItemChanged:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ModulesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && _modulesLoaded)
            {
                foreach (IBingMapModule module in e.NewItems)
                {
                    module.InitAsync(Id);
                }
            }
        }

        private async Task RemoveItemAsync(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                await _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.removeItem", Id, baseBingMapEntity as object);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private bool SetParameter<T>(ref T prop, T value, Action onChange = null)
        {
            if (EqualityComparer<T>.Default.Equals(prop, value))
            {
                return false;
            }

            prop = value;
            onChange?.Invoke();

            return true;
        }

        private async Task UpdateItemAsync(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                var b = System.Text.Json.JsonSerializer.Serialize(baseBingMapEntity);
                await _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.updateItem", Id, baseBingMapEntity as object);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateView(BingMapsViewConfig viewConfig)
        {
            _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.updateView", Id, viewConfig as object);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!init)
            {
                init = true;
                BaseBingMapEntity.JSRuntime = _jSRuntime;
                await _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.getMap", _thisRef, Id, MapsConfig, ApiKey, MapLanguage);
            }

            UpdateView(ViewConfig);
            _shouldRender = false;
        }

        protected override bool ShouldRender()
        {
            return _shouldRender;
        }

        public void Dispose()
        {
            Modules = null;
            Entities = null;
            Layers = null;
            MapLoaded = null;

            _thisRef.Dispose();
            _jSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.unloadMap", Id);
        }

        public async Task<LocationRectangle> GetBoundsAsync()
        {
            return await _jSRuntime.InvokeAsync<LocationRectangle>($"{_mapNamespace}.getBounds", Id);
        }

        public async Task<Geocoordinate> GetCenterAsync()
        {
            return await _jSRuntime.InvokeAsync<Geocoordinate>($"{_mapNamespace}.getCenter", Id);
        }

        [JSInvokable]
        public async Task NotifyMapLoaded()
        {
            if (Modules != null)
            {
                foreach (var module in Modules)
                {
                    _logger.LogDebug($"init module [{module}]");
                    await module.InitAsync(Id);
                }
            }
            StateHasChanged();
            MapLoaded?.Invoke();
            _modulesLoaded = true;
        }

        #endregion Methods
    }
}
