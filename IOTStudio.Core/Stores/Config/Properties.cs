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
		private const string STORES_LAYOUTS_SECTION_NAME = "Stores/Layouts";		
		private const string DB_SECTION_NAME = "Database/Settings";
		private const string DB_LITE_SECTION_NAME = "Database/DBLiteSettings";
		private const string PKG_MANAGER_SETTINGS_SECTION_NAME = "PackageManager/Settings";
		private const string UPDATE_SERVICE_WEB_SECTION_NAME = "UpdateService/WebSource";
		private const string PKGS_FEATURES_SECTION_NAME = "Packages/FeatureSettings";
		private const string PKGS_PKGS_SECTION_NAME = "Packages/PackageSettings";
		
		
		public static class Features
		{
			public static string Get(string key)
			{
				return GetGet(key, PKGS_FEATURES_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, PKGS_FEATURES_SECTION_NAME);
			}
		}
		
		public static class Packages
		{
			public static string Get(string key)
			{
				return GetGet(key, PKGS_PKGS_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, PKGS_PKGS_SECTION_NAME);
			}
		}
		
		public static class UpdateServiceFromWeb{
			public static string Get(string key)
			{
				return GetGet(key, UPDATE_SERVICE_WEB_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, UPDATE_SERVICE_WEB_SECTION_NAME);
			}
		}
		
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
		
		public static class PackageManager
		{
			public static string Get(string key)
			{
				return GetGet(key, PKG_MANAGER_SETTINGS_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, PKG_MANAGER_SETTINGS_SECTION_NAME);
			}
		}
		
		public static class Layouts
		{
			public static string Get(string key)
			{
				return GetGet(key, STORES_LAYOUTS_SECTION_NAME);
			}
			
			public static void Set(string key, string value)
			{
				SetSet(key, value, STORES_LAYOUTS_SECTION_NAME);
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
