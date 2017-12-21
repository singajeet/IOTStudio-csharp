/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 6:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace BluePrintEditor.Items
{
	/// <summary>
	/// Description of HamburgerMenuCustomBaseItem.
	/// </summary>
	public class HamburgerMenuCustomBaseItem : HamburgerMenuIconItem, INotifyPropertyChanged
	{
		protected UserControl _View;
		
		public HamburgerMenuCustomBaseItem()
		{	
		}
		
		public UserControl View{
			get { return this._View; }
			set { this._View = value; 
				OnPropertyChanged();
			}
		}

		#region INotifyPropertyChanged implementation

		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
