/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Interactivity;
using IOTStudio.Core.Elements.Interfaces;
using Newtonsoft.Json;

namespace IOTStudio.Core.Elements
{
	/// <summary>
	/// Description of BaseElement.
	/// </summary>
	
	[DataContract]
	public class BaseElement : DependencyObject, IBaseElement, IEditorElement
	{
		public static readonly DependencyProperty
											IdProperty = DependencyProperty
											.Register("Id", typeof(Guid), typeof(BaseElement));
		[DataMember]
		public virtual Guid Id{
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value);}
		}
		
		public static readonly DependencyProperty
											NameProperty = DependencyProperty
															.Register("Name", typeof(string), typeof(BaseElement));
		
		[DataMember]
		public virtual string Name{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}

		public static readonly DependencyProperty
											BehaviorsProperty = DependencyProperty
																	.Register("Behaviors", 
			          												typeof(ObservableCollection<Behavior<UIElement>>), 
			          												typeof(BaseElement),
			          												new PropertyMetadata(new ObservableCollection<Behavior<UIElement>>()));
		[DataMember]
		public virtual ObservableCollection<Behavior<UIElement>> Behaviors{
			get { return (ObservableCollection<Behavior<UIElement>>)GetValue(BehaviorsProperty); }
			set { SetValue(BehaviorsProperty, value); }
		}
		
		public static readonly DependencyProperty
											EventTriggersProperty = DependencyProperty
																	.Register("EventTriggers", 
			          												typeof(ObservableCollection<TriggerAction<UIElement>>), 
			          												typeof(BaseElement),
			          												new PropertyMetadata(new ObservableCollection<TriggerAction<UIElement>>()));
		
		public virtual ObservableCollection<TriggerAction<UIElement>> EventTriggers{
			get { return (ObservableCollection<TriggerAction<UIElement>>)GetValue(EventTriggersProperty); }
			set { SetValue(EventTriggersProperty, value); }
		}
	}
}
