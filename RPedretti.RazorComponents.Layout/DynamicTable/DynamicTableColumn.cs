using Microsoft.AspNetCore.Components;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public class DynamicTableColumn<T>
    {
        #region Properties

        public string Classes { get; set; }
        public string SortProp { get; set; }
        public RenderFragment<T> Template { get; set; }

        #endregion Properties
    }
}
