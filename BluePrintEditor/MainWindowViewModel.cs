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
using System.Windows;
using BluePrintEditor.Utilities;
using log4net;
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
		private Visibility _ProgressBarVisibility = Visibility.Hidden;
		private double _ProgressBarValue = 100;
		
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
					OnPropertyChanged();
			}
		}
		
		ILog Logger = Log.Get(typeof(MainWindowViewModel));
		
		private MainWindowViewModel()
		{
			Logger.InstanceCreated();
			Id = Guid.NewGuid();
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
				Logger.PropertyChanged(value);
			}
		}

		public HamburgerMenuItem HamburgerMenuSelectedItem{
			get { return this._HamburgerMenuSelectedItem; }
			set { this._HamburgerMenuSelectedItem = value;
				ContentLabel = ((HamburgerMenuIconItem)this._HamburgerMenuSelectedItem).Label;
				OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		public string StatusBarAppStatus {
			get { return _StatusBarAppStatus; }
			set { _StatusBarAppStatus = value; 
				OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
			
		public string StatusBarMessage{
			get{ return _StatusBarMessage; }
			set { _StatusBarMessage = value; 
				OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		public Visibility ProgressBarVisibility{
			get { return _ProgressBarVisibility; }
			set { _ProgressBarVisibility = value; 
				OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		public double ProgressBarValue{
			get { return _ProgressBarValue; }
			set { _ProgressBarValue = value; 
				OnPropertyChanged();
				Logger.PropertyChanged(value);
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
		
		public override string ToString()
		{
			return string.Format("[MainWindowViewModel Id={0}]", id);
		}

	}
}
