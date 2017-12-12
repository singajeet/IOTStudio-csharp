/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 7:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Interactivity;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Types;
using Newtonsoft.Json;
using Xceed.Wpf.AvalonDock;

namespace IOTStudio.Core.Elements.GUI
{
	/// <summary>
	/// Description of BaseLayoutElement.
	/// </summary>
	[DataContract]
	public class BaseLayoutElement : DependencyObject, INotifyPropertyChanged, IBaseElement, ILayoutElement
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}

		private BaseElement baseElement = new BaseElement();
		
		[DataMember]
		public Guid Id {
			get {
				return baseElement.Id;
			}
			set {
				baseElement.Id = value;
				OnPropertyChanged();
			}
		}
		
		[DataMember]
		public string Name {
			get { return baseElement.Name; }
			set {
				baseElement.Name = value; 
				OnPropertyChanged();
			}
		}

		[DataMember]
		public ObservableCollection<Behavior<UIElement>> Behaviors {
			get {
				return baseElement.Behaviors;
			}
			set {
				baseElement.Behaviors = value;
				OnPropertyChanged();
			}
		}

		[DataMember]
		public ObservableCollection<TriggerAction<UIElement>> EventTriggers {
			get {
				return baseElement.EventTriggers;
			}
			set {
				baseElement.EventTriggers = value;
				OnPropertyChanged();
			}
		}

		
		public static readonly DependencyProperty LayoutTypeProperty =
			DependencyProperty.Register("LayoutType", typeof(LayoutElementType), typeof(BaseLayoutElement),
			                            new FrameworkPropertyMetadata());
		
		public LayoutElementType LayoutType {
			get { return (LayoutElementType)GetValue(LayoutTypeProperty); }
			set { SetValue(LayoutTypeProperty, value); }
		}
		
		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected", typeof(bool), typeof(BaseLayoutElement),
			                            new FrameworkPropertyMetadata());
		
		public bool IsSelected {
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}
		
	}
}
