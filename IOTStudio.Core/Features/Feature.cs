/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 5:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Providers;
using IOTStudio.Core.Providers.Pipes;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of Feature.
	/// </summary>
	[DataContract]
	public class Feature : IFeature, INotifyPropertyChanged
	{
		private Guid id;
		private string name;
		private bool isEnabled;
		private IFeature parentFeature;
		private FeatureCollection childFeatures = new FeatureCollection();
		private InputPipe inPipe = new InputPipe();
		private OutputPipe outPipe = new OutputPipe();
		private string inputFlagName;
		private string outputFlagName;
		private IUIFeatureOptionsElement uiOptionsElement;
		private IUIRootElement rootElement;
		private ObservableCollection<INavigationElement> navigationElements = new ObservableCollection<INavigationElement>();
		private IFeatureInfo info = new FeatureInfo();

		public Feature()
		{
			Id = info.Id;
			Name = info.Name;
		}

		#region IFeature implementation		

		public object Run(object parameter)
		{
			return null;
		}
		
		public void Enable()
		{
			IsEnabled = true;
		}
		
		public void Disable()
		{
			IsEnabled = false;
		}

		[DataMember]
		public Guid Id {
			get {
				return id;
			}
			set {
				id = value;
				OnPropertyChanged();
			}
		}

		[DataMember]
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				OnPropertyChanged();
			}
		}
		
		[DataMember]
		public bool IsEnabled{
			get { return isEnabled; }
			set { isEnabled = value;
				OnPropertyChanged();
			}
		}
		
		[IgnoreDataMember]
		public IFeatureInfo Info{
			get { return info; }
			set { info = value; 
				OnPropertyChanged();
			}
		}

		[DataMember]
		public IFeature ParentFeature {
			get {
				return parentFeature;
			}
			set {
				parentFeature = value;
				OnPropertyChanged();
			}
		}

		[DataMember]
		public FeatureCollection ChildFeatures {
			get {
				return childFeatures;
			}
			set {
				childFeatures = value;
				OnPropertyChanged();
			}
		}

		[IgnoreDataMember]
		public InputPipe InPipe {
			get {
				return inPipe;
			}
			set {
				inPipe = value;
				OnPropertyChanged();
			}
		}

		[IgnoreDataMember]
		public OutputPipe OutPipe {
			get {
				return outPipe;
			}
			set {
				outPipe = value;
				OnPropertyChanged();
			}
		}

		[DataMember]
		public string InputFlagName {
			get {
				return inputFlagName;
			}
			set {
				inputFlagName = value;
				OnPropertyChanged();
				
				if (FlagNameChanged != null) {
					FlagNameChanged(this, new FlagNameChangedEventArgs(FlagType.InputFlag, value));
				}
			}
		}

		[DataMember]
		public string OutputFlagName {
			get {
				return outputFlagName;
			}
			set {
				outputFlagName = value;
				OnPropertyChanged();
				
				if (FlagNameChanged != null) {
					FlagNameChanged(this, new FlagNameChangedEventArgs(FlagType.OutputFlag, value));
				}
			}
		}

		[IgnoreDataMember]
		public IUIFeatureOptionsElement UIOptionsElement {
			get {
				return uiOptionsElement;
			}
			set {
				uiOptionsElement = value;
				OnPropertyChanged();
			}
		}

		[IgnoreDataMember]
		public IUIRootElement RootElement {
			get {
				return rootElement;
			}
			set {
				rootElement = value;
				OnPropertyChanged();
				
				if (UIRootElementChanged != null)
					UIRootElementChanged(this, new UIRootElementChangedEventArgs(value));
			}
		}

		[IgnoreDataMember]
		public ObservableCollection<INavigationElement> NavigationElements {
			get {
				return navigationElements;
			}
			set {
				navigationElements = value;
				OnPropertyChanged();
				
				if (NavigationElementChanged != null)
					NavigationElementChanged(this, new NavigationElementChangedEventArgs(value));
			}
		}
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		
		public event EventHandler UIRootElementChanged;
		public event EventHandler NavigationElementChanged;
		public event EventHandler OutputProducing;
		public event EventHandler OutputProduced;
		public event EventHandler InputConsuming;
		public event EventHandler InputConsumed;
		public event EventHandler FlagNameChanged;
		
		#endregion
	}
	
	public class InputOutputPipeDataEventArgs : EventArgs
	{
		public Stack Data;
		public StackType DataType;
		public bool IsOperationCompleted;
		
		public InputOutputPipeDataEventArgs(Stack data, StackType dataType, bool isOpsCompleted)
		{
			Data = data;
			DataType = dataType;
			IsOperationCompleted = isOpsCompleted;
		}
	}
	
	public class UIRootElementChangedEventArgs : EventArgs
	{
		public IUIRootElement RootElement;
		
		public UIRootElementChangedEventArgs(IUIRootElement root)
		{
			RootElement = root;
		}
	}
	
	public class NavigationElementChangedEventArgs : EventArgs
	{
		public ObservableCollection<INavigationElement> NavigationElements;
		
		public NavigationElementChangedEventArgs(ObservableCollection<INavigationElement> elements)
		{
			NavigationElements = elements;
		}
	}
	
	public class FlagNameChangedEventArgs : EventArgs
	{
		public FlagType FlagType;
		public string FlagName;
		
		public FlagNameChangedEventArgs(FlagType type, string name)
		{
			FlagType = type;
			FlagName = name;
		}
	}
}
