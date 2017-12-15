/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 6:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Stores.Pipes
{
	/// <summary>
	/// Description of StackProvider.
	/// </summary>
	public class InputPipe : INotifyPropertyChanged
	{
		private Guid id;
		private string name;
		
		private event EventHandler _inputAvailable;
		
		private readonly object eventSyncObject = new object();
		private readonly object stackSyncObject = new object();
		private readonly object featureSyncObject = new object();

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		public Guid Id{
			get{ return id; }
			set { id = value; 
				OnPropertyChanged();
			}
		}
		
		public string Name{
			get { return name; }
			set { name = value; 
				OnPropertyChanged();
			}
		}
		
		private Stack inputObjects;
		
		public Stack InputObjects {
			get { return inputObjects; }
			set { inputObjects = value; 
				OnPropertyChanged();
			}
		}
		
		private NotificationType inputNotificationType;
		
		public NotificationType InputNotificationType {
			get { return inputNotificationType; }
			set { inputNotificationType = value; 
				OnPropertyChanged();
			}
		}
		
		private InputConsumerCollection inputConsumers;
		
		public InputConsumerCollection InputConsumers {
			get { return inputConsumers; }
			set { inputConsumers = value; 
				OnPropertyChanged();
			}
		}
		
		public StackType ProviderType {
			get { return StackType.InputStack; }			
		}
		
		public event EventHandler InputAvailable{
			add {
				lock(eventSyncObject){
					_inputAvailable += value;
				}
			}
			
			remove{
				lock (eventSyncObject) {
					_inputAvailable -= value;
				}
			}
		}
		
		protected void OnInputAvailable()
		{
			Logger.Debug("{0}: Input is available on stack, notifying all consumers now", Name);
			
			if (_inputAvailable != null) {
				_inputAvailable(this, new InputAvailableEventArgs(this));
			}
			
			foreach (IInputConsumer consumer in InputConsumers) {				
				consumer.InputAvailableNotification(this);
			}
		}		
		
		public InputPipe()
		{
			Id = Guid.NewGuid();
			Name = Get.i.Names.GetName("InputPipe");
			
			InputObjects = InputObjects ?? new Stack();
			InputConsumers = InputConsumers ?? new InputConsumerCollection();
		}	
		
		public void RegisterConsumer(IInputConsumer consumer)
		{
			lock (featureSyncObject) {
				InputConsumers.Add(consumer);
			}
			
			Logger.Debug("{0}: New input consumer registered", Name);
		}
		
		public void UnregisterConsumer(IInputConsumer consumer)
		{
			lock (featureSyncObject) {
				InputConsumers.Remove(consumer);
			}
			
			Logger.Debug("{0}: Input consumer has been unregistered", Name);
		}
		
		public void PushObject(object instance)
		{
			lock (stackSyncObject) {
				InputObjects.Push(instance);
			}
			
			if(InputNotificationType == NotificationType.Auto)
				OnInputAvailable();
		}
		
		public void PushMultiObjects(IList<object> instances)
		{
			lock (stackSyncObject) {
				foreach (object instance in instances) {
					InputObjects.Push(instance);
				}
			}
			
			if(InputNotificationType == NotificationType.Auto)
				OnInputAvailable();
		}
		
		public bool IsInputAvailable()
		{
			return (InputObjects.Count > 0);
		}
		
		public void NotifyInputConsumers()
		{
			if (InputNotificationType == NotificationType.Manual)
				OnInputAvailable();
		}
		
		public IEnumerable<object> ConsumeInput()
		{
			lock (stackSyncObject) {
				while (InputObjects.Count > 0) {
					object input = InputObjects.Pop();
					if (InputObjects.Count == 0)
						yield break;
					else
						yield return input;
				}
			}
		}
		
		public override string ToString()
		{
			return string.Format("[InputPipe Id={0}, Name={1}, InputNotificationType={2}]", id, name, inputNotificationType);
		}

		
	}
	
	public class InputAvailableEventArgs : EventArgs
	{
		public InputPipe Source;
		
		public InputAvailableEventArgs(InputPipe source)
		{
			this.Source = source;
		}	
		
	}
}
