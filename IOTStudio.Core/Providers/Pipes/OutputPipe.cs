/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 10:01 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Providers.Pipes
{
	/// <summary>
	/// Description of OutputPipe.
	/// </summary>
	public class OutputPipe : INotifyPropertyChanged
	{
		private Guid id;
		private string name;
		
		private event EventHandler _outputAvailable;
		
		private readonly object eventSyncObject = new object();
		private readonly object stackSyncObject = new object();
		private readonly object featureSyncObject = new object();
		
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		
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
		
		public OutputPipe()
		{
			Id = Guid.NewGuid();
			Name = RuntimeNameProvider.GetName("OutputPipe");
			
			OutputObjects = OutputObjects ?? new Stack();
			OutputConsumers = OutputConsumers ?? new OutputConsumerCollection();
		}
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		private Stack outputObjects;
		
		public Stack OutputObjects {
			get { return outputObjects; }
			set { outputObjects = value; 
				OnPropertyChanged();
			}
		}
		
		private NotificationType outputNotificationType;
		
		public NotificationType OutputNotificationType {
			get { return outputNotificationType; }
			set { outputNotificationType = value; 
				OnPropertyChanged();
			}
		}
		
		private OutputConsumerCollection outputConsumers;
		
		public OutputConsumerCollection OutputConsumers {
			get { return outputConsumers; }
			set { outputConsumers = value; 
				OnPropertyChanged();
			}
		}
	
		private object producer;
		
		public object Producer {
			get { return producer; }
			set { producer = value;
				OnPropertyChanged();                                                         
			}
		}                                                                                  
		                  
		public StackType ProviderType {
			get { return StackType.OutputStack; }			
		}
		
		public event EventHandler OutputAvailable{
			add {
				lock(eventSyncObject){
					_outputAvailable += value;
				}
			}
			
			remove{
				lock (eventSyncObject) {
					_outputAvailable -= value;
				}
			}
		}
		
		protected void OnOutputAvailable()
		{
			if (_outputAvailable != null) {
				_outputAvailable (this, new OutputAvailableEventArgs(this));
			}
			
			foreach (IOutputConsumer consumer in OutputConsumers) {				
				consumer.OutputAvailableNotification(this);
			}
		}
		
		public void RegisterConsumer(IOutputConsumer consumer)
		{
			lock (featureSyncObject) {
				OutputConsumers.Add(consumer);
			}
		}
		
		public void UnregisterConsumer(IOutputConsumer consumer)
		{
			lock (featureSyncObject) {
				OutputConsumers.Remove(consumer);
			}
		}
		
		public void PushObject(object instance)
		{
			lock (stackSyncObject) {
				OutputObjects.Push(instance);
			}
			
			if(OutputNotificationType == NotificationType.Auto)
				OnOutputAvailable();
		}
		
		public void PushMultiObjects(IList<object> instances)
		{
			lock (stackSyncObject) {
				foreach (object instance in instances) {
					OutputObjects.Push(instance);
				}
			}
			
			if(OutputNotificationType == NotificationType.Auto)
				OnOutputAvailable();
		}
		
		public bool IsInputAvailable()
		{
			return (OutputObjects.Count > 0);
		}
		
		public void NotifyInputConsumers()
		{
			if (OutputNotificationType == NotificationType.Manual)
				OnOutputAvailable();
		}
		
		public IEnumerable<object> ConsumeOutput()
		{
			lock (stackSyncObject) {
				while (OutputObjects.Count > 0) {
					object input = OutputObjects.Pop();
					if (OutputObjects.Count == 0)
						yield break;
					else
						yield return input;
				}
			}
		}
		
	}
	
	
	public class OutputAvailableEventArgs : EventArgs
	{
		public OutputPipe Source;
		
		public OutputAvailableEventArgs(OutputPipe source)
		{
			this.Source = source;
		}	
		
	}
	
}
