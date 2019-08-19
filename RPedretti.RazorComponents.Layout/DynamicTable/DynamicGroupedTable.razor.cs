using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Shared.Components;
using System.Collections.Generic;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public class DynamicGroupedTableBase : BaseComponent
    {
        #region Properties

        [Parameter] public string Classes { get; set; }

        [Parameter] public IEnumerable<DynamicTableHeader> Headers { get; set; }
        [Parameter] public bool Loading { get; set; }

        #endregion Properties
    }
}
