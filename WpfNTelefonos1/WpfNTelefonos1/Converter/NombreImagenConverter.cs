using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfNTelefonos1.Converter
{
    public class NombreImagenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Nos permite transformar un objeto de una tipo específico a uno nuevo. En este caso lo transformamos en un Bitmapimage
            BitmapImage b = new BitmapImage();
            b.BeginInit();

            b.UriSource = new Uri(string.Format("..\\..\\Assets\\{0}", (string)value), UriKind.RelativeOrAbsolute);
            b.EndInit();

            return b;
            
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
