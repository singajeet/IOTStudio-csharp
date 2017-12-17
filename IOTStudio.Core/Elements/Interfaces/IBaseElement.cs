/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 6:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Interactivity;

namespace IOTStudio.Core.Elements.Interfaces
{
	/// <summary>
	/// Description of IBaseElement.
	/// </summary>
	public interface IBaseElement
	{
		Guid Id { get; }
		string Name { get; set; }
		ObservableCollection<Behavior<UIElement>> Behaviors { get; set; }
		ObservableCollection<TriggerAction<UIElement>> EventTriggers { get; set; }
	}
}
