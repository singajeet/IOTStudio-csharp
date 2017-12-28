/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 12:39 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BluePrintEditor.Commands
{
	/// <summary>
	/// Description of GridAddRowCommand.
	/// </summary>
	public static class GridAddRowCommand
	{
		public static readonly RoutedCommand Command = new RoutedCommand();
		
		static GridAddRowCommand()
		{
			Application.Current.MainWindow.CommandBindings.Add(new CommandBinding(Command, Execute, CanExecute));
		}
		
		private static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
			Grid grid = e.Parameter as Grid;
			if (grid == null)
				e.CanExecute = false;
			
			e.CanExecute = true;
        }

        private static async void Execute(object sender, ExecutedRoutedEventArgs e)
        {
			Grid grid = e.Parameter as Grid;
			if (grid != null) {
				RowDefinition row = new RowDefinition();
				row.Height = GridLength.Auto;
				grid.RowDefinitions.Add(row);
			}
			e.Handled = false;
        }
	}
}
