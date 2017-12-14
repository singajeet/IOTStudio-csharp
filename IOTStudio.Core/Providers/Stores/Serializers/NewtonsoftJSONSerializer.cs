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
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Logging;
using Newtonsoft.Json;

namespace IOTStudio.Core.Providers.Stores.Serializers
{
	/// <summary>
	/// Description of NewtonsoftJSONSerializer.
	/// </summary>
	public class NewtonsoftJSONSerializer : IProvider
	{
		private JsonSerializer serializer;
		private StreamWriter streamOut;
		private StreamReader streamIn;
		private JsonWriter jsonWriter;
		private JsonReader jsonReader;

		#region IProvider implementation
		public Guid Id {
			get {
				return new Guid("CAB5251E-1724-45F0-9037-364BF6683858");
			}
		}
		public string Name {
			get {
				return "NewtonsoftJSONSerializer";
			}
		}
		#endregion		
		public void Serialize(object instance, string filename)
		{
			Logger.Debug("Serializing object [{0}] to file [{1}] in JSON format", instance.GetType().FullName, filename);
			
			serializer = new JsonSerializer();
			streamOut = new StreamWriter(filename);
			jsonWriter = new JsonTextWriter(streamOut);
			
			serializer.PreserveReferencesHandling = (PreserveReferencesHandling)Enum.Parse(typeof(PreserveReferencesHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("PreserveReferencesHandling") as String);
			serializer.ReferenceLoopHandling = (ReferenceLoopHandling)Enum.Parse(typeof(ReferenceLoopHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("ReferenceLoopHandling") as String);
			
			using (jsonWriter) {
				serializer.Serialize(jsonWriter, instance);
			}		
			Logger.Debug("Serializing of object [{0}] has been completed", instance.GetType().FullName, filename);
		}
		
		public void Serialize(object instance, string filename, Type type)
		{
			Logger.Debug("Serializing object [{0}] to file [{1}] in JSON format", instance.GetType().FullName, filename);
			
			serializer = new JsonSerializer();
			streamOut = new StreamWriter(filename);
			jsonWriter = new JsonTextWriter(streamOut);
			
			serializer.PreserveReferencesHandling = (PreserveReferencesHandling)Enum.Parse(typeof(PreserveReferencesHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("PreserveReferencesHandling") as String);
			serializer.ReferenceLoopHandling = (ReferenceLoopHandling)Enum.Parse(typeof(ReferenceLoopHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("ReferenceLoopHandling") as String);
			
			using (jsonWriter) {
				serializer.Serialize(jsonWriter, instance, type);
			}		
			Logger.Debug("Serializing of object [{0}] has been completed", instance.GetType().FullName, filename);
		}
		
		public object Deserialize(string filename)
		{
			Logger.Debug("Deserializing object from file [{0}] stored in JSON format", filename);
			
			object instance = null;
			serializer = new JsonSerializer();
			streamIn = new StreamReader(filename);
			jsonReader = new JsonTextReader(streamIn);
			
			serializer.PreserveReferencesHandling = (PreserveReferencesHandling)Enum.Parse(typeof(PreserveReferencesHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("PreserveReferencesHandling") as String);
			serializer.ReferenceLoopHandling = (ReferenceLoopHandling)Enum.Parse(typeof(ReferenceLoopHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("ReferenceLoopHandling") as String);
			
			using (jsonReader) {
				instance = serializer.Deserialize(jsonReader);
				
			}
			
			Logger.Debug("Deserialization of object from file [{0}] has been completed!", filename);
			
			return instance;
		}
		
		public object Deserialize(string filename, Type type)
		{
			Logger.Debug("Deserializing object from file [{0}] stored in JSON format", filename);
			
			object instance = null;
			serializer = new JsonSerializer();
			streamIn = new StreamReader(filename);
			jsonReader = new JsonTextReader(streamIn);
			
			serializer.PreserveReferencesHandling = (PreserveReferencesHandling)Enum.Parse(typeof(PreserveReferencesHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("PreserveReferencesHandling") as String);
			serializer.ReferenceLoopHandling = (ReferenceLoopHandling)Enum.Parse(typeof(ReferenceLoopHandling),
			                                                   Properties.Serializer
			                                                   .GetProperty("ReferenceLoopHandling") as String);
			
			using (jsonReader) {
				instance = serializer.Deserialize(jsonReader, type);				
			}
			
			Logger.Debug("Deserialization of object from file [{0}] has been completed!", filename);
			
			return instance;
		}
	}
}
