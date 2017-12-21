/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 21:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace BluePrintEditor.Items.Editor
{
	/// <summary>
	/// Description of HamburgerMenuEditorItemViewModel.
	/// </summary>
	public class HamburgerMenuEditorItemViewModel: INotifyPropertyChanged, IDisposable
	{
		private ListBoxItem _MenuSelectedItem;
		private bool _ToolsFlyoutIsOpen;
		public HamburgerMenuEditorItemViewModel()
		{
		}
		
		public ListBoxItem MenuSelectedItem{
			get{ return _MenuSelectedItem; }
			set{ _MenuSelectedItem = value; 
				OpenFlyout();
				OnPropertyChanged();
			}
		}
		
		public bool ToolsFlyoutIsOpen{
			get{ return _ToolsFlyoutIsOpen; }
			set{ _ToolsFlyoutIsOpen = value; 
				OnPropertyChanged();
			}
		}
		
		private void OpenFlyout()
		{
			if (MenuSelectedItem.Name.Equals("SelectionTool")) {
				if (ToolsFlyoutIsOpen == false)
					ToolsFlyoutIsOpen = true;
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
		#region IDisposable implementation

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
