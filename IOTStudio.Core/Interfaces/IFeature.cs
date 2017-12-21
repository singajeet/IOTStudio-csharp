/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 7:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.IO;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Features;
using IOTStudio.Core.Packages;
using IOTStudio.Core.Stores.Pipes;
using IOTStudio.Core.Stores.Providers;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of IFeature.
	/// </summary>
	public interface IFeature
	{
		Guid Id { get; set; }
		string Name { get; set; }		
		Package ParentPackage { get; set; }
		FeatureRecord Record { get; set; }
		IFeatureDetails DetailsFromFile { get; set; }
		FileInfo RawFeatureInfFile { get; set; }
		
		IFeature ParentFeature { get; set; }
		FeatureCollection ChildFeatures { get; set; }
		
		InputPipe InPipe { get; set; }
		OutputPipe OutPipe { get; set; }
		
		IUIFeatureOptionsElement UIOptionsElement { get; set; }
		IUIRootElement RootElement { get; set; }
		ObservableCollection<INavigationElement> NavigationElements { get; set; }
		
		object Run(object parameter);
		bool ValidateFile();
		void Install();
		void Uninstall();
		void Activate();
		void Deactivate();
		
		event EventHandler UIOptionsElementLoaded;
		event EventHandler UIRootElementLoaded;
		event EventHandler NavigationElementLoaded;
		event EventHandler OutputProducing;
		event EventHandler OutputProduced;
		event EventHandler InputConsuming;
		event EventHandler InputConsumed;
		event EventHandler FlagDetailsLoaded;
		
		event EventHandler InstallationStarted;
		event EventHandler InstallationCompleted;
		event EventHandler FeatureDetailsLoaded;
		event EventHandler ChildFeaturesDetailsLoaded;
		event EventHandler ParentFeatureDetailsLoaded;
		event EventHandler FeatureRegistered;				
		event EventHandler FeatureActivated;
		event EventHandler FeatureDeactivated;
		event EventHandler UninstallationStarted;
		event EventHandler UninstallationCompleted;
		event EventHandler FeatureFileValidated;
		
	}
}
