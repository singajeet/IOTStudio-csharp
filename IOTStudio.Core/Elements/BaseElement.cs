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
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
	public class BaseElement : INotifyPropertyChanged, IBaseElement, IEditorElement
	{
		private Guid id;
		private string name;

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion	
		
		[DataMember]
		public virtual Guid Id{
			get { return id; }
			set { id= value;
				OnPropertyChanged();
			}
		}
		
		[DataMember]
		public virtual string Name{
			get { return name; }
			set { name= value; 
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Behavior<UIElement>> behaviors;
			
		[DataMember]
		public virtual ObservableCollection<Behavior<UIElement>> Behaviors{
			get { return behaviors; }
			set { behaviors= value; 
				OnPropertyChanged();
			}
		}
		
		private ObservableCollection<TriggerAction<UIElement>> eventTriggers;
		
		[DataMember]
		public virtual ObservableCollection<TriggerAction<UIElement>> EventTriggers{
			get { return eventTriggers; }
			set { eventTriggers= value; 
				OnPropertyChanged();
			}
		}
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		public BaseElement()
		{
			Behaviors = Behaviors ?? new ObservableCollection<Behavior<UIElement>>();
			EventTriggers = EventTriggers ?? new ObservableCollection<TriggerAction<UIElement>>();
		}
	}
}
