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
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Elements.Editor
{
	/// <summary>
	/// Description of BaseProjectElement.
	/// </summary>
	[DataContract]
	public class BaseProjectElement : BaseElement, IProjectElement
	{
		private EditorElementType projectType;
		
		#region IProjectElement implementation
		[DataMember]
		public EditorElementType ProjectType {
			get {
				return projectType;
			}
			set {
				projectType = value;
				OnPropertyChanged();
			}
		}
		#endregion
		
	}
}
