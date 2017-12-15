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

namespace IOTStudio.Core.Stores.Config
{
	/// <summary>
	/// Description of PropertyProvider.
	/// </summary>
	public static class Properties
	{
		internal static NameValueCollection configSection;		
		private const string SERIALIZER_SECTION_NAME = "Serializers/JsonSerializerSettings";
		private const string LAYOUT_SELECTOR_SECTION_NAME = "Layouts/SelectorSettings";
		private const string LAYOUT_SECTION_NAME = "Layouts/LayoutSettings";
		private const string NAME_TABLE_SECTION_NAME = "Providers/NameTableSettings";
		private const string FLAGS_SECTION_NAME = "Providers/FlagProviderSettings";
		private const string FEATURES_SECTION_NAME = "Features/Settings";
		private const string DB_SECTION_NAME = "Database/Settings";
		private const string DB_LITE_SECTION_NAME = "Database/DBLiteSettings";
		
		public static class Serializer{
			public static string Get(string key)
			{
				return GetGet(key, SERIALIZER_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, SERIALIZER_SECTION_NAME);
			}
		}
		
		public static class DB{
			public static string Get(string key)
			{
				return GetGet(key, DB_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, DB_SECTION_NAME);
			}
		}
		
		public static class DBLite{
			public static string Get(string key)
			{
				return GetGet(key, DB_LITE_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, DB_LITE_SECTION_NAME);
			}
		}
		
		public static class LayoutSelector
		{
			public static string Get(string key)
			{
				return GetGet(key, LAYOUT_SELECTOR_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, LAYOUT_SELECTOR_SECTION_NAME);
			}
		}

		public static class Layout
		{
			public static string Get(string key)
			{
				return GetGet(key, LAYOUT_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, LAYOUT_SECTION_NAME);
			}
		}
		
		public static class Names
		{
			public static string Get(string key)
			{
				return GetGet(key, NAME_TABLE_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, NAME_TABLE_SECTION_NAME);
			} 
		}
		
		public static class Flags
		{
			public static string Get(string key)
			{
				return GetGet(key, FLAGS_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, FLAGS_SECTION_NAME);
			} 
		}
		
		public static class Features
		{
			public static string Get(string key)
			{
				return GetGet(key, FEATURES_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, FEATURES_SECTION_NAME);
			} 
		}
		
		internal static string GetGet(string key, string sectionName)
		{
			configSection = ConfigurationManager
									.GetSection(sectionName) as NameValueCollection;
				
			return configSection[key];
		}
		
		public static void SetSet(string key, string value, string sectionName)
		{
			configSection = ConfigurationManager
									.GetSection(sectionName) as NameValueCollection;
			configSection[key] = value;
		}
	}
}
