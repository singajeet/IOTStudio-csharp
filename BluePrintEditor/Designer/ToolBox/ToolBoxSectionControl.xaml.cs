/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 14:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Interaction logic for ToolBoxSectionControl.xaml
	/// </summary>
	public partial class ToolBoxSectionControl : UserControl
	{
		public static readonly DependencyProperty IdProperty =
			DependencyProperty.Register("Id", typeof(Guid), typeof(ToolBoxSectionControl),
			                            new FrameworkPropertyMetadata());
		
		public Guid Id {
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		public ToolBoxSectionControl()
		{
			InitializeComponent();
			Id = Guid.NewGuid();
		}
		
		public override string ToString()
		{
			return string.Format("[ToolBoxSectionControl Id={0}]", Id);
		}

	}
}