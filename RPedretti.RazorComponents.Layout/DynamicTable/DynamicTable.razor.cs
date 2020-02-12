using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public partial class DynamicTable<TItem>
    {
        #region Fields

        protected readonly Dictionary<DynamicTableHeader, bool> SortedTable = new Dictionary<DynamicTableHeader, bool>();

        #endregion Fields

        #region Properties

        protected DynamicTableHeader CurrentOrdered { get; set; }
        [Parameter] public string Classes { get; set; }
        [Parameter] public IEnumerable<DynamicTableColumn<TItem>> Columns { get; set; } = new DynamicTableColumn<TItem>[] { };
        [Parameter] public IEnumerable<DynamicTableHeader> Headers { get; set; }
        [Parameter] public bool Loading { get; set; }
        [Parameter] public RenderFragment LoadingTemplate { get; set; }
        [Parameter] public Func<DynamicTableRow<TItem>, Task> OnRowClick { get; set; }
        [Parameter] public IEnumerable<DynamicTableRow<TItem>> Rows { get; set; } = new DynamicTableRow<TItem>[] { };
        [Parameter] public Func<string, bool, Task> SortRequest { get; set; }

        #endregion Properties

        #region Methods

        protected void Sort(DynamicTableHeader header)
        {
            if (CurrentOrdered != null && CurrentOrdered != header)
            {
                SortedTable.Remove(CurrentOrdered);
            }

            CurrentOrdered = header;

            if (!SortedTable.ContainsKey(CurrentOrdered))
            {
                SortedTable[CurrentOrdered] = true;
            }

            var isAsc = SortedTable[CurrentOrdered];

            SortRequest?.Invoke(header.SortId, isAsc);

            SortedTable[CurrentOrdered] = !SortedTable[CurrentOrdered];
        }

        protected Task SortHeader(DynamicTableHeader header)
        {
            if (header.CanSort) Sort(header);
            return Task.CompletedTask;
        }

        #endregion Methods
    }
}
