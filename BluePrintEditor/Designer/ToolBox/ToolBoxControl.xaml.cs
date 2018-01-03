/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 5:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Interaction logic for ToolBoxControl.xaml
	/// </summary>
	public partial class ToolBoxControl : UserControl
	{
		public static readonly DependencyProperty IdProperty =
			DependencyProperty.Register("Id", typeof(Guid), typeof(ToolBoxControl),
			                            new FrameworkPropertyMetadata());
		
		public Guid Id {
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		
		public ToolBoxControl()
		{
			InitializeComponent();
			Id = Guid.NewGuid();			
		}
		
		public override string ToString()
		{
			return string.Format("[ToolBoxControl Id={0}]", Id);
		}

	}
}