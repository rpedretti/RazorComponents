using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Layout.DynamicTable;
using RPedretti.RazorComponents.Sample.Shared.Data;
using RPedretti.RazorComponents.Sample.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Forecast
{
    public partial class ForecastPage
    {
        #region Fields

        private bool _grouped;

        private bool _loading;

        #endregion Fields

        #region Properties

        [Inject] private IForecastService _forecastService { get; set; }

        protected IEnumerable<DynamicTableColumn<WeatherForecast>> Columns { get; set; } = new DynamicTableColumn<WeatherForecast>[0];

        protected IEnumerable<DynamicTableRow<WeatherForecast>> Forecasts { get; set; } = new DynamicTableRow<WeatherForecast>[0];

        protected IEnumerable<DynamicTableGroup<WeatherForecast>> GroupedForecast { get; set; } = new DynamicTableGroup<WeatherForecast>[0];

        protected IEnumerable<DynamicTableHeader> Headers { get; set; } = new DynamicTableHeader[0];

        protected bool Loading
        {
            get => _loading;
            set => SetParameter(ref _loading, value, StateHasChanged);
        }

        public bool Grouped
        {
            get => _grouped;
            set => SetParameter(ref _grouped, value, StateHasChanged);
        }

        #endregion Properties

        #region Constructors

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public ForecastPage() => Headers = new List<DynamicTableHeader>()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            {
                new DynamicTableHeader{ Title =  "Date", Classes = "-l"},
                new DynamicTableHeader{ Title =  "Temp. (C)", CanSort = true, Classes = "-l", SortId = "1" },
                new DynamicTableHeader{ Title =  "Rain chance (%)", CanSort = true, Classes = "-r", SortId = "2" },
                new DynamicTableHeader{ Title =  "Rain Ammount (mm)", CanSort = true, Classes = "-r", SortId = "3" }
            };

        #endregion Constructors

        #region Methods

        private async Task GetForecastAsync()
        {
            Loading = true;

            var forecasts = await _forecastService.GetForecastAsync();

            Forecasts = forecasts.Select(f => new DynamicTableRow<WeatherForecast> { Context = f }).ToList();

            GroupedForecast = forecasts.GroupBy(f => f.Date.Date).Select(g =>
            {
                return new DynamicTableGroup<WeatherForecast>
                {
                    Rows = g.Select(f => new DynamicTableRow<WeatherForecast> { Context = f }).ToList()
                };
            });

            Loading = false;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetForecastAsync();
        }

        protected Task RowClicked(DynamicTableRow<WeatherForecast> row)
        {
            Console.WriteLine(row.Context.Date);
            return Task.CompletedTask;
        }

        protected async Task Sort(string sortId, bool isAsc)
        {
            Loading = true;

            await Task.Delay(1500);

            var column = Columns.ElementAt(int.Parse(sortId));

            if (isAsc)
            {
                Forecasts = column.SortProp switch
                {
                    nameof(WeatherForecast.Date) => Forecasts.OrderBy(r => r.Context.Date).ToList(),
                    nameof(WeatherForecast.RainAmmount) => Forecasts.OrderBy(r => r.Context.RainAmmount).ToList(),
                    nameof(WeatherForecast.RainChangePercent) => Forecasts.OrderBy(r => r.Context.RainChangePercent).ToList(),
                    nameof(WeatherForecast.Temperature) => Forecasts.OrderBy(r => r.Context.Temperature).ToList(),
                    _ => throw new ArgumentException($"invalid property", column.SortProp)
                };
            }
            else
            {
                Forecasts = column.SortProp switch
                {
                    nameof(WeatherForecast.Date) => Forecasts.OrderByDescending(r => r.Context.Date).ToList(),
                    nameof(WeatherForecast.RainAmmount) => Forecasts.OrderByDescending(r => r.Context.RainAmmount).ToList(),
                    nameof(WeatherForecast.RainChangePercent) => Forecasts.OrderByDescending(r => r.Context.RainChangePercent).ToList(),
                    nameof(WeatherForecast.Temperature) => Forecasts.OrderByDescending(r => r.Context.Temperature).ToList(),
                    _ => throw new ArgumentException($"invalid property", column.SortProp)
                };
            }

            Loading = false;
        }

        public void ToggleColumn(DynamicTableHeader header)
        {
            header.Hidden = !header.Hidden;
        }

        #endregion Methods
    }
}
