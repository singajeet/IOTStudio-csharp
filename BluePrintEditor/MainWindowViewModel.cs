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
		private HamburgerMenuItem _HamburgerMenuSelectedItem;
		private string _ContentLabel;
		private string _StatusBarAppStatus = "Ready!";
		private string _StatusBarMessage = "Welcome to BluePrint Editor";
		
		public MainWindowViewModel()
		{
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
			get { 
				return _StatusBarAppStatus;
			}
		}
			
		public string StatusBarMessage{
			get{ 
				return _StatusBarMessage;
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
