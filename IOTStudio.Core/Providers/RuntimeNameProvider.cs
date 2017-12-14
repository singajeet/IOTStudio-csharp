/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 5:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
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
	public class RuntimeNameProvider
	{
		
		private static Dictionary<string, int> nameTable = new Dictionary<string, int>();
		
		public RuntimeNameProvider()
		{
			string nameTablePath = PropertyProvider.NameProvider.GetProperty("NameTablePath") as string;
			
			if(System.IO.File.Exists(nameTablePath + @"\NameTable.json"))
				LoadNameTable();
			
			NameTable = NameTable ?? new Dictionary<string, int>();
		}
		
		[DataMember]
		private Dictionary<string, int> NameTable{
			get { return nameTable; }
			set { nameTable = value; }
		}

		private void LoadNameTable()
		{
			string nameTablePath = PropertyProvider.NameProvider.GetProperty("NameTablePath") as string;
			
			Logger.Debug("NameTable will be deserialized from the following file: {0}", nameTablePath + @"\NameTable.json");
			
			NameTable = NewtonsoftJSONSerializer.Deserialize(nameTablePath + @"\NameTable.json", typeof(Dictionary<string, int>)) as Dictionary<string, int>;
		}
		
		public string GetName(string key)
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
		
		public void SaveNameTable()
		{
			string nameTablePath = PropertyProvider.NameProvider.GetProperty("NameTablePath") as string;
			
			Logger.Debug("NameTable will be serialized to the following file: {0}", nameTablePath + @"\NameTable.json");
			
			NewtonsoftJSONSerializer.Serialize(NameTable, nameTablePath + @"\NameTable.json", typeof(Dictionary<string, int>));
		}

		~RuntimeNameProvider()
			{
				SaveNameTable();
			}
		
	}
}
