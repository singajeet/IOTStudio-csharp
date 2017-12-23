/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/22/2017
 * Time: 17:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BluePrintEditor.Designer.Controls
{
	/// <summary>
	/// Interaction logic for DesignerView.xaml
	/// </summary>
	public partial class CanvasBaseItem : UserControl
	{
		public CanvasBaseItem()
		{
			InitializeComponent();
		}
		
		private void OnClick(object sender, RoutedEventArgs args)
        {
            CheckBox selectionCheckBox = sender as CheckBox;
            if (selectionCheckBox != null && selectionCheckBox.IsChecked == true)
            {
                foreach (Control child in EditorCanvas.Children)
                {
                    Selector.SetIsSelected(child, true);
                }
            }
            else
            {
                foreach (Control child in EditorCanvas.Children)
                {
                    Selector.SetIsSelected(child, false);
                }
            }
        }
	}
}