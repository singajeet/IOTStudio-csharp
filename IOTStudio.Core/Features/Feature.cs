/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 5:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Providers;
using IOTStudio.Core.Providers.Pipes;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of Feature.
	/// </summary>
	public class Feature : IFeature, INotifyPropertyChanged
	{
		private Guid id;
		private string name;
		private IFeature parentFeature;
		private FeatureCollection childFeatures;
		private InputPipe inPipe;
		private OutputPipe outPipe;
		private string inputFlagName;
		private string outputFlagName;
		private IUIFeatureOptionsElement uiOptionsElement;
		private IUIRootElement rootElement;
		private INavigationElement navigationElement;
		
		public Feature()
		{
			Id = Guid.NewGuid();
			Name = RuntimeNameProvider.GetName("Feature");
		}

		#region IFeature implementation		

		public object Run(object parameter)
		{
			
		}

		public Guid Id {
			get {
				return id;
			}
			set {
				id = value;
				OnPropertyChanged();
			}
		}

		public string Name {
			get {
				return name;
			}
			set {
				name = value;
				OnPropertyChanged();
			}
		}

		public IFeature ParentFeature {
			get {
				return parentFeature;
			}
			set {
				parentFeature = value;
				OnPropertyChanged();
			}
		}

		public FeatureCollection ChildFeatures {
			get {
				return childFeatures;
			}
			set {
				childFeatures = value;
				OnPropertyChanged();
			}
		}

		public InputPipe InPipe {
			get {
				return inPipe;
			}
			set {
				inPipe = value;
				OnPropertyChanged();
			}
		}

		public OutputPipe OutPipe {
			get {
				return outPipe;
			}
			set {
				outPipe = value;
				OnPropertyChanged();
			}
		}

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

		public IUIFeatureOptionsElement UIOptionsElement {
			get {
				return uiOptionsElement;
			}
			set {
				uiOptionsElement = value;
				OnPropertyChanged();
			}
		}

		public IUIRootElement RootElement {
			get {
				return rootElement;
			}
			set {
				rootElement = value;
				OnPropertyChanged();
				
				if (UIRootElementChanged != null)
					UIRootElementChanged(this, new EventArgs());
			}
		}

		public INavigationElement NavigationElement {
			get {
				return navigationElement;
			}
			set {
				navigationElement = value;
				OnPropertyChanged();
				
				if (NavigationElementChanged != null)
					NavigationElementChanged(this, new EventArgs());
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
