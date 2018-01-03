/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 1/2/2018
 * Time: 10:47 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Designer.ToolBox.Items
{
	/// <summary>
	/// Description of PointerToolBoxItem.
	/// </summary>
	public class PointerToolBoxItem : BaseToolBoxItem
	{
		public PointerToolBoxItem()
		{
			this.Name = "PointerToolBoxItem";
			this.ToolTitle = "Pointer";
			//this.IconKind = MahApps.Metro.IconPacks.PackIconModernKind.CursorDefault;
			this.Category = "Selection";
			this.Description = "A selection tool";
		}
	}
}
