using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Layout.Pager;
using System;
using RPedretti.RazorComponents.Shared.Components;

namespace RPedretti.RazorComponents.Layout.PagedGrid
{
    public class PagedGridBase : BaseAccessibleComponent
    {
        #region Properties

        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected int CurrentPage { get; set; }
        [Parameter] protected bool HasContent { get; set; }
        [Parameter] protected bool Loading { get; set; }
        [Parameter] protected int MaxIndicators { get; set; }
        [Parameter] protected string NoContentMessage { get; set; } = "No content";
        [Parameter] protected EventCallback<int> OnRequestPage { get; set; }
        [Parameter] protected int PageCount { get; set; }
        [Parameter] protected PagerPosition PagerPosition { get; set; } = PagerPosition.CENTER;
        [Parameter] protected bool SmallPager { get; set; }

        #endregion Properties
    }
}
