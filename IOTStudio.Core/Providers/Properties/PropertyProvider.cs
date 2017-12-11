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
		
		public static class Serializer{
			public static string GetProperty(string key)
			{
				configSection = ConfigurationManager
									.GetSection(SERIALIZER_SECTION_NAME) as NameValueCollection;
				
				return configSection[key];
			}
			
			public static void SetProperty(string key, string value)
			{
				configSection = ConfigurationManager
									.GetSection(SERIALIZER_SECTION_NAME) as NameValueCollection;
				configSection[key] = value;
			}
		}
	}
}
