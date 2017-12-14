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
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Properties;

namespace IOTStudio.Core.Providers
{
	/// <summary>
	/// Description of Names.
	/// </summary>
	[DataContract]
	public class Names : IProvider
	{
		
		private static Dictionary<string, int> nameTable = new Dictionary<string, int>();

		#region IProvider implementation
		public Guid Id {
			get {
				return new Guid("06DFF8DC-53EA-45DB-AD84-35B249ACEC77");
			}
		}
		public string Name {
			get {
				return "Names";
			}
		}
		#endregion		
		public Names()
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
			
			NameTable = Get.i.JSONSerializer.Deserialize(nameTablePath + @"\NameTable.json", typeof(Dictionary<string, int>)) as Dictionary<string, int>;
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
			
			Get.i.JSONSerializer.Serialize(NameTable, nameTablePath + @"\NameTable.json", typeof(Dictionary<string, int>));
		}

		~Names()
			{
				SaveNameTable();
			}
		
	}
}
