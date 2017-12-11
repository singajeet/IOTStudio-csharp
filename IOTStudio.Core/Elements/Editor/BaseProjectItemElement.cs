/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:58 PM
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
	/// Description of BaseProjectItemElement.
	/// </summary>
	[DataContract]
	public class BaseProjectItemElement : BaseElement, IProjectItemElement
	{
		public static readonly DependencyProperty ProjectItemTypeProperty
														= DependencyProperty
															.Register("ProjectItemType", 
								          						typeof(EditorElementType), typeof(BaseProjectItemElement));
		
		#region IProjectItemElement implementation
		[DataMember]
		public EditorElementType ProjectItemType {
			get {
				return (EditorElementType)GetValue(ProjectItemTypeProperty);
			}
			set {
				SetValue(ProjectItemTypeProperty, value);
			}
		}
		#endregion
	}
}
