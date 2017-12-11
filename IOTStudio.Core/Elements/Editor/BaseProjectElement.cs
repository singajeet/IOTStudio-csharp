/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:57 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.Serialization;
using System.Windows;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Types;
using Newtonsoft.Json;

namespace IOTStudio.Core.Elements.Editor
{
	/// <summary>
	/// Description of BaseProjectElement.
	/// </summary>
	[DataContract]
	public class BaseProjectElement : BaseElement, IProjectElement
	{
		public static readonly DependencyProperty ProjectTypeProperty
														= DependencyProperty
															.Register("ProjectType", 
								          						typeof(EditorElementType), typeof(BaseProjectElement));
		
		#region IProjectElement implementation
		[DataMember]
		public EditorElementType ProjectType {
			get {
				return (EditorElementType)GetValue(ProjectTypeProperty);
			}
			set {
				SetValue(ProjectTypeProperty, value);
			}
		}
		#endregion
		
	}
}
