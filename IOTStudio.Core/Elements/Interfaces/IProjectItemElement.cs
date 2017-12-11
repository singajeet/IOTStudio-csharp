/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Elements.Interfaces
{
	/// <summary>
	/// Description of IProjectItemElement.
	/// </summary>
	public interface IProjectItemElement
	{
		EditorElementType ProjectItemType { get; set; }
	}
}
