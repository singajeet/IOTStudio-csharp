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
using IOTStudio.Core.Stores.Objects;
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
		private FlagsStore flagProvider;
		private AssembliesStore assemblyLoader;
		private FeaturesStore featureManager;
		private LayoutsStore layoutsStore;
		private DatabaseFactory dbFactory;
		private DefaultObjectsStore defaultObjectsStore;
		
		private Get()
		{
			Logger.Debug("Initiating Core Services...");
			
			nameProvider = new NamesStore();
			Logger.Debug("[{0}] has been initiated", nameProvider);
			
			flagProvider = new FlagsStore();
			Logger.Debug("[{0}] has been initiated", flagProvider);
			
			assemblyLoader = new AssembliesStore();
			Logger.Debug("[{0}] has been initiated", assemblyLoader);
			
			featureManager = new FeaturesStore();
			Logger.Debug("[{0}] has been initiated", featureManager);	

			layoutsStore = new LayoutsStore();
			Logger.Debug("[{0}] has been initiated", layoutsStore);
			
			dbFactory = DatabaseFactory.Instance;
			Logger.Debug("[{0}] has been initiated", dbFactory);		
			
			defaultObjectsStore = new DefaultObjectsStore();
			Logger.Debug("[{0}] has been initiated", defaultObjectsStore);
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
		
		public FlagsStore Flags{
			get{ return flagProvider; }
		}
		
		public AssembliesStore Assemblies{
			get { return assemblyLoader; }
		}
		
		public FeaturesStore Features{
			get { return featureManager; }
		}
		
		public LayoutsStore Layouts{
			get{ return layoutsStore; }
		}
		
		public DefaultObjectsStore Objects{
			get { return defaultObjectsStore; }
		}
	}
}
