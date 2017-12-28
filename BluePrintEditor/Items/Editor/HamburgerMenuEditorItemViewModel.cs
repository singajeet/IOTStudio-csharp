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
using System.Windows;
using System.Windows.Controls;
using BluePrintEditor.Utilities;
using log4net;
using MahApps.Metro.Controls;

namespace BluePrintEditor.Items.Editor
{
	/// <summary>
	/// Description of HamburgerMenuEditorItemViewModel.
	/// </summary>
	public class HamburgerMenuEditorItemViewModel: INotifyPropertyChanged, IDisposable
	{
		ILog Logger = Log.Get(typeof(HamburgerMenuEditorItemViewModel));
		
		public HamburgerMenuEditorItemViewModel()
		{
		}
		
		

		#region INotifyPropertyChanged implementation
		protected void OnPropertyChanged(object value, [CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
			
			Logger.DebugF("MainWindowViewModel property [{0}] value changed to [{1}]", memberName, value);
		}
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		#region IDisposable implementation

		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		#endregion
	}
}
