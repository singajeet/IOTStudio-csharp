/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 6:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IOTStudio.Core.Interfaces;
using System.Linq;
using IOTStudio.Core.Providers.Stores;
using LiteDB;

namespace IOTStudio.Core.Stores.Providers
{
	using Logger = IOTStudio.Core.Stores.Logs.Logger;
	
	public class Flag{
		public string Key;
		public bool Value;
		
		public Flag(string key, bool value){
			Key= key;
			Value= value;
		}
		
		public override string ToString()
		{
			return string.Format("[Flag Key={0}, Value={1}]", Key, Value);
		}

	}
	
	/// <summary>
	/// Description of FlagProvider.
	/// </summary>
	public class FlagsStore : BaseStore, IProvider
	{
		IDBDriver dbDriver;
		ObservableCollection<Flag> flags;
		
		#region IProvider implementation

		public Guid Id {
			get {
				return new Guid("8D7EE3C3-7FC4-44B1-AF77-6FA3D248DCAD");
			}
		}
	
		public string Name {
			get {
				return "Flags";
			}
		}
	
		#endregion

		public FlagsStore()
		{
			dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE);
			dbDriver.Connect();	
		}	
		
		private LiteCollection<Flag> _allFlags{
			get { 
				return dbDriver.DB.GetCollection<Flag>("flags");
			}
		}
		
		public ObservableCollection<Flag> AllFlags{
			get{ 
				if (flags == null || flags.Count != _allFlags.Count()) {
					flags = new ObservableCollection<Flag>();
					foreach (Flag flag in _allFlags.FindAll()) {
						flags.Add(flag);
					}
				}
				return flags;
			}
		}
		
		public void RegisterFlag(string key, bool value)
		{
			if (this.ContainsKey(key)) {
				throw new Exception("Key provided is already registered for another object");
			}
			
			Flag flag = new Flag(key, value);
			var flags = dbDriver.DB.GetCollection<Flag>("flags");
			flags.Insert(flag);
			
			Logger.Debug("New Flag {0} registered with default value as {1}", flag.Key, flag.Value);
		}
		
		public bool ContainsKey(string key)
		{
			return _allFlags.Exists(f => f.Key.Equals(key));
		}
		
		public bool GetFlagStatus(string key)
		{
			return _allFlags.FindOne(f => f.Key.Equals(key)).Value;
		}
		
		public void SetFlagStatus(string key, bool value)
		{
			Flag flag = _allFlags.FindOne(f => f.Key.Equals(key));
			flag.Value = value;
			_allFlags.Update(flag);
			
			Logger.Debug("Flag {0} status changed to {1}", flag.Key, flag.Value);
		}
		
		public void UnregisterFlag(string key)
		{
			if (!this.ContainsKey(key)) {
				throw new Exception("No such key is registered");
			}				
			
			_allFlags.Delete(f => f.Key.Equals(key));
			Logger.Debug("Flag {0} has been unregistered from this provider", key);
		}	
			
		public override string ToString()
		{
			return string.Format("[FlagsStore Id={0}, Name={1}]", Id, Name);
		}

	}
}
