﻿using Microsoft.JSInterop;
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
        private DotNetObjectReference<BingMapLayer> thisRef;

        #endregion Fields

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
            switch (e.Type)
            {
                case RangeChangeType.Add:
                    AddItems(e.StartIndex, e.Ammount);
                    break;
            }
        }

        #endregion Methods

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

        public void Add(BaseBingMapEntity item)
        {
            _items.Add(item);
        }

        public void AddRange(IEnumerable<BaseBingMapEntity> items)
        {
            _items.AddRange(items);
        }

        public void RemoveRange(int start, int ammount)
        {
            _items.RemoveRange(start, ammount);
        }

        public override void Dispose()
        {
            thisRef.Dispose();
        }

        public void Remove(BaseBingMapEntity item)
        {
            _items.Remove(item);
        }

        public IEnumerator<BaseBingMapEntity> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
