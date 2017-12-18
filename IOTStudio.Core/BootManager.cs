/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 5:36 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Packages;

namespace IOTStudio.Core
{
	/// <summary>
	/// Description of BootManager.
	/// </summary>
	public sealed class BootManager
	{
		private static BootManager instance = new BootManager();		
		private PackageManager pkgManager;
		
		public static BootManager Instance {
			get {
				return instance;
			}
		}
		
		private BootManager()
		{
			Logger.Debug("BootManager instance has been created");
		}
		
		public void StartServices()
		{
			Logger.Debug("Starting core services now...");
			
			pkgManager = PackageManager.Instance;
			
			pkgManager.StartFileSystemWatcher();
			
			
		}
	}
}
