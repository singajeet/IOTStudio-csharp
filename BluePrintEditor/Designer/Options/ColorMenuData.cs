/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/27/2017
 * Time: 17:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Media;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Designer.Options
{
	/// <summary>
	/// Description of ColorMenuData.
	/// </summary>
	public class ColorMenuData
	{
		ILog Logger = Log.Get(typeof(ColorMenuData));
		
		private SolidColorBrush solidBrush;
		private Brush colorBrush;
		
		public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        
		public Brush ColorBrush { 
        	get { 
				if (colorBrush == null)
					return Brushes.White;
				
				return colorBrush;
        	}
        	set { 
				colorBrush = value;
				Logger.PropertyChanged(value);
        	}
        }
        
        public SolidColorBrush SolidBrush {
			get { 
				if (solidBrush == null) {
					if (ColorBrush != null)
						return (SolidColorBrush)ColorBrush;
					else
						return new SolidColorBrush(Colors.White);
        		}
				return solidBrush;
        	}
			set {
				solidBrush = value;
				Logger.PropertyChanged(value);
			}
        }
        
		public ColorMenuData()
		{
		}
	}
}
