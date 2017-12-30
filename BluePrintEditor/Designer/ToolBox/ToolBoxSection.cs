/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 12:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using BluePrintEditor.Utilities;
using log4net;


namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Description of ToolBoxSection.
	/// </summary>
	public class ToolBoxSection : INotifyPropertyChanged
	{
		ILog Logger = Log.Get(typeof(ToolBoxSection));
		
		string sectionName;
		
		public string SectionName {
			get { return sectionName; }
			set { 
					sectionName = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		ToolBoxItems toolBoxItems;
		
		public ToolBoxItems ToolBoxItems {
			get { return toolBoxItems; }
			set { 
					toolBoxItems = value;
					OnPropertyChanged();
					Logger.PropertyChanged(value);
			}
		}
		
		public ToolBoxSection()
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
