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
		private PackagesStore pkgStore;
		
		private Get()
		{
			Logger.Debug("ProviderManager [Get()] instance created");
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
			get {
				if (dbFactory == null) {
					dbFactory = DatabaseFactory.Instance;
					Logger.Debug("[{0}] has been initiated", dbFactory);		
				}
				return dbFactory; 
			}
		}
		
		public NamesStore Names{
			get { 
				if (nameProvider == null) {
					nameProvider = new NamesStore();
					Logger.Debug("[{0}] has been initiated", nameProvider);
				}
				
				return nameProvider; 
			}
		}
		
		public FlagsStore Flags{
			get{
				if (flagProvider == null) {
					flagProvider = new FlagsStore();
					Logger.Debug("[{0}] has been initiated", flagProvider);				
				}
				return flagProvider; 
			}
		}
		
		public AssembliesStore Assemblies{
			get {
				if (assemblyLoader == null) {
					assemblyLoader = new AssembliesStore();
					Logger.Debug("[{0}] has been initiated", assemblyLoader);
				}
				return assemblyLoader; 
			}
		}
		
		public FeaturesStore Features{
			get { 
				if (featureManager == null) {
					featureManager = new FeaturesStore();
					Logger.Debug("[{0}] has been initiated", featureManager);	
				}
				return featureManager; 
			}
		}
		
		public LayoutsStore Layouts{
			get{ 
				if (layoutsStore == null) {
					layoutsStore = new LayoutsStore();
					Logger.Debug("[{0}] has been initiated", layoutsStore);
				}
				return layoutsStore; 
			}
		}
		
		public PackagesStore PackagesStore{
			get { 
				if (pkgStore == null) {
					pkgStore = new PackagesStore();
					Logger.Debug("[{0}] has been initiated", pkgStore);
				}
				return pkgStore;
			}
		}
		
		public DefaultObjectsStore Objects{
			get {
				if (defaultObjectsStore == null) {
					defaultObjectsStore = new DefaultObjectsStore();
					Logger.Debug("[{0}] has been initiated", defaultObjectsStore);
				}
				return defaultObjectsStore; 
			}
		}
	}
}
