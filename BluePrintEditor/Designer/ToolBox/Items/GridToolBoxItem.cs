/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 1/2/2018
 * Time: 11:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Designer.ToolBox.Items
{
	/// <summary>
	/// Description of GridToolBoxItem.
	/// </summary>
	public class GridToolBoxItem : BaseToolBoxItem
	{
		public GridToolBoxItem()
		{
			this.Name = "GridToolBoxItem";
			this.ToolTitle = "Grid";
			//this.IconKind = MahApps.Metro.IconPacks.PackIconModernKind.Grid;
			this.Category = "Elements";
			this.Description = "Add a Grid";
		}
	}
}
