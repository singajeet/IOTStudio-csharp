/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 5:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.Serialization;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Stores;
using LiteDB;

namespace IOTStudio.Core.Stores.Providers
{
	using Logger = IOTStudio.Core.Stores.Logs.Logger;
	
	/// <summary>
	/// Name object representing an entry in Names collection
	/// </summary>
	public class NameRecord
	{
		public ObjectId Id { get; set; }
		public string Key { get; set; }
		public int Counter { get; set; }
		
		public NameRecord()
		{
		}
		
		public NameRecord(string key, int counter)
		{
			Key = key;
			Counter = counter;
		}
		
		public override string ToString()
		{
			return string.Format("[NameRecord Id={0}, Key={1}, Counter={2}]", Id, Key, Counter);
		}

	}
	
	/// <summary>
	/// Description of Names.
	/// </summary>
	[DataContract]
	public class NamesStore : BaseStore, IProvider
	{
		IDBDriver dbDriver;
		
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
		
		public NamesStore()
		{
			
		}
		
		public string GetName(string key)
		{
			if (dbDriver == null) {
				dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE_SCHEMA);
				dbDriver.Connect();		
			}			
			
			var names = dbDriver.DB.GetCollection<NameRecord>(NAMES_COLLECTION);
			
			if (names.Exists(n => n.Key.Equals(key))) {				
				NameRecord name = names.FindOne(n => n.Key.Equals(key));
				name.Counter++;				
				names.Update(name);
				
				Logger.Debug("Key [{0}] already exists and next counter value [{1}] will be returned", name.Key, name.Counter);
				
				return name.Key + "_" + name.Counter;
			} else {				
				NameRecord name = new NameRecord(key, 1);
				names.Insert(name);
				
				Logger.Debug("Key [{0}] doesn't exists in collection; Creating new entry with counter value [{1}]", name.Key, name.Counter);
				
				return name.Key + "_" + name.Counter;
			}
		}
		
		public override string ToString()
		{
			return string.Format("[NamesStore Id={0}, Name={1}]", Id, Name);
		}

	}
}
