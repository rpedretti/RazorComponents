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
    public partial class BingMapBase : ComponentBase, IDisposable
    {
        #region Fields

        private const string _mapNamespace = "rpedrettiBlazorComponents.bingMap.map";
        private BingMapEntityList _entities;
        private BingMapLayerList _layers;
        private ObservableCollection<IBingMapModule> _modules;
        private bool _shouldRender;
        private BingMapsViewConfig _viewConfig;
        private bool modulesLoaded;
        private DotNetObjectReference<BingMapBase> thisRef;

        protected bool init;

        #endregion Fields

        #region Properties

        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] private ILogger<BingMapBase> Logger { get; set; }

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
                        ClearItems();
                    }

                    _entities = value;

                    if (_entities != null)
                    {
                        AddItems(0, Entities.Count);
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
        [Parameter] public string ApiKey { get; set; }

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

        public BingMapBase()
        {
            thisRef = DotNetObjectReference.Create(this);
        }

        #endregion Constructors

        #region Methods

        private async Task AddItemAsync(BaseBingMapEntity baseBingMapEntity)
        {
            try
            {
                await JSRuntime.InvokeAsync<object>($"{_mapNamespace}.addItem", Id, baseBingMapEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task AddItems(int start, int count)
        {
            await JSRuntime.InvokeAsync<object>($"{_mapNamespace}.addItems", Id, Entities.Skip(start).Take(count));
        }

        private async void BeforeRemoveRange(object sender, IEnumerable<BaseBingMapEntity> e)
        {
            await JSRuntime.InvokeAsync<object>($"{_mapNamespace}.removeItems", e);
        }

        private async Task ClearItems()
        {
            await JSRuntime.InvokeAsync<object>($"{_mapNamespace}.clearItems");
        }

        private void EntitiesChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    AddItemAsync(_entities[e.NewIndex]);
                    break;

                case ListChangedType.ItemChanged:
                    UpdateItemAsync(_entities[e.NewIndex]);
                    break;
            }
        }

        private void EntitiesRangeChanged(object sender, RangeChangdEventArgs e)
        {
            switch (e.Type)
            {
                case RangeChangeType.Add:
                    AddItems(e.StartIndex, e.Ammount);
                    break;
            }
        }

        private async void EntitiesRemoved(object sender, BaseBingMapEntity removed)
        {
            try
            {
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.removeItem", Id, removed);
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
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.removeLayer", Id, removed);
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
                        await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.addLayer", Id, _layers[e.NewIndex].Id);
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
            if (e.Action == NotifyCollectionChangedAction.Add && modulesLoaded)
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
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.removeItem", Id, baseBingMapEntity);
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
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.updateItem", Id, baseBingMapEntity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateView(BingMapsViewConfig viewConfig)
        {
            JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.updateView", Id, viewConfig);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!init)
            {
                init = true;
                BaseBingMapEntity.JSRuntime = JSRuntime;
                await JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.getMap", thisRef, Id, MapsConfig, ApiKey, MapLanguage);
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

            thisRef.Dispose();
            JSRuntime.InvokeAsync<object>("rpedrettiBlazorComponents.bingMap.map.unloadMap", Id);
        }

        public async Task<LocationRectangle> GetBoundsAsync()
        {
            return await JSRuntime.InvokeAsync<LocationRectangle>($"{_mapNamespace}.getBounds", Id);
        }

        public async Task<Geocoordinate> GetCenterAsync()
        {
            return await JSRuntime.InvokeAsync<Geocoordinate>($"{_mapNamespace}.getCenter", Id);
        }

        [JSInvokable]
        public async Task NotifyMapLoaded()
        {
            if (Modules != null)
            {
                foreach (var module in Modules)
                {
                    Logger.LogDebug($"init module [{module}]");
                    await module.InitAsync(Id);
                }
            }
            StateHasChanged();
            MapLoaded?.Invoke();
            modulesLoaded = true;
        }

        #endregion Methods
    }
}
