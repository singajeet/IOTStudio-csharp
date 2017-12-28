/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 1:09 PM
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
	/// Description of GridAddColumnCommand.
	/// </summary>
	public static class GridAddColumnCommand
	{
		public static readonly RoutedCommand Command = new RoutedCommand();
		
		static GridAddColumnCommand()
		{
			Application.Current.MainWindow.CommandBindings.Add(new CommandBinding(Command, Execute, CanExecute));
		}
		
		private static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
			Grid grid = e.Parameter as Grid;
			if (grid == null) {
				e.CanExecute = false;
				return;
			}
			
			e.CanExecute = true;
        }

        private static async void Execute(object sender, ExecutedRoutedEventArgs e)
        {
			Grid grid = e.Parameter as Grid;
			if (grid != null) {
				
				ColumnDefinition column = new ColumnDefinition();
				column.Width = GridLength.Auto;
				grid.ColumnDefinitions.Add(column);
			}
			e.Handled = false;
        }
	}
}
