using Business;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Workshop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new ImdbClient();

            var result = new ObservableCollection<Movie>();

            Movies.ItemsSource = result;

            await foreach(var movies in client.FindAllAsync(Search.Text))
            {
                foreach(var movie in movies)
                {
                    result.Add(movie);
                }
            }
        }

        private async void Movies_SelectionChanged(object sender, 
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var movie = e.AddedItems[0] as Movie;

            var client = new ImdbClient();

            var result = await client.DetailsAsync(movie.ImdbID);

            Details.DataContext = result;
        }
    }

    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dc = (System.Drawing.Color)value;
            return new SolidColorBrush(new Color { A = dc.A, R = dc.R, G = dc.G, B = dc.B });
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
