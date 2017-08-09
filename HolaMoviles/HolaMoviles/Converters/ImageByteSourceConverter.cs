using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace HolaMoviles.Converters
{
	public class ImageByteSourceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			byte[] bytes = value as byte[];
			if (bytes == null)
			{
				return null;
			}
			return ImageSource.FromStream(() => new MemoryStream(bytes));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
