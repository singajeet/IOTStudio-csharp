/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 11:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MahApps.Metro.Controls;

namespace BluePrintEditor
{
	/// <summary>
	/// Description of MainWindowViewModel.
	/// </summary>
	public class MainWindowViewModel: INotifyPropertyChanged, IDataErrorInfo, IDisposable
	{
		private static MainWindowViewModel _Instance;
		private HamburgerMenuItem _HamburgerMenuSelectedItem;
		private bool _ToolsBoxFlyoutIsOpen;
		private string _ContentLabel;
		private string _StatusBarAppStatus = "Ready!";
		private string _StatusBarMessage = "Welcome to BluePrint Editor";
		
		private MainWindowViewModel()
		{
		}
		
		public static MainWindowViewModel Instance{
			get{ 
				if (_Instance == null)
					_Instance = new MainWindowViewModel();
				
				return _Instance;
			}
		}
		
		
		
		public string ContentLabel{
			get { return this._ContentLabel; }
			set { this._ContentLabel = value; 
				OnPropertyChanged();
			}
		}

		public HamburgerMenuItem HamburgerMenuSelectedItem{
			get { return this._HamburgerMenuSelectedItem; }
			set { this._HamburgerMenuSelectedItem = value;
				ContentLabel = ((HamburgerMenuIconItem)this._HamburgerMenuSelectedItem).Label;
				OnPropertyChanged();
			}
		}
		
		public string StatusBarAppStatus {
			get { return _StatusBarAppStatus; }
			set { _StatusBarAppStatus = value; 
				OnPropertyChanged();
			}
		}
			
		public string StatusBarMessage{
			get{ return _StatusBarMessage; }
			set { _StatusBarMessage = value; 
				OnPropertyChanged();
			}
		}
		
		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		#endregion
		#region IDataErrorInfo implementation
		public string this[string columnName] {
			get {
				throw new NotImplementedException();
			}
		}
		public string Error {
			get {
				throw new NotImplementedException();
			}
		}
		#endregion
		#region IDisposable implementation

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
