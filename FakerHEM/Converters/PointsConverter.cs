using FakerHEM.ViewModels;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace FakerHEM.Converters
{
    public class PointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<PointViewModel> points)
            {
                var pointCollection = new PointCollection();
                foreach (var point in points)
                {
                    pointCollection.Add(new System.Windows.Point(point.ScaledX, point.ScaledY));
                }
                return pointCollection;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}