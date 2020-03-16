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

        private IEnumerable<DynamicTableColumn<WeatherForecast>> _columns { get; set; } = new DynamicTableColumn<WeatherForecast>[0];

        private IEnumerable<DynamicTableRow<WeatherForecast>> _forecasts { get; set; } = new DynamicTableRow<WeatherForecast>[0];

        private IEnumerable<DynamicTableGroup<WeatherForecast>> _groupedForecast { get; set; } = new DynamicTableGroup<WeatherForecast>[0];

        private IEnumerable<DynamicTableHeader> _headers { get; set; } = new DynamicTableHeader[0];

        private RenderFragment<WeatherForecast> _dateTemplate;
        private RenderFragment<WeatherForecast> _tempTemplate;
        private RenderFragment<WeatherForecast> _rainChanceTemplate;
        private RenderFragment<WeatherForecast> _rainAmmountTemplate;
        private RenderFragment<DynamicTableGroup<WeatherForecast>> _groupHeaderTemplate;

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

        #region Methods

        private async Task GetForecastAsync()
        {
            Loading = true;

            var forecasts = await _forecastService.GetForecastAsync();

            _forecasts = forecasts.Select(f => new DynamicTableRow<WeatherForecast> { Context = f }).ToList();

            _groupedForecast = forecasts.GroupBy(f => f.Date.Date).Select(g =>
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

            var column = _columns.ElementAt(int.Parse(sortId));

            if (isAsc)
            {
                _forecasts = column.SortProp switch
                {
                    nameof(WeatherForecast.Date) => _forecasts.OrderBy(r => r.Context.Date).ToList(),
                    nameof(WeatherForecast.RainAmmount) => _forecasts.OrderBy(r => r.Context.RainAmmount).ToList(),
                    nameof(WeatherForecast.RainChangePercent) => _forecasts.OrderBy(r => r.Context.RainChangePercent).ToList(),
                    nameof(WeatherForecast.Temperature) => _forecasts.OrderBy(r => r.Context.Temperature).ToList(),
                    _ => throw new ArgumentException($"invalid property", column.SortProp)
                };
            }
            else
            {
                _forecasts = column.SortProp switch
                {
                    nameof(WeatherForecast.Date) => _forecasts.OrderByDescending(r => r.Context.Date).ToList(),
                    nameof(WeatherForecast.RainAmmount) => _forecasts.OrderByDescending(r => r.Context.RainAmmount).ToList(),
                    nameof(WeatherForecast.RainChangePercent) => _forecasts.OrderByDescending(r => r.Context.RainChangePercent).ToList(),
                    nameof(WeatherForecast.Temperature) => _forecasts.OrderByDescending(r => r.Context.Temperature).ToList(),
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
