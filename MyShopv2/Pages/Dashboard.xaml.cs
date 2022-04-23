using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
namespace MyShopv2.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : INotifyPropertyChanged
    {
        public SeriesCollection SeriesCollection { get; set; } //Loai san pham ben phai

        public SeriesCollection SeriesCollections { get; set; } //Loai san pham ben trai
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var observable in series.Values.Cast<ObservableValue>())
                {
                    observable.Value = r.Next(0, 10);
                }
            }
        }
        public Dashboard()
        {
            InitializeComponent();
            //ben phai
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(3),
                        new ObservableValue(5),
                        new ObservableValue(2),
                        new ObservableValue(7),
                        new ObservableValue(7),
                        new ObservableValue(4)
                    },
                    PointGeometrySize = 0,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent
                },
                new LineSeries
                {
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(3),
                        new ObservableValue(4),
                        new ObservableValue(6),
                        new ObservableValue(8),
                        new ObservableValue(7),
                        new ObservableValue(5)
                    },
                    PointGeometrySize = 0,
                    StrokeThickness = 4,
                    Fill = Brushes.Transparent
                }
            };

            //ben trai
            SeriesCollections = new SeriesCollection()
            {
                new ColumnSeries
                {
                    Title="2015",
                    Values=new ChartValues<double> {10,50,34,12}
                },
                new ColumnSeries
                {
                    Title="2016",
                    Values=new ChartValues<double> {20,90,34,18}
                },
                new ColumnSeries
                {
                    Title="2017",
                    Values=new ChartValues<double> {30,20,59,45}
                }


            };

            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
