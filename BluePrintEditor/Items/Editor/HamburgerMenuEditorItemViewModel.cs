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
		
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		public HamburgerMenuEditorItemViewModel()
		{
			Id = Guid.NewGuid();
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
			//throw new NotImplementedException();
		}

		#endregion
		
		public override string ToString()
		{
			return string.Format("[HamburgerMenuEditorItemViewModel Id={0}]", id);
		}

	}
}
