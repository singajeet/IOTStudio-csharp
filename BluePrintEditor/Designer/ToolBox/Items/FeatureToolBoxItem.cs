/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 1/2/2018
 * Time: 11:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Designer.ToolBox.Items
{
	/// <summary>
	/// Description of FeatureToolBoxItem.
	/// </summary>
	public class FeatureToolBoxItem : BaseToolBoxItem
	{
		public FeatureToolBoxItem()
		{
			this.Name = "FeatureToolBoxItem";
			this.ToolTitle = "Feature";
			//this.IconKind = MahApps.Metro.IconPacks.PackIconModernKind.PuzzleRound;
			this.Category = "Elements";
			this.Description = "Add a feature";
		}
	}
}
