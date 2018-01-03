/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 1/1/2018
 * Time: 1:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using MahApps.Metro.IconPacks;

namespace BluePrintEditor.Designer.ToolBox.Interfaces
{
	/// <summary>
	/// Description of IToolItem.
	/// </summary>
	public interface IToolItem
	{
		Guid Id { get; set; }
		string Name { get; set; }
		string ToolTitle { get; set; }
		string Description { get; set; }
		Type ToolCommandType { get; set; }
		string Category { get; set; }
		string IconUri { get; set; }
		PackIconModernKind IconKind { get; set; }
	}
}
