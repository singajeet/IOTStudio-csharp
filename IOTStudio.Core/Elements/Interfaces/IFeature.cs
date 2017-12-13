/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 7:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Input;
using IOTStudio.Core.Features;
using IOTStudio.Core.Providers.Pipes;

namespace IOTStudio.Core.Elements.Interfaces
{
	/// <summary>
	/// Description of IFeature.
	/// </summary>
	public interface IFeature
	{
		Guid Id { get; set; }
		string Name { get; set; }
		
		IFeature ParentFeature { get; set; }
		FeatureCollection ChildFeatures { get; set; }
		
		InputPipe InPipe { get; set; }
		OutputPipe OutPipe { get; set; }
		string InputFlagName { get; set; }
		string OutputFlagName { get; set; }
		
		IUIFeatureOptionsElement UIOptionsElement { get; set; }
		IUIRootElement RootElement { get; set; }
		INavigationElement NavigationElement { get; set; }
		
		object Run(object parameter);
		
		event EventHandler UIRootElementChanged;
		event EventHandler NavigationElementChanged;
		event EventHandler OutputProducing;
		event EventHandler OutputProduced;
		event EventHandler InputConsuming;
		event EventHandler InputConsumed;
		event EventHandler FlagNameChanged;
		
	}
}
