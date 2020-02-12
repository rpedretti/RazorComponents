using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public partial class DynamicGroupedTable<TItem>
    {
        #region Properties

        [Parameter] public string Classes { get; set; }
        [Parameter] public IEnumerable<DynamicTableColumn<TItem>> Columns { get; set; } = new DynamicTableColumn<TItem>[] { };
        [Parameter] public RenderFragment<DynamicTableGroup<TItem>> GroupHeaderTemplate { get; set; }
        [Parameter] public IEnumerable<DynamicTableGroup<TItem>> Groups { get; set; } = new List<DynamicTableGroup<TItem>>();
        [Parameter] public IEnumerable<DynamicTableHeader> Headers { get; set; }
        [Parameter] public bool Loading { get; set; }
        [Parameter] public RenderFragment LoadingTemplate { get; set; }

        #endregion Properties

        #region Methods

        protected Task ToggleGroupCollapsed(DynamicTableGroup<TItem> group)
        {
            group.Collapsed = !group.Collapsed;
            return Task.CompletedTask;
        }

        #endregion Methods
    }
}
