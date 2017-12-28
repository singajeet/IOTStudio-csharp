/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/27/2017
 * Time: 23:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Data;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of ConverterIntToString.
	/// </summary>
	public class ConverterIntToString : IValueConverter
	{
		public ConverterIntToString()
		{
		}

		#region IValueConverter implementation

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			try{
				return value.ToString();
			} catch (Exception ex) {
				return string.Empty;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{	
			try{
				return int.Parse(value.ToString());
			} catch (Exception ex) {
				return 0;
			}
		}

		#endregion
	}
}
