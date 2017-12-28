/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 1:12 PM
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
	/// Description of GridDeleteColumnCommand.
	/// </summary>
	public static class GridDeleteColumnCommand
	{
		public static readonly RoutedCommand Command = new RoutedCommand();
		
		static GridDeleteColumnCommand()
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
			
			if (grid.ColumnDefinitions == null) {
				e.CanExecute = false;
				return;
			}
			
			if (grid.ColumnDefinitions.Count == 0) {
				e.CanExecute = false;
				return;
			}
			
			e.CanExecute = true;
        }

        private static async void Execute(object sender, ExecutedRoutedEventArgs e)
        {
			Grid grid = e.Parameter as Grid;
			if (grid != null && grid.ColumnDefinitions != null && grid.ColumnDefinitions.Count > 0) {
				grid.ColumnDefinitions.RemoveAt(0);
			}
			e.Handled = false;
        }
	}
}
