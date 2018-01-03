/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 1:14 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Input;

namespace BluePrintEditor.Commands
{
	/// <summary>
	/// Description of AddFeatureCommand.
	/// </summary>
	public static class AddFeaturesCommand
	{
		public static readonly RoutedCommand Command = new RoutedCommand();
				
		static AddFeaturesCommand()
		{
			Application.Current.MainWindow.CommandBindings.Add(new CommandBinding(Command, Execute, CanExecute));
		}
		
		private static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
			e.CanExecute = true;
        }

        private static async void Execute(object sender, ExecutedRoutedEventArgs e)
        {
			
			e.Handled = true;
        }
	}
}
