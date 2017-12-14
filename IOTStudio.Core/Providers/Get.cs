/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 9:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Providers.Runtime;
using IOTStudio.Core.Providers.DataStore.Serializers;
using IOTStudio.Core.Providers.Flags;
using IOTStudio.Core.Providers.Logging;

namespace IOTStudio.Core.Providers
{
	/// <summary>
	/// Description of Get.
	/// </summary>
	public class Get
	{
		private static Get manager;
		
		private Names nameProvider;
		private FlagProvider flagProvider;
		private Assemblies assemblyLoader;
		private FeatureManager featureManager;
		private NewtonsoftJSONSerializer jsonSerializer;
		
		private Get()
		{
			Logger.Debug("Initiating Core Services...");
			
			nameProvider = new Names();
			Logger.Debug("Names [{0}] has been initiated", nameProvider.Id);
			
			flagProvider = new FlagProvider();
			Logger.Debug("FlagProvider [{0}] has been initiated", flagProvider.Id);
			
			assemblyLoader = new Assemblies();
			Logger.Debug("Assemblies [{0}] has been initiated", assemblyLoader.Id);
			
			featureManager = new FeatureManager();
			Logger.Debug("FeatureManager [{0}] has been initiated", featureManager.Id);
			
			jsonSerializer = new NewtonsoftJSONSerializer();
			Logger.Debug("NewtonsoftJSONSerializer [{0}] has been initiated", jsonSerializer.Id);
			
		}
		
		public static Get i
		{
			get {
				if (manager == null)
					manager = new Get();
			
				return manager;
			}
		}
		
		public NewtonsoftJSONSerializer JSONSerializer{
			 get {
				return jsonSerializer;
			}
		}
		
		public Names NameProvider{
			get {
				return nameProvider;
			}
		}
		
		public FlagProvider FlagProvider{
			get{ return flagProvider; }
		}
		
		public Assemblies Assemblies{
			get { return assemblyLoader; }
		}
		
		public FeatureManager FeatureManager{
			get { return featureManager; }
		}
	}
}
