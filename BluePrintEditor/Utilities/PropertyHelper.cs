/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 10:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of PropertyHelper.
	/// </summary>
	public sealed class PropertyHelper
	{
		private static PropertyHelper instance = new PropertyHelper();
		
		public static PropertyHelper Instance {
			get {
				return instance;
			}
		}
		
		private PropertyHelper()
		{
		}
		
//		public Dictionary<string, PropertyMetadata> GetProperties(object obj)
//		{
//			Dictionary<string, PropertyMetadata> keyValue = new Dictionary<string, PropertyMetadata>();
//			
//			PropertyInfo[] properties = obj.GetType().GetProperties();
//			for (int i = 0; i < properties.Length; i++) {
//				PropertyAttribute attr = (PropertyAttribute)Attribute.GetCustomAttribute(properties[i], typeof(PropertyAttribute));
//				
//				if (attr != null) {
//					PropertyMetadata meta = new PropertyMetadata();
//					meta.Name = attr.Name ?? properties[i].Name;
//					meta.Category = attr.Category;
//					meta.Description = attr.Description;
//					if (properties[i].GetIndexParameters().Length == 0) {
//						object val = properties[i].GetValue(obj);
//						meta.Value = properties[i].PropertyType.IsValueType ? properties[i].GetValue(obj) as string : string.Format("PropType: {0}, Prop: {1}", properties[i].PropertyType, properties[i].ToString());
//					} else {
//						meta.Value = string.Format("IndexParms:{0}, Prop:{1}", properties[i].GetIndexParameters().Length, properties[i].ToString());
//					}
//				
//					keyValue.Add(properties[i].Name, meta);
//				}
//			}
//			return keyValue;
//		}
	}
	
//	public class PropertyMetadata
//	{
//		public string Name{ get; set; }
//		public string Value{ get; set; }
//		public string Category { get; set; }
//		public string Description { get; set; }
//	}
}
