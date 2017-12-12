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
using System.Collections.ObjectModel;
using System.Windows;
using IOTStudio.Core.Features.Interfaces;

namespace IOTStudio.Core.Providers.Stacks
{
	/// <summary>
	/// Description of StackProvider.
	/// </summary>
	public class InputStack : DependencyObject
	{
		private event EventHandler _inputAvailable;
		
		private readonly object eventSyncObject = new object();
		private readonly object stackSyncObject = new object();
		private readonly object featureSyncObject = new object();
		
		public static readonly DependencyProperty InputObjectsProperty =
			DependencyProperty.Register("InputObjects", typeof(Stack), typeof(InputStack),
			                            new FrameworkPropertyMetadata());
		
		public Stack InputObjects {
			get { return (Stack)GetValue(InputObjectsProperty); }
			set { SetValue(InputObjectsProperty, value); }
		}
		
		public static readonly DependencyProperty InputNotificationTypeProperty =
			DependencyProperty.Register("InputNotificationType", typeof(NotificationType), typeof(InputStack),
			                            new FrameworkPropertyMetadata());
		
		public NotificationType InputNotificationType {
			get { return (NotificationType)GetValue(InputNotificationTypeProperty); }
			set { SetValue(InputNotificationTypeProperty, value); }
		}
		
		public static readonly DependencyProperty RegisteredFeaturesProperty =
			DependencyProperty.Register("RegisteredFeatures", typeof(ObservableCollection<IFeature>), typeof(InputStack),
			                            new FrameworkPropertyMetadata());
		
		public ObservableCollection<IFeature> RegisteredFeatures {
			get { return (ObservableCollection<IFeature>)GetValue(RegisteredFeaturesProperty); }
			set { SetValue(RegisteredFeaturesProperty, value); }
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
		
		private void OnInputAvailable()
		{
			if (_inputAvailable != null) {
				_inputAvailable(this, new InputAvailableEventArgs(this));
			}
			
			foreach (IFeature feature in RegisteredFeatures) {				
				feature.InputAvailableFromInputStack(this);
			}
		}		
		
		public InputStack()
		{
			InputObjects = InputObjects ?? new Stack();
			RegisteredFeatures = RegisteredFeatures ?? new ObservableCollection<IFeature>();
		}	
		
		public void RegisterFeature(IFeature feature)
		{
			lock (featureSyncObject) {
				RegisteredFeatures.Add(feature);
			}
		}
		
		public void UnregisterFeature(IFeature feature)
		{
			lock (featureSyncObject) {
				RegisteredFeatures.Remove(feature);
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
		
		public void Notify()
		{
			if (InputNotificationType == NotificationType.Manual)
				OnInputAvailable();
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
