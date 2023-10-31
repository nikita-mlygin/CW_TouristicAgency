using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NCourseWork.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility), ParameterType = typeof(bool))]
    internal class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible && !isVisible)
            {
                return Visibility.Visible;
            }

            if (parameter is bool useHidden && useHidden)
            {
                return Visibility.Hidden;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
