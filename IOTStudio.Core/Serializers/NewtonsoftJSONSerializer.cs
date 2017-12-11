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
		private static StreamWriter streamOut;
		private static StreamReader streamIn;
		private static JsonWriter jsonWriter;
		private static JsonReader jsonReader;
		
		public static void Serialize(object instance, string filename)
		{
			serializer = new JsonSerializer();
			streamOut = new StreamWriter(filename);
			jsonWriter = new JsonTextWriter(streamOut);
			
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
		
		public static object Deserialize(string filename)
		{
			object instance = null;
			serializer = new JsonSerializer();
			streamIn = new StreamReader(filename);
			jsonReader = new JsonTextReader(streamIn);
			
			serializer.PreserveReferencesHandling = (PreserveReferencesHandling)Enum.Parse(typeof(PreserveReferencesHandling),
			                                                   PropertyProvider.Serializer
			                                                   .GetProperty("PreserveReferencesHandling") as String);
			serializer.ReferenceLoopHandling = (ReferenceLoopHandling)Enum.Parse(typeof(ReferenceLoopHandling),
			                                                   PropertyProvider.Serializer
			                                                   .GetProperty("ReferenceLoopHandling") as String);
			
			using (jsonReader) {
				instance = serializer.Deserialize(jsonReader);
			}
			
			return instance;
		}
	}
}
