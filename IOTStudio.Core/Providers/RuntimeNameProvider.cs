/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 5:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Runtime.Serialization;
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Properties;
using IOTStudio.Core.Serializers;

namespace IOTStudio.Core.Providers
{
	/// <summary>
	/// Description of RuntimeNameProvider.
	/// </summary>
	[DataContract]
	public static class RuntimeNameProvider
	{
		private static ObjectFinalizer finalizer = new ObjectFinalizer();
		private static Hashtable nameTable = new Hashtable();
		
		static RuntimeNameProvider()
		{
			LoadNameTable();
		}
		
		[DataMember]
		private static Hashtable NameTable{
			get { return nameTable; }
			set { nameTable = value; }
		}

		private static void LoadNameTable()
		{
			string nameTablePath = PropertyProvider.NameProvider.GetProperty("NameTablePath") as string;
			
			Logger.Debug("NameTable will be deserialized from the following file: {0}", nameTablePath + @"\NameTable.json");
			
			NameTable = NewtonsoftJSONSerializer.Deserialize(nameTablePath + @"\NameTable.json") as Hashtable;
		}
		
		public static string GetName(string key)
		{
			if (nameTable.ContainsKey(key)) {
				
				int value = (int)NameTable[key];
				value++;
				NameTable[key] = value;
				
				return key + "_" + value;
			} else {
				NameTable[key] = 1;
				return key + "_1";
			}
		}
		
		public static void SaveNameTable()
		{
			string nameTablePath = PropertyProvider.NameProvider.GetProperty("NameTablePath") as string;
			
			Logger.Debug("NameTable will be serialized to the following file: {0}", nameTablePath + @"\NameTable.json");
			
			NewtonsoftJSONSerializer.Serialize(NameTable, nameTablePath + @"\NameTable.json");
		}

		private class ObjectFinalizer
		{
			~ObjectFinalizer()
			{
				SaveNameTable();
			}
		}
	}
}
