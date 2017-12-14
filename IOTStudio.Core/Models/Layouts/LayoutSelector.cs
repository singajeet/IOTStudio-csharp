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
using IOTStudio.Core.Elements.UI;
using IOTStudio.Core.Providers;
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Properties;
using System.Linq;

namespace IOTStudio.Core.Models.Layouts
{
	/// <summary>
	/// Description of LayoutSelector.
	/// </summary>
	[DataContract(IsReference = true)]
	public class LayoutSelector: DependencyObject, INotifyPropertyChanged
	{		

		public static readonly DependencyProperty LayoutsProperty =
			DependencyProperty.Register("Layouts", typeof(ObservableCollection<BaseLayoutElement>), typeof(LayoutSelector),
			                            new FrameworkPropertyMetadata());
		
		[DataMember]
		public ObservableCollection<BaseLayoutElement> Layouts {
			get { return (ObservableCollection<BaseLayoutElement>)GetValue(LayoutsProperty); }
			set { SetValue(LayoutsProperty, value); }
		}
		
		public static readonly DependencyProperty 
											SelectedLayoutProperty = DependencyProperty
																	.Register("SelectedLayout", 
			          												typeof(BaseLayoutElement), 
			          												typeof(LayoutSelector),
			                                                        new PropertyMetadata());
		
		[DataMember]
		public BaseLayoutElement SelectedLayout{
			get { return (BaseLayoutElement)GetValue(SelectedLayoutProperty); }
			set { SetValue(SelectedLayoutProperty, value); }
		}
		
		public LayoutSelector()
		{
			Logger.Debug("Instance created successfully");
			
			Logger.Debug("Loading all available layouts");
			Layouts = Layouts ?? LoadLayouts();
			
			Logger.Debug("{0} layouts loaded from the configured path", Layouts.Count);
			
			SelectedLayout = SelectedLayout ?? Layouts[0];
		}

		public void SelectLayout(string layout)
		{
			Layouts.Where(w => w.IsSelected == true)
							.ToList()
							.ForEach(f => f.IsSelected = false);
			
			SelectedLayout = Layouts.Where(w=> w.Name.Equals(layout)).ToList().Single();
			SelectedLayout.IsSelected = true;
			
			Logger.Debug("Selected layout changed to: {0}", SelectedLayout.Name);
		}
		
		public ObservableCollection<BaseLayoutElement> LoadLayouts()
		{
			string path = Properties.LayoutSelector.Get("LayoutsCollectionPath") as string;
			Logger.Debug("Layouts will be loaded from the following path: {0}", path);
			
			return Get.i.Assemblies.GetCollectionOfObjects<BaseLayoutElement>(path);
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
