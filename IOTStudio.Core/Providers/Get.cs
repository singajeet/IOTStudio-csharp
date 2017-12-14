/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 9:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.FlagProviders;
using IOTStudio.Core.Providers.Runtime;
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Stores;
using IOTStudio.Core.Providers.Stores.Database;
using IOTStudio.Core.Providers.Stores.Serializers;

namespace IOTStudio.Core.Providers
{
	/// <summary>
	/// Description of Get.
	/// </summary>
	public class Get
	{
		private static Get manager;
		
		private Names nameProvider;
		private Flags flagProvider;
		private Assemblies assemblyLoader;
		private Features featureManager;
		private NewtonsoftJSONSerializer jsonSerializer;
		private IDatabase database;
		private DataStore dataStore;
		
		private Get()
		{
			Logger.Debug("Initiating Core Services...");
			
			nameProvider = new Names();
			Logger.Debug("Names [{0}] has been initiated", nameProvider.Id);
			
			flagProvider = new Flags();
			Logger.Debug("FlagProvider [{0}] has been initiated", flagProvider.Id);
			
			assemblyLoader = new Assemblies();
			Logger.Debug("Assemblies [{0}] has been initiated", assemblyLoader.Id);
			
			featureManager = new Features();
			Logger.Debug("FeatureManager [{0}] has been initiated", featureManager.Id);
			
			jsonSerializer = new NewtonsoftJSONSerializer();
			Logger.Debug("NewtonsoftJSONSerializer [{0}] has been initiated", jsonSerializer.Id);
			
			string configuredDatabase = Properties.DB.Get("ActiveDatabase");
			database = DatabaseFactory.Instance.LoadDatabase(configuredDatabase);
			Logger.Debug("Database [{0}] has been initiated", configuredDatabase);
			
			dataStore = new DataStore(database);
			Logger.Debug("DataStore [{0}] has been initiated", dataStore.Id);
		}
		
		public static Get i
		{
			get {
				if (manager == null)
					manager = new Get();
			
				return manager;
			}
		}
		
		public IDatabase DB{
			get { return database; }
		}
		
		public NewtonsoftJSONSerializer JSONSerializer{
			 get {
				return jsonSerializer;
			}
		}
		
		public Names Names{
			get {
				return nameProvider;
			}
		}
		
		public Flags Flags{
			get{ return flagProvider; }
		}
		
		public Assemblies Assemblies{
			get { return assemblyLoader; }
		}
		
		public FeatureManager Features{
			get { return featureManager; }
		}
	}
}
