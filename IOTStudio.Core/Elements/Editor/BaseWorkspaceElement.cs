/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Types;
using Newtonsoft.Json;

namespace IOTStudio.Core.Elements.Editor
{
	/// <summary>
	/// Description of BaseWorkspaceElement.
	/// </summary>
	[DataContract]
	public class BaseWorkspaceElement : BaseElement, IWorkspaceElement
	{
		public static readonly DependencyProperty WorkspaceTypeProperty
														= DependencyProperty
															.Register("WorkspaceType", 
								          						typeof(EditorElementType), typeof(BaseWorkspaceElement));
		
		#region IWorkspaceElement implementation
		[DataMember]
		public EditorElementType WorkspaceType {
			get {
				return (EditorElementType)GetValue(WorkspaceTypeProperty);
			}
			set {
				SetValue(WorkspaceTypeProperty, value);
			}
		}
		#endregion
	}
}
