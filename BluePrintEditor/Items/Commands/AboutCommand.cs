/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 13:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Input;

namespace BluePrintEditor.Commands
{
	/// <summary>
	/// Description of AboutCommand.
	/// </summary>
	public static class AboutCommand
	{
		public static readonly RoutedCommand Command = new RoutedCommand();

        static AboutCommand()
        {
            Application.Current.MainWindow.CommandBindings.Add(new CommandBinding(Command, Execute, CanExecute));
        }

        private static void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //var mainViewModel = ((MainWindow)sender).DataContext as MainWindowViewModel;
            //e.CanExecute = mainViewModel != null && mainViewModel.CanShowHamburgerAboutCommand;
        }

        private static async void Execute(object sender, ExecutedRoutedEventArgs e)
        {
            //var menuItem = e.Parameter as HamburgerMenuItem;
            //await ((MainWindow)sender).ShowMessageAsync("", $"You clicked on {menuItem.Label} button");
        }
	}
}
