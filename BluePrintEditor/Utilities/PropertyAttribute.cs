/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 10:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of PropertyAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
	public class PropertyAttribute:  Attribute
	{
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }
		
		public PropertyAttribute()
		{
		}
		
		public PropertyAttribute(string name, string category=null, string description=null)
		{
			Name = name;
			Category = category;
			Description = description;
		}
	}
}
