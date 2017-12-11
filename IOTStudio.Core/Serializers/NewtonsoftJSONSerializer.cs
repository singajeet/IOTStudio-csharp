/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 6:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using IOTStudio.Core.Providers.Properties;
using Newtonsoft.Json;

namespace IOTStudio.Core.Serializers
{
	/// <summary>
	/// Description of NewtonsoftJSONSerializer.
	/// </summary>
	public static class NewtonsoftJSONSerializer
	{
		private static JsonSerializer serializer;
		private static StreamWriter stream;
		private static JsonWriter jsonWriter;
		
		public static void Serialize(object instance, string filename)
		{
			serializer = new JsonSerializer();
			stream = new StreamWriter(filename);
			jsonWriter = new JsonTextWriter(stream);
			
			serializer.PreserveReferencesHandling = (PreserveReferencesHandling)Enum.Parse(typeof(PreserveReferencesHandling),
			                                                   PropertyProvider.Serializer
			                                                   .GetProperty("PreserveReferencesHandling") as String);
			serializer.ReferenceLoopHandling = (ReferenceLoopHandling)Enum.Parse(typeof(ReferenceLoopHandling),
			                                                   PropertyProvider.Serializer
			                                                   .GetProperty("ReferenceLoopHandling") as String);
			
			using (jsonWriter) {
				serializer.Serialize(jsonWriter, instance);
			}
		}
	}
}
