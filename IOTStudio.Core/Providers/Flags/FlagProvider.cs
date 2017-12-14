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
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Properties;
using IOTStudio.Core.Serializers;

namespace IOTStudio.Core.Providers.Flags
{
	/// <summary>
	/// Description of FlagProvider.
	/// </summary>
	public class FlagProvider
	{
		private Dictionary<string, bool> flags;
		
		public Dictionary<string, bool> Flags {
			get { return flags; }
			set { flags= value; }
		}
		
		public void RegisterFlag(string key, bool value)
		{
			if (Flags.ContainsKey(key)) {
				throw new Exception("Key provided is already registered for another object");
			}
			
			Flags.Add(key, value);
			Logger.Debug("New Flag {0} registered with default value as {1}", key, value);
		}
		
		public bool ContainsKey(string key)
		{
			return Flags.ContainsKey(key);
		}
		
		public bool GetFlagStatus(string key)
		{
			bool value = (bool)Flags[key];
			return value;
		}
		
		public void SetFlagStatus(string key, bool value)
		{
			Flags[key] = value;
		}
		
		public void UnregisterFlag(string key)
		{
			if (!Flags.ContainsKey(key)) {
				throw new Exception("No such key is registered");
			}
			
			Flags.Remove(key);
			Logger.Debug("Flag {0} has been unregistered from this provider", key);
		}
		
		public FlagProvider()
		{
			string flagProviderPath = PropertyProvider.FlagProvider.GetProperty("FlagProviderPath") as string;
			
			if (System.IO.File.Exists(flagProviderPath + @"\FlagProvider.json"))
				LoadFlags();
			
			Flags = Flags ?? new Dictionary<string, bool>();
			
			Logger.Debug("Dictionary created for FlagProvider");
		}
		
		private void LoadFlags()
		{
			string flagProviderPath = PropertyProvider.FlagProvider.GetProperty("FlagProviderPath") as string;
			
			Logger.Debug("FlagProvider will be deserialized from the following file: {0}", flagProviderPath + @"\FlagProvider.json");
			
			Flags = NewtonsoftJSONSerializer.Deserialize(flagProviderPath + @"\FlagProvider.json", typeof(Dictionary<string, bool>)) as Dictionary<string, bool>;
		}
		
		public void SaveFlags()
		{
			string flagProviderPath = PropertyProvider.FlagProvider.GetProperty("FlagProviderPath") as string;
			
			Logger.Debug("FlagProvider will be serialized to the following file: {0}", flagProviderPath + @"\FlagProvider.json");
			
			NewtonsoftJSONSerializer.Serialize(Flags, flagProviderPath + @"\FlagProvider.json", typeof(Dictionary<string, bool>));
		}

		~FlagProvider()
			{
				SaveFlags();
			}
		
	}
}
