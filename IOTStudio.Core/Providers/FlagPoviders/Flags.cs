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
using IOTStudio.Core.Providers.Logging;
using System.Linq;
using IOTStudio.Core.Providers.Stores;

namespace IOTStudio.Core.Providers.FlagProviders
{
	
	public class Flag{
		public string Key { get; set;} 
		public bool Value { get; set;}
		
		public Flag(string key, bool value){
		Key= key;
		Value= value;
		}
	}
	
	/// <summary>
	/// Description of FlagProvider.
	/// </summary>
	public class Flags : IProvider
	{
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

		private ObservableCollection<Flag> allFlags;
		
		public ObservableCollection<Flag> AllFlags {
			get { return allFlags; }
			set { allFlags= value; }
		}
		
		public void RegisterFlag(string key, bool value)
		{
			if (this.ContainsKey(key)) {
				throw new Exception("Key provided is already registered for another object");
			}
			
			AllFlags.Add(new Flag(key, value));
			Logger.Debug("New Flag {0} registered with default value as {1}", key, value);
		}
		
		public bool ContainsKey(string key)
		{
			return AllFlags.Where(f => f.Key.Equals(key)).ToList().Count > 0;
		}
		
		public bool GetFlagStatus(string key)
		{
			bool value = AllFlags.Where(f => f.Key.Equals(key))
				.ToList().SingleOrDefault().Value;
			return value;
		}
		
		public void SetFlagStatus(string key, bool value)
		{
			AllFlags.Where(f => f.Key.Equals(key))
				.ToList().SingleOrDefault().Value = value;
		}
		
		public void UnregisterFlag(string key)
		{
			if (!this.ContainsKey(key)) {
				throw new Exception("No such key is registered");
			}
			
			Flag flag = AllFlags.Where(f => f.Key.Equals(key))
				.ToList().SingleOrDefault();
			AllFlags.Remove(flag);
			Logger.Debug("Flag {0} has been unregistered from this provider", key);
		}
		
		public Flags()
		{
			string flagProviderPath = Properties.Flags.Get("FlagProviderPath") as string;
			
			if (System.IO.File.Exists(flagProviderPath + @"\FlagProvider.json"))
				LoadFlags();
			
			AllFlags = AllFlags ?? new ObservableCollection<Flag>();
			
			Logger.Debug("Dictionary created for FlagProvider");
		}
		
		private void LoadFlags()
		{
			string flagProviderPath = Properties.Flags.Get("FlagProviderPath") as string;
			
			Logger.Debug("FlagProvider will be deserialized from the following file: {0}", flagProviderPath + @"\FlagProvider.json");
			
			AllFlags = Get.i.JSONSerializer.Deserialize(flagProviderPath + @"\FlagProvider.json", typeof(ObservableCollection<Flag>)) as ObservableCollection<Flag>;
		}
		
		public void SaveFlags()
		{
			string flagProviderPath = Properties.Flags.Get("FlagProviderPath") as string;
			
			Logger.Debug("FlagProvider will be serialized to the following file: {0}", flagProviderPath + @"\FlagProvider.json");
			
			Get.i.JSONSerializer.Serialize(AllFlags, flagProviderPath + @"\FlagProvider.json", typeof(Dictionary<string, bool>));
		}

		~Flags()
			{
				SaveFlags();
			}
		
	}
}
