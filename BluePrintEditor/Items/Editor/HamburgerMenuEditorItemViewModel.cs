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
using MahApps.Metro.Controls;

namespace BluePrintEditor.Items.Editor
{
	/// <summary>
	/// Description of HamburgerMenuEditorItemViewModel.
	/// </summary>
	public class HamburgerMenuEditorItemViewModel: INotifyPropertyChanged, IDisposable
	{
		private ListBoxItem _CommandsBoxSelectedItem;
		private bool _AddFeatureButtonIsSelected;
		private Visibility _FeaturesVisibility=Visibility.Hidden;
		public HamburgerMenuEditorItemViewModel()
		{
		}
		
		public ListBoxItem CommandsBoxSelectedItem {
			get{ return _CommandsBoxSelectedItem; }
			set {
				_CommandsBoxSelectedItem = value; 
			
				OnPropertyChanged();
			}
		}
		
		public bool AddFeatureButtonIsSelected{
			get { return _AddFeatureButtonIsSelected;  }
			set{ _AddFeatureButtonIsSelected = value; 
				FeaturesVisibility = _AddFeatureButtonIsSelected ? Visibility.Visible : Visibility.Hidden;
				OnPropertyChanged();
			}
		}
		
		public Visibility FeaturesVisibility{
			get{ return _FeaturesVisibility; }
			set{ _FeaturesVisibility = value; 
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
		#region IDisposable implementation

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
