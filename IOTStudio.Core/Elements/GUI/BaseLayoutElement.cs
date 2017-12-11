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
	public class BaseLayoutElement : DockingManager, IBaseElement, ILayoutElement
	{
		private BaseElement baseElement = new BaseElement();
		
		public Guid Id {
			get {
				return baseElement.Id;
			}
			set {
				baseElement.Id = value;
			}
		}

	public ObservableCollection<Behavior<UIElement>> Behaviors {
		get {
				return baseElement.Behaviors;
		}
		set {
				baseElement.Behaviors = value;
		}
	}

	public ObservableCollection<TriggerAction<UIElement>> EventTriggers {
		get {
				return baseElement.EventTriggers;
		}
		set {
				baseElement.EventTriggers = value;
		}
	}

		
		public static readonly DependencyProperty LayoutTypeProperty
														= DependencyProperty
															.Register("LayoutType", 
								          						typeof(LayoutElementType), typeof(BaseLayoutElement));
		
		#region IProjectElement implementation
		[JsonProperty]
		public LayoutElementType LayoutType {
			get {
				return (LayoutElementType)GetValue(LayoutTypeProperty);
			}
			set {
				SetValue(LayoutTypeProperty, value);
			}
		}
		#endregion
	}
}
