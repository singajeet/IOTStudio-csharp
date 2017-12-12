/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.Serialization;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Elements.Editor
{
	/// <summary>
	/// Description of BaseWorkspaceElement.
	/// </summary>
	[DataContract]
	public class BaseWorkspaceElement : BaseElement, IWorkspaceElement
	{
		private EditorElementType workspaceType; 
		
		#region IWorkspaceElement implementation
		[DataMember]
		public EditorElementType WorkspaceType {
			get {
				return workspaceType;
			}
			set {
				workspaceType= value;
				OnPropertyChanged();	
			}
		}
		#endregion
	}
}
