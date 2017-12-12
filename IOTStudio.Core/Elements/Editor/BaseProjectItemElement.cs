/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 9:58 PM
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
	/// Description of BaseProjectItemElement.
	/// </summary>
	[DataContract]
	public class BaseProjectItemElement : BaseElement, IProjectItemElement
	{
		private EditorElementType projectItemType;
		
		#region IProjectItemElement implementation
		[DataMember]
		public EditorElementType ProjectItemType {
			get {
				return projectItemType;
			}
			set {
				projectItemType= value;
				OnPropertyChanged();
			}
		}
		#endregion
	}
}
