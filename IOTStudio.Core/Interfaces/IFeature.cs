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
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Features;
using IOTStudio.Core.Stores.Pipes;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of IFeature.
	/// </summary>
	public interface IFeature
	{
		Guid Id { get; set; }
		string Name { get; set; }
		bool IsEnabled { get; set; }
		
		IFeatureInfo Info { get; set; }
		
		IFeature ParentFeature { get; set; }
		FeatureCollection ChildFeatures { get; set; }
		
		InputPipe InPipe { get; set; }
		OutputPipe OutPipe { get; set; }
		string InputFlagName { get; set; }
		string OutputFlagName { get; set; }
		
		IUIFeatureOptionsElement UIOptionsElement { get; set; }
		IUIRootElement RootElement { get; set; }
		ObservableCollection<INavigationElement> NavigationElements { get; set; }
		
		object Run(object parameter);
		void Enable();
		void Disable();
		
		event EventHandler UIRootElementChanged;
		event EventHandler NavigationElementChanged;
		event EventHandler OutputProducing;
		event EventHandler OutputProduced;
		event EventHandler InputConsuming;
		event EventHandler InputConsumed;
		event EventHandler FlagNameChanged;
		
	}
}
