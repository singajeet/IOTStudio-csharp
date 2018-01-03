/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 13:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Interaction logic for ToolBoxSectionPanel.xaml
	/// </summary>
	public partial class ToolBoxSectionItemsContainer : UserControl
	{
		public static readonly DependencyProperty IdProperty =
			DependencyProperty.Register("Id", typeof(Guid), typeof(ToolBoxSectionItemsContainer),
			                            new FrameworkPropertyMetadata());
		
		public Guid Id {
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		
		public ToolBoxSectionItemsContainer()
		{
			InitializeComponent();
			Id = Guid.NewGuid();
		}
		
		public override string ToString()
		{
			return string.Format("[ToolBoxSectionItemsContainer Id={0}]", Id);
		}

	}
}