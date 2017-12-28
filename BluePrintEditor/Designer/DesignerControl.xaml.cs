/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 5:45 PM
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
using BluePrintEditor.Designer.Options;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Designer
{
	/// <summary>
	/// Interaction logic for DesignerControl.xaml
	/// </summary>
	public partial class DesignerControl : UserControl
	{
		ILog Logger = Log.Get(typeof(DesignerControl));
		
		public DesignerControl()
		{
			Logger.InstanceCreated();
			InitializeComponent();
			this.DataContextChanged+= DesignerControl_DataContextChanged;
			CanvasViewModel vm = CanvasViewModel.Instance;	
			vm.GridCellSize = 10;
			vm.GridColor = Brushes.Red;
			vm.GridLinesThickness = 1;
			vm.ShowGridLines = true;
			
			this.DataContext = vm;				
		}

		void DesignerControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Logger.DataContextChanged(e);
		}
	}
}