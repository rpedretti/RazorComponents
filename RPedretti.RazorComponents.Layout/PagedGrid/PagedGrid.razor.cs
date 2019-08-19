using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Layout.Pager;
using RPedretti.RazorComponents.Shared.Components;

namespace RPedretti.RazorComponents.Layout.PagedGrid
{
    public class PagedGridBase : BaseAccessibleComponent
    {
        #region Properties

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public int CurrentPage { get; set; }
        [Parameter] public bool HasContent { get; set; }
        [Parameter] public bool Loading { get; set; }
        [Parameter] public int MaxIndicators { get; set; }
        [Parameter] public string NoContentMessage { get; set; } = "No content";
        [Parameter] public EventCallback<int> OnRequestPage { get; set; }
        [Parameter] public int PageCount { get; set; }
        [Parameter] public PagerPosition PagerPosition { get; set; } = PagerPosition.CENTER;
        [Parameter] public bool SmallPager { get; set; }

        #endregion Properties
    }
}
