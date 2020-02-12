using Microsoft.JSInterop;
using RPedretti.RazorComponents.BingMap.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap.Entities.Layer
{
    public class BingMapLayer : BaseBingMapEntity, IEnumerable<BaseBingMapEntity>
    {
        #region Fields

        private const string layerNamespace = "rpedrettiBlazorComponents.bingMap.map.layer";
        private readonly BingMapEntityList _items = new BingMapEntityList();
        private readonly DotNetObjectReference<BingMapLayer> thisRef;

        #endregion Fields

        #region Constructors

        public BingMapLayer(string id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            thisRef = DotNetObjectReference.Create(this);
            _items.ListChanged += ItemsChanged;
            _items.BeforeRemoveRange += BeforeRemoveRange;
            _items.ListRangeChanged += ListRangeChanged;
            _items.BeforeRemove += ItemRemoved;
            JSRuntime.InvokeAsync<object>(layerNamespace + ".init", Id, thisRef);
        }

        #endregion Constructors

        #region Methods

        private async Task AddItems(int start, int count)
        {
            await JSRuntime.InvokeAsync<object>($"{layerNamespace}.addItems", Id, _items.Skip(start).Take(count));
        }

        private async void BeforeRemoveRange(object sender, IEnumerable<BaseBingMapEntity> e)
        {
            await JSRuntime.InvokeAsync<object>($"{layerNamespace}.removeItems", e);
        }

        private void ItemRemoved(object sender, BaseBingMapEntity e)
        {
            JSRuntime.InvokeAsync<object>($"{layerNamespace}.removeItems", Id, e.Id);
        }

        private void ItemsChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    JSRuntime.InvokeAsync<object>($"{layerNamespace}.addItem", Id, _items[e.NewIndex]);
                    break;

                case ListChangedType.ItemChanged:
                    JSRuntime.InvokeAsync<object>($"{layerNamespace}.updateItem", Id, _items[e.NewIndex]);
                    break;
            }
        }

        private void ListRangeChanged(object sender, RangeChangdEventArgs e)
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

        public void Add(BaseBingMapEntity item)
        {
            _items.Add(item);
        }

        public void AddRange(IEnumerable<BaseBingMapEntity> items)
        {
            _items.AddRange(items);
        }

        public override void Dispose()
        {
            thisRef.Dispose();
        }

        public IEnumerator<BaseBingMapEntity> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public void Remove(BaseBingMapEntity item)
        {
            _items.Remove(item);
        }

        public void RemoveRange(int start, int ammount)
        {
            _items.RemoveRange(start, ammount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion Methods
    }
}
