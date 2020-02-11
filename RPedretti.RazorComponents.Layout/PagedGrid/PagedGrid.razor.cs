using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Layout.Pager;
using System.Collections.Generic;

namespace RPedretti.RazorComponents.Layout.PagedGrid
{
    public partial class PagedGrid<TItem>
    {
        #region Properties

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public int CurrentPage { get; set; }
        [Parameter] public bool HasContent { get; set; }
        [Parameter] public string ItemClasses { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public RenderFragment<PagedGridContext> ItemTemplate { get; set; }
        [Parameter] public string ListClasses { get; set; }
        [Parameter] public bool Loading { get; set; }
        [Parameter] public RenderFragment LoadingTemplate { get; set; }
        [Parameter] public int MaxIndicators { get; set; }
        [Parameter] public string NoContentMessage { get; set; } = "No content";
        [Parameter] public RenderFragment NoContentTemplate { get; set; }
        [Parameter] public EventCallback<int> OnRequestPage { get; set; }
        [Parameter] public int PageCount { get; set; }
        [Parameter] public PagerPosition PagerPosition { get; set; } = PagerPosition.CENTER;
        [Parameter] public bool SmallPager { get; set; }

        #endregion Properties

        #region Classes

        public class PagedGridContext
        {
            #region Properties

            public int Index { get; set; }
            public TItem Item { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}
