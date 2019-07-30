using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using RPedretti.RazorComponents.Shared.Components;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public class DynamicGroupedTableBase : BaseComponent
    {
        #region Properties

        [Parameter] protected string Classes { get; set; }

        [Parameter] protected List<DynamicTableHeader> Headers { get; set; }
        [Parameter] protected bool Loading { get; set; }

        #endregion Properties
    }
}
