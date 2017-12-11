/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 10:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace IOTStudio.Core.Providers.Properties
{
	/// <summary>
	/// Description of PropertyProvider.
	/// </summary>
	public static class PropertyProvider
	{
		internal static NameValueCollection configSection;		
		private const string SERIALIZER_SECTION_NAME = "Serializers/JsonSerializerSettings";
		private const string LAYOUT_SELECTOR_SECTION_NAME = "Layouts/SelectorSettings";
		private const string LAYOUT_SECTION_NAME = "Layouts/LayoutSettings";
		
		public static class Serializer{
			public static string GetProperty(string key)
			{
				return GetPropertyFromSection(key, SERIALIZER_SECTION_NAME);
			}
			
			public static void SetProperty(string key, string value)
			{
				SetPropertyOfSection(key, value, SERIALIZER_SECTION_NAME);
			}
		}
		
		public static class LayoutSelector
		{
			public static string GetProperty(string key)
			{
				return GetPropertyFromSection(key, LAYOUT_SELECTOR_SECTION_NAME);
			}
			
			public static void SetProperty(string key, string value)
			{
				SetPropertyOfSection(key, value, LAYOUT_SELECTOR_SECTION_NAME);
			}
		}

		public static class Layout
		{
			public static string GetProperty(string key)
			{
				return GetPropertyFromSection(key, LAYOUT_SECTION_NAME);
			}
			
			public static void SetProperty(string key, string value)
			{
				SetPropertyOfSection(key, value, LAYOUT_SECTION_NAME);
			}
		}
		
		internal static string GetPropertyFromSection(string key, string sectionName)
		{
			configSection = ConfigurationManager
									.GetSection(sectionName) as NameValueCollection;
				
			return configSection[key];
		}
		
		public static void SetPropertyOfSection(string key, string value, string sectionName)
		{
			configSection = ConfigurationManager
									.GetSection(sectionName) as NameValueCollection;
			configSection[key] = value;
		}
	}
}
