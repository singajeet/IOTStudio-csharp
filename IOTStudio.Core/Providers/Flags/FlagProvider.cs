/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 6:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Providers.Logging;

namespace IOTStudio.Core.Providers.Flags
{
	/// <summary>
	/// Description of FlagProvider.
	/// </summary>
	public static class FlagProvider
	{
		private static Dictionary<string, bool> flags;
		
		public static Dictionary<string, bool> Flags {
			get { return flags; }
			set { flags= value; }
		}
		
		public static void RegisterFlag(string key, bool value)
		{
			if (Flags.ContainsKey(key)) {
				throw new Exception("Key provided is already registered for another object");
			}
			
			Flags.Add(key, value);
			Logger.Debug("New Flag {0} registered with default value as {1}", key, value);
		}
		
		public static bool ContainsKey(string key)
		{
			return Flags.ContainsKey(key);
		}
		
		public static bool GetFlagStatus(string key)
		{
			bool value = (bool)Flags[key];
			return value;
		}
		
		public static void SetFlagStatus(string key, bool value)
		{
			Flags[key] = value;
		}
		
		public static void UnregisterFlag(string key)
		{
			if (!Flags.ContainsKey(key)) {
				throw new Exception("No such key is registered");
			}
			
			Flags.Remove(key);
			Logger.Debug("Flag {0} has been unregistered from this provider", key);
		}
		
		static FlagProvider()
		{
			Flags = Flags ?? new Dictionary<string, bool>();
			Logger.Debug("Dictionary created for FlagProvider");
		}
	}
}
