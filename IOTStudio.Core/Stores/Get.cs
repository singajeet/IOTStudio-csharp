/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 9:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Database;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Stores.Providers;

namespace IOTStudio.Core.Stores
{
	/// <summary>
	/// Description of Get.
	/// </summary>
	public class Get
	{
		private static Get manager;
		
		private NamesStore nameProvider;
		private Flags flagProvider;
		private Assemblies assemblyLoader;
		private Features featureManager;
		private DatabaseFactory dbFactory;
		
		private Get()
		{
			Logger.Debug("Initiating Core Services...");
			
			nameProvider = new NamesStore();
			Logger.Debug("Names [{0}] has been initiated", nameProvider.Id);
			
			flagProvider = new Flags();
			Logger.Debug("FlagProvider [{0}] has been initiated", flagProvider.Id);
			
			assemblyLoader = new Assemblies();
			Logger.Debug("Assemblies [{0}] has been initiated", assemblyLoader.Id);
			
			featureManager = new Features();
			Logger.Debug("FeatureManager [{0}] has been initiated", featureManager.Id);			
			
			dbFactory = DatabaseFactory.Instance;
			Logger.Debug("Database Factory [{0}] has been initiated", dbFactory.Id);		
			
		}
		
		public static Get i
		{
			get {
				if (manager == null)
					manager = new Get();
			
				return manager;
			}
		}
		
		public DatabaseFactory DBFactory{
			get { return dbFactory; }
		}
		
		public NamesStore Names{
			get { return nameProvider; }
		}
		
		public Flags Flags{
			get{ return flagProvider; }
		}
		
		public Assemblies Assemblies{
			get { return assemblyLoader; }
		}
		
		public Features Features{
			get { return featureManager; }
		}
	}
}
