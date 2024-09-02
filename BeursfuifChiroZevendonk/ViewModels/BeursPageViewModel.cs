using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class BeursPageViewModel : BaseViewModel
    {
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;
        public ObservableCollection<ISeries> Series { get; set; } = new ObservableCollection<ISeries>();

        private readonly DrinksDataService _drinksService;
        public IEnumerable<Axis> YAxes { get; set; }
        public IEnumerable<Axis> XAxes { get; set; }

        public BeursPageViewModel(DrinksDataService drinksService)
        {
            base.Title = "Beurs";
            _drinksService = drinksService;
            MessagingCenter.Subscribe<App>((App)Application.Current, "PricesUpdated", (sender) => {
                UpdateChart();
            });
            UpdateChart();
        }

        private void InitializeAxes()
        {
            decimal highestMaxPrice = Drinks.Max(drink => drink.MaxPrice) + 0.25m;
            decimal lowestMinPrice = Drinks.Min(drink => drink.MinPrice) - 0.25m;

            YAxes = new Axis[]
            {
        new Axis
        {
            MinLimit = (double)lowestMinPrice,
            MaxLimit = (double)highestMaxPrice,
            ShowSeparatorLines = false,
            LabelsPaint = new SolidColorPaint(SKColors.White),
            TextSize = 20
        }
            };
            XAxes = new Axis[]
{
        new Axis
        {
            IsVisible = false,
        }
};

        }


        public void UpdateChart()
        {
            InitializeAxes();
            var series = new List<ISeries>();

            foreach (var drink in Drinks)
            {
                var columnSeries = new ColumnSeries<decimal>
                {
                    Values = new decimal[] { drink.CurrentPrice },
                    Name = drink.Name,
                    Fill = new SolidColorPaint(SKColor.Parse(drink.DrinkColorHex)),
                    MaxBarWidth = double.NaN, 
                    Padding = 30
                };

                series.Add(columnSeries);
            }

            Series.Clear();
            foreach (var serie in series)
            {
                Series.Add(serie);
            }

            RefreshChart();
        }

        private void RefreshChart()
        {
            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(YAxes));
            OnPropertyChanged(nameof(XAxes));
        }
    }
}
