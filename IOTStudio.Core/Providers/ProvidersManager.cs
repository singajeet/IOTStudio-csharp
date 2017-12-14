/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 9:05 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Features;
using IOTStudio.Core.Providers.Assemblies;
using IOTStudio.Core.Providers.Flags;
using IOTStudio.Core.Serializers;

namespace IOTStudio.Core.Providers
{
	/// <summary>
	/// Description of ProvidersManager.
	/// </summary>
	public class ProvidersManager
	{
		private static ProvidersManager manager;
		
		private RuntimeNameProvider nameProvider;
		private FlagProvider flagProvider;
		private AssemblyLoader assemblyLoader;
		private FeatureManager featureManager;
		
		private ProvidersManager()
		{
			nameProvider = new RuntimeNameProvider();
			flagProvider = new FlagProvider();
			assemblyLoader = new AssemblyLoader();
			featureManager = new FeatureManager();
		}
		
		public static ProvidersManager i
		{
			get {
				if (manager == null)
					manager = new ProvidersManager();
			
				return manager;
			}
		}
		
		public RuntimeNameProvider NameProvider{
			get {
				return nameProvider;
			}
		}
		
		public FlagProvider FlagProvider{
			get{ return flagProvider; }
		}
		
		public AssemblyLoader AssemblyLoader{
			get { return assemblyLoader; }
		}
		
		public FeatureManager FeatureManager{
			get { return featureManager; }
		}
	}
}
