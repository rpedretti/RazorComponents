using Microsoft.AspNetCore.Components;
using System;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public class DynamicTableColumn<T>
    {
        #region Properties

        public string Classes { get; set; }
        public RenderFragment<T> Template { get; set; }
        public string SortProp { get; set; }

        #endregion Properties
    }
}
