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
using IOTStudio.Core.Features;
using IOTStudio.Core.Features.Interfaces;

namespace IOTStudio.Core.Providers.Pipes
{
	/// <summary>
	/// Description of StackProvider.
	/// </summary>
	public class InputPipe : INotifyPropertyChanged
	{
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
		
		private FeatureCollection inputConsumerFeatures;
		
		public FeatureCollection InputConsumerFeatures {
			get { return inputConsumerFeatures; }
			set { inputConsumerFeatures = value; 
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
			if (_inputAvailable != null) {
				_inputAvailable(this, new InputAvailableEventArgs(this));
			}
			
			foreach (IFeature feature in InputConsumerFeatures) {				
				feature.NotifyInputAvailable(this);
			}
		}		
		
		public InputPipe()
		{
			InputObjects = InputObjects ?? new Stack();
			InputConsumerFeatures = InputConsumerFeatures ?? new FeatureCollection();
		}	
		
		public void RegisterConsumerFeature(IFeature feature)
		{
			lock (featureSyncObject) {
				InputConsumerFeatures.Add(feature);
			}
		}
		
		public void UnregisterConsumerFeature(IFeature feature)
		{
			lock (featureSyncObject) {
				InputConsumerFeatures.Remove(feature);
			}
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
		
		public object ConsumeInput()
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
		
	}
	
	public enum NotificationType
	{
		Auto,
		Manual
	}
	
	public enum StackType
	{
		InputStack,
		OutputStack
	}
	
	public class InputAvailableEventArgs : EventArgs
	{
		public InputStack Source;
		
		public InputAvailableEventArgs(InputStack source)
		{
			this.Source = source;
		}	
		
	}
}
