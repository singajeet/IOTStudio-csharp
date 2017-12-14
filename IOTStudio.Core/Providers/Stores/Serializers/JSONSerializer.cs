/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 1:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace IOTStudio.Core.Providers.Stores.Serializers
{
	/// <summary>
	/// Description of JSONSerializer.
	/// </summary>
	public static class JSONSerializer
	{
		
		private static DataContractJsonSerializer serializer;
		private static FileStream stream;
	
		static JSONSerializer()
		{
			
		}
		#region IJSONSerializer implementation

		public static void Serialize(object instance, Type type, string filename)
		{
			if(serializer== null)
				serializer = new DataContractJsonSerializer(type);
			
			if(stream== null)
				stream = new FileStream(filename, FileMode.Append);
			
			using (stream) {
				serializer.WriteObject(stream, instance);
			}
			
		}

		public static object Deserialize<T>(Type type, string filename)
		{
			object instance = null;
			
			if(serializer== null)
				serializer = new DataContractJsonSerializer(type);
			
			if(stream== null)
				stream = new FileStream(filename, FileMode.Open);
			
			using (stream) {
				instance = serializer.ReadObject(stream);
			}
			return instance;
		}

		#endregion
	}
}
