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
	public class Name
	{
		public string Key;
		public int Counter;
		
		public Name(string key, int counter)
		{
			Key = key;
			Counter = counter;
		}
		
		public override string ToString()
		{
			return string.Format("[Name Key={0}, Counter={1}]", Key, Counter);
		}

	}
	
	/// <summary>
	/// Description of Names.
	/// </summary>
	[DataContract]
	public class NamesStore : BaseStore, IProvider
	{
		readonly IDBDriver dbDriver;
		
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
			dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE);
			dbDriver.Connect();			
		}
		
		public string GetName(string key)
		{
			var names = dbDriver.DB.GetCollection<Name>("names");
			
			if (names.Exists(n => n.Key.Equals(key))) {				
				Name name = names.FindOne(n => n.Key.Equals(key));
				name.Counter++;				
				names.Update(name);
				
				Logger.Debug("Key [{0}] already exists and next counter value [{1}] will be returned", name.Key, name.Counter);
				
				return name.Key + "_" + name.Counter;
			} else {				
				Name name = new Name(key, 1);
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
