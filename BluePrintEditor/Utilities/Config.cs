/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 1/1/2018
 * Time: 1:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of Config.
	/// </summary>
	public static class Config
	{
		internal static NameValueCollection configSection;		
		private const string SETTINGS_SECTION_NAME = "Editor/Settings";
		private const string TOOLITEMS_SECTION_NAME = "Editor/ToolItems";
		private const string GRID_SECTION_NAME = "Editor/Grid";
		
		public static string GetSetting(string key)
		{
			return GetGet(key, SETTINGS_SECTION_NAME);
		}
		
		public static string GetToolItemSetting(string key)
		{
			return GetGet(key, TOOLITEMS_SECTION_NAME);
		}
		
		public static string GetGridSetting(string key)
		{
			return GetGet(key, GRID_SECTION_NAME);
		}
		
		internal static string GetGet(string key, string sectionName)
		{
			configSection = ConfigurationManager
									.GetSection(sectionName) as NameValueCollection;
				
			return configSection[key];
		}
	}
}
