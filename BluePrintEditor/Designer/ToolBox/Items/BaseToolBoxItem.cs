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
using BluePrintEditor.Designer.ToolBox.Interfaces;
using BluePrintEditor.Utilities;
using log4net;
using MahApps.Metro.IconPacks;

/**********************************************
 * Sample ToolBoxItem
 * 		Uncomment the "IToolItem" interface
 * 		if you are going to use this sample
 * 		code
 * ********************************************/
namespace BluePrintEditor.Designer.ToolBox.Items
{
	/// <summary>
	/// Description of ToolBoxItem.
	/// </summary>
	public class BaseToolBoxItem : IToolItem, INotifyPropertyChanged 
	{
		ILog Logger = Log.Get(typeof(BaseToolBoxItem));
		
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
					OnPropertyChanged();
			}
		}
		
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
		
		string description;
		
		public string Description { 
			get { return description; } 
			set { description = value; 
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
		
		string category;
		
		public string Category {
			get { return category; }
			set { 
					category = value; 
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
		
		public BaseToolBoxItem()
		{
			Logger.InstanceCreated();
			Id = Guid.NewGuid();
		}

		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		
		public override string ToString()
		{
			return string.Format("[BaseToolBoxItem Id={0}, Name={1}, ToolCommandType={2}]", id, name, toolCommandType);
		}

	}
}
