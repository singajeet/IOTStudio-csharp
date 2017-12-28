/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 5:49 PM
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

namespace BluePrintEditor.Designer.Canvas
{
	/// <summary>
	/// Interaction logic for CanvasControl.xaml
	/// </summary>
	public partial class CanvasControl : UserControl
	{
		ILog Logger = Log.Get(typeof(CanvasControl));
		
		public CanvasControl()
		{
			Logger.InstanceCreated();
			
			InitializeComponent();
			this.DataContextChanged+= CanvasControl_DataContextChanged;			
		}

		void CanvasControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Logger.DataContextChanged(e);
		}
	}
}