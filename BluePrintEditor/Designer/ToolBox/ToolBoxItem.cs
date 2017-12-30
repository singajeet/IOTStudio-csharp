/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 13:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BluePrintEditor.Utilities;
using log4net;
using MahApps.Metro.IconPacks;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Description of ToolBoxItem.
	/// </summary>
	public class ToolBoxItem : INotifyPropertyChanged
	{
		ILog Logger = Log.Get(typeof(ToolBoxItem));
		
		string name;
		
		public string Name {
			get { return name; }
			set { 
					name = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		string toolTitle;
		
		public string ToolTitle {
			get { return toolTitle; }
			set { 
					toolTitle = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		Type toolCommandType;
		
		public Type ToolCommandType {
			get { return toolCommandType; }
			set { 
					toolCommandType = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		string iconUri;
		
		public string IconUri {
			get { return iconUri; }
			set { 
					iconUri = value; 
					OnPropertyChanged();
					Logger.PropertyChanged(value);
			}
		}
		
		PackIconModernKind iconKind;
		
		public PackIconModernKind IconKind {
			get { return iconKind; }
			set { 
					iconKind = value; 
					OnPropertyChanged();
			}
		}
		
		public ToolBoxItem()
		{
			Logger.InstanceCreated();
		}

		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
