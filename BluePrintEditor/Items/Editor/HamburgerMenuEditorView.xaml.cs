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
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Items.Editor
{
	/// <summary>
	/// Interaction logic for HamburgerMenuEditorView.xaml
	/// </summary>
	public partial class HamburgerMenuEditorView : UserControl
	{
		ILog Logger = Log.Get(typeof(HamburgerMenuEditorView));
		
		public static readonly DependencyProperty IdProperty =
			DependencyProperty.Register("Id", typeof(Guid), typeof(HamburgerMenuEditorView),
			                            new FrameworkPropertyMetadata());
		
		public Guid Id {
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		
		HamburgerMenuEditorItemViewModel vm;
		public HamburgerMenuEditorView()
		{
			Logger.InstanceCreated();
			Id = Guid.NewGuid();
			
			InitializeComponent();
			vm = new HamburgerMenuEditorItemViewModel();
			this.DataContext = vm;
			
			
		}
		void SelectToolButton_Selected(object sender, RoutedEventArgs e)
		{
			
		}
		
		public override string ToString()
		{
			return string.Format("[HamburgerMenuEditorView Id={0}]", Id);
		}

	}
}