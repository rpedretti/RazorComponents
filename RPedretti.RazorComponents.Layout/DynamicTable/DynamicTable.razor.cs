using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Shared.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public class DynamicTableBase : BaseComponent
    {
        #region Fields

        protected readonly Dictionary<DynamicTableHeader, bool> SortedTable = new Dictionary<DynamicTableHeader, bool>();

        #endregion Fields

        #region Properties

        protected DynamicTableHeader CurrentOrdered { get; set; }
        [Parameter] public string Classes { get; set; }
        [Parameter] public IEnumerable<DynamicTableHeader> Headers { get; set; }
        [Parameter] public bool Loading { get; set; }
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

        #endregion Methods
    }
}
