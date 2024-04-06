using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeursfuifChiroZevendonk.ViewModels
{
    public partial class BeursPageViewModel : BaseViewModel
    {
        public ObservableCollection<Drink> Drinks => _drinksService.Drinks;
        public ObservableCollection<ISeries> Series { get; set; } = new ObservableCollection<ISeries>();

        private readonly DrinksDataService _drinksService;
        public IEnumerable<Axis> XAxes { get; set; }
        public IEnumerable<Axis> YAxes { get; set; }


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
            TextSize = 16
        }
            };

            var now = DateTime.Now;
            var labels = new List<string>();
            for (int i = 11; i >= 0; i--)
            {
                labels.Add(now.AddMinutes(-5 * i).ToString("HH:mm"));
            }
            labels.Add(now.ToString("HH:mm"));

            XAxes = new Axis[]
            {
        new Axis
        {
            Labels = labels.ToArray(),
            LabelsRotation = 15
        }
            };
        }

        public void UpdateChart()
        {
            InitializeAxes();
            var series = new List<ISeries>();

            foreach (var drink in Drinks)
            {
                var lastHourPrices = drink.HistoricalPrices.Skip(Math.Max(0, drink.HistoricalPrices.Count - 12)).ToList();
                if (!lastHourPrices.Contains(drink.CurrentPrice))
                {
                    lastHourPrices.Add(drink.CurrentPrice);
                }
                var lineSeries = new LineSeries<decimal>
                {
                    Values = lastHourPrices.ToArray(),
                    Name = drink.Name,
                    Stroke = new SolidColorPaint(SKColor.Parse(drink.DrinkColorHex)) { StrokeThickness = 2 },
                    Fill = new SolidColorPaint(SKColors.Transparent),
                    GeometrySize = 0,
                    LineSmoothness = 0.5
                };

                series.Add(lineSeries);
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
            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }

    }
}
