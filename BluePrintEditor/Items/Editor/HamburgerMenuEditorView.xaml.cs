/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 20:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BluePrintEditor.Items.Editor
{
	/// <summary>
	/// Interaction logic for HamburgerMenuEditorView.xaml
	/// </summary>
	public partial class HamburgerMenuEditorView : UserControl
	{
		HamburgerMenuEditorItemViewModel vm;
		public HamburgerMenuEditorView()
		{
			InitializeComponent();
			vm = new HamburgerMenuEditorItemViewModel();
			this.DataContext = vm;
		}
		void SelectToolButton_Selected(object sender, RoutedEventArgs e)
		{
			MyDesignerHost.MyDesignerCanvas.Children.Clear();
		}
	}
}