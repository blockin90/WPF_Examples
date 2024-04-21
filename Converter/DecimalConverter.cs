using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Converter
{
    internal class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string) {
                var strValue = value as string;
                strValue = strValue.Replace(',', '.');
                if (decimal.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result)) {
                    return result;
                } else {
                    //какая то логика, если передали не строку
                    return default(decimal);
                }
            } else {
                throw new ArgumentException();
            }
        }
    }
}
