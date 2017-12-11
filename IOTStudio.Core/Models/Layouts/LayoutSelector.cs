/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/11/2017
 * Time: 1:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Providers.Assemblies;
using IOTStudio.Core.Providers.Properties;

namespace IOTStudio.Core.Models.Layouts
{
	/// <summary>
	/// Description of LayoutSelector.
	/// </summary>
	[DataContract(IsReference = true)]
	public class LayoutSelector: DependencyObject, INotifyPropertyChanged
	{
		public static readonly DependencyProperty LayoutsProperty =
			DependencyProperty.Register("Layouts", typeof(ObservableCollection<ILayoutElement>), typeof(LayoutSelector),
			                            new FrameworkPropertyMetadata());
		
		[DataMember]
		public ObservableCollection<ILayoutElement> Layouts {
			get { return (ObservableCollection<ILayoutElement>)GetValue(LayoutsProperty); }
			set { SetValue(LayoutsProperty, value); }
		}
		
		public static readonly DependencyProperty 
											SelectedLayoutProperty = DependencyProperty
																	.Register("SelectedLayout", 
			          												typeof(ILayoutElement), 
			          												typeof(LayoutSelector),
			                                                        new PropertyMetadata());
		
		[DataMember]
		public ILayoutElement SelectedLayout{
			get { return (ILayoutElement)GetValue(SelectedLayoutProperty); }
			set { SetValue(SelectedLayoutProperty, value); }
		}
		
		public LayoutSelector()
		{
			Layouts = Layouts ?? LoadLayouts();
			SelectedLayout = SelectedLayout ?? new DefaultLayout();
		}

		ObservableCollection<ILayoutElement> LoadLayouts()
		{
			string path = PropertyProvider.LayoutSelector.GetProperty("LayoutsCollectionPath") as string;
			return AssemblyLoader.GetCollectionOfObjects<ILayoutElement>(path);
		}
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
			}
			
		}
		#endregion
	}
}
