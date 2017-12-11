/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Elements.Interfaces
{
	/// <summary>
	/// Description of IWorkspaceElement.
	/// </summary>
	public interface IWorkspaceElement
	{
		EditorElementType WorkspaceType { get; set; }
	}
}
