/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/24/2017
 * Time: 10:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using BluePrintEditor.Items.Editor;
using MahApps.Metro.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of PropertyGridHelper.
	/// </summary>
	public sealed class PropertyGridHelper
	{
		private static PropertyGridHelper instance = new PropertyGridHelper();
		
		public static PropertyGridHelper Instance {
			get {
				return instance;
			}
		}
		
		private PropertyGridHelper()
		{
		}
		
		public void UpdateSelectedItem<T>(object item){
			
			if(item != null){
			HamburgerMenuItem selectedMenuItem = MainWindowViewModel.Instance.HamburgerMenuSelectedItem;
			
			if (selectedMenuItem != null) {
			
				HamburgerMenuEditorViewItem editorViewItem = selectedMenuItem as HamburgerMenuEditorViewItem;
				if (editorViewItem != null) {
					if (editorViewItem.View != null) {
						HamburgerMenuEditorView editorView = editorViewItem.View as HamburgerMenuEditorView;
						
							//editorView.PropertyGrid.ItemsSource = PropertyHelper.Instance.GetProperties(item);
					}
				}
			}
			}
		}
	}
}
