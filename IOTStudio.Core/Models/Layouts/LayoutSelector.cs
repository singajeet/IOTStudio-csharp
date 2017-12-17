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
using System.Linq;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Stores.Providers;

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
			
			SelectedLayout = SelectedLayout ?? GetSelectedLayout();
		}

		BaseLayoutElement GetSelectedLayout()
		{			
			if (Layouts.Count(l => l.IsSelected == true) > 0) {
				BaseLayoutElement selectedLayout = Layouts.Where(l => l.IsSelected == true)
														.ToList().SingleOrDefault();
				return selectedLayout;
			} else {
				Layouts[0].IsSelected = true;
				if (Get.i.Layouts.ContainsKey(Layouts[0].Id))
					Get.i.Layouts.SaveLayout(Layouts[0]);
				else
					Get.i.Layouts.InsertLayout(Layouts[0]);
			}
			return Layouts[0];
		}
		
		public void SelectLayout(Guid key)
		{
			Layouts.Where(w => w.IsSelected == true)
							.ToList()
							.ForEach(f => f.IsSelected = false);
			
			Get.i.Layouts.UnselectAll();
			
			SelectedLayout = Layouts.Where(w=> w.Id == key).ToList().Single();
			SelectedLayout.IsSelected = true;
			
			if (Get.i.Layouts.ContainsKey(key)) {
				Get.i.Layouts.SaveLayout(SelectedLayout);
			} else {
				Get.i.Layouts.InsertLayout(SelectedLayout);
			}
			
			SelectedLayout.SerializeToXml();
			
			Logger.Debug("Selected layout changed to: {0}", SelectedLayout.Name);
		}
		
		public ObservableCollection<BaseLayoutElement> LoadLayouts()
		{
			string path = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Properties.Layouts.Get("SavePath"));
			Logger.Debug("Layouts will be loaded from the following path: {0}", path);
			
			ObservableCollection<BaseLayoutElement> layoutObjects = Get.i.Assemblies.GetCollectionOfObjects<BaseLayoutElement>(path);
			
			foreach (BaseLayoutElement layout in layoutObjects) {
				if (Get.i.Layouts.ContainsKey(layout.Id)) {
					Layout metadata = Get.i.Layouts.LoadLayout(layout.Id);					
					layout.IsSelected = metadata.IsSelected;				
				} else {
					Get.i.Layouts.InsertLayout(layout);
				}
			}
			return layoutObjects;
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
		
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
