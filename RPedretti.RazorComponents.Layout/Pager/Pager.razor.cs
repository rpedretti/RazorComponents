using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Layout.Pager
{
    public enum PagerPosition
    {
        START,
        END,
        CENTER
    }

    public partial class Pager
    {
        #region Fields

        private int _currentPage;

        private bool _initialized;

        private int _maxIndicators = 5;

        private int _pageCount;

        #endregion Fields

        #region Properties

        private int _totalPaginationPages { get; set; }

        protected int IndicatorCount { get; set; }

        protected Indicator[] Indicators { get; set; }

        protected bool ShowFirst { get; set; } = false;

        protected bool ShowLast { get; set; } = false;

        [Parameter]
        public int CurrentPage
        {
            get => _currentPage;
            set => SetParameter(ref _currentPage, value, UpdatePagerCount);
        }

        [Parameter]
        public int MaxIndicators
        {
            get => _maxIndicators;
            set => SetParameter(ref _maxIndicators, value, UpdatePagerCount);
        }

        [Parameter] public EventCallback<int> OnRequestPage { get; set; }

        [Parameter]
        public int PageCount
        {
            get => _pageCount;
            set => SetParameter(ref _pageCount, value, UpdatePagerCount);
        }

        [Parameter] public PagerPosition Position { get; set; } = PagerPosition.CENTER;

        [Parameter] public bool Small { get; set; }

        #endregion Properties

        #region Methods

        private void UpdatePagerCount()
        {
            if (_initialized)
            {
                if (PageCount > MaxIndicators)
                {
                    var start = MaxIndicators * ((CurrentPage - 1) / MaxIndicators) + 1;
                    var limit = Math.Min(PageCount - start + 1, MaxIndicators);

                    ShowFirst = CurrentPage > MaxIndicators;
                    ShowLast = Math.Ceiling(CurrentPage / (double)MaxIndicators) < _totalPaginationPages;

                    for (int i = 0; i < limit; i++)
                    {
                        var page = i + start;
                        Indicators[i].Visible = true;
                        Indicators[i].Active = CurrentPage == page;
                        Indicators[i].Content = page.ToString();
                        Indicators[i].Page = page;
                    }

                    if (limit < MaxIndicators)
                    {
                        for (int i = limit; i < MaxIndicators; i++)
                        {
                            Indicators[i].Visible = false;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < IndicatorCount; i++)
                    {
                        Indicators[i].Active = CurrentPage == i + 1;
                    }
                }
            }
        }

        protected async Task FirstPage()
        {
            await OnRequestPage.InvokeAsync(1);
        }

        protected async Task LastPage()
        {
            await OnRequestPage.InvokeAsync(PageCount);
        }

        protected async Task NextPage()
        {
            await OnRequestPage.InvokeAsync(CurrentPage + 1);
        }

        protected async Task NextPagination()
        {
            var ammount = (CurrentPage - 1) % MaxIndicators;
            await OnRequestPage.InvokeAsync(CurrentPage + MaxIndicators - ammount);
        }

        protected override void OnInitialized()
        {
            IndicatorCount = Math.Min(PageCount, MaxIndicators);
            Indicators = new Indicator[IndicatorCount];
            for (int i = 0; i < IndicatorCount; i++)
            {
                var page = i + 1;
                Indicators[i] = new Indicator
                {
                    Active = CurrentPage == page,
                    Visible = true,
                    Content = page.ToString(),
                    Page = page
                };
            }

            _totalPaginationPages = (int)Math.Ceiling(PageCount / (double)MaxIndicators);
            _initialized = true;
            UpdatePagerCount();
        }

        protected async Task PreviousPage()
        {
            await OnRequestPage.InvokeAsync(CurrentPage - 1);
        }

        protected async Task PreviousPagination()
        {
            var ammount = (CurrentPage - 1) % MaxIndicators;
            await OnRequestPage.InvokeAsync(CurrentPage - MaxIndicators - ammount);
        }

        #endregion Methods

        #region Classes

        protected class Indicator
        {
            #region Properties

            public bool Active { get; set; }
            public string Content { get; set; }
            public int Page { get; set; }
            public bool Visible { get; set; }

            #endregion Properties
        }

        #endregion Classes
    }
}
