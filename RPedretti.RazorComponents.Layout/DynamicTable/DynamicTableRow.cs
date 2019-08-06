using System.Collections.Generic;

namespace RPedretti.RazorComponents.Layout.DynamicTable
{
    public class DynamicTableRow<T>
    {
        #region Properties

        public string Classes { get; set; }
        public T Context { get; set; }

        #endregion Properties
    }
}
