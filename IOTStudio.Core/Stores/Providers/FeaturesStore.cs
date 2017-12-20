/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 7:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.Serialization;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Stores;
using LiteDB;

namespace IOTStudio.Core.Stores.Providers
{
	using Logger = IOTStudio.Core.Stores.Logs.Logger;	
	
	public class FeatureRecord
	{
		public ObjectId Id { get; set; }
		public Guid FeatureKey { get; set; }
		public Guid ParentPackageKey { get; set; }
		public string Name { get; set; }	
		public string FeatureInfFilePath { get; set; }
		public string FeatureInfFileName { get; set; }
		public long FeatureInfFileSize { get; set; }
		public bool IsActive { get; set; }
		public bool IsInstalled { get; set; }
		public string FeatureBasePath { get; set; }
		public string[] Files { get; set; }
		public string[] Directories { get; set; }
		public Guid ParentFeatureKey { get; set; }
		public Guid[] ChildFeatureKeys { get; set; }
		public bool IsUpdateAvailable { get; set; }
		public DateTime InstalledOn { get; set; }
		public string InputFlagName { get; set; }
		public string OutputFlagName { get; set; }		
		
		public FeatureRecord()
		{
		}
		
		public FeatureRecord(IFeature feature)
		{
			FeatureKey = feature.Id;
			Name = feature.Name;
		}
		
		public FeatureRecord(Guid featureId, string name)
		{
			FeatureKey = featureId;
			Name = name;
		}
		
		public override string ToString()
		{
			return string.Format("[FeatureRecord Id={0}, FeatureKey={1}, Name={2}, IsActive={3}, IsInstalled={4}]", Id, FeatureKey, Name, IsActive, IsInstalled);
		}

	}
	
	/// <summary>
	/// Description of FeatureManager.
	/// </summary>
	[DataContract]
	public class FeaturesStore : BaseStore, IProvider
	{
		IDBDriver dbDriver;
		
		#region IProvider implementation
		public Guid Id {
			get {
				return new Guid("1CA3AD47-59A9-4C45-A079-54384F49172E");
			}
		}
		public string Name {
			get {
				return "FeatureManager";
			}
		}
		#endregion		
				
		public FeaturesStore()
		{
			
		}
		
		private void CheckAndConnect()
		{
			if (dbDriver == null) {
				dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE_SCHEMA);
				dbDriver.Connect();	
			}
		}
		
		private LiteCollection<FeatureRecord> AllFeatures{
			get {
				return dbDriver.DB.GetCollection<FeatureRecord>(FEATURES_COLLECTION);
			}
		}
		
		public void RegisterFeature(IFeature feature)
		{
			CheckAndConnect();
			
			if (this.ContainsKey(feature.Id))
				throw new Exception(string.Format("Feature with key {0} is already registered", feature.Id));
			
			FeatureRecord featureObject = new FeatureRecord(feature);
			AllFeatures.Insert(featureObject);
			
			Logger.Info("[{0}] has been registered successfully", featureObject);
		}
		
		public bool ContainsName(string name)
		{
			CheckAndConnect();
			
			return AllFeatures.Exists(f => f.Name.Equals(name));
		}
		
		public bool ContainsKey(Guid key)
		{
			CheckAndConnect();
			
			return AllFeatures.Exists(f => f.FeatureKey == key);
		}
		
		public void SaveFeature(IFeature feature)
		{
			#if DEBUG
				Logger.Debug("[FeaturesStore]: Trying to save feature => [{0}]", feature);
			#endif
			AllFeatures.Upsert(feature.Info);
			
			#if DEBUG
				Logger.Debug("[FeaturesStore]: Feature has been updated/inserted successfully");
			#endif
					
		}
		
		public FeatureRecord LoadFeature(Guid key)
		{
			if (ContainsKey(key)) {
				return AllFeatures.FindOne(f => f.FeatureKey == key);
			} else {
				throw new Exception(string.Format("[FeaturesStore]: No information found for feature with Key => [{0}]", Id));
			}
		}
		
		public FeatureRecord LoadFeature(string name)
		{
			if (ContainsName(name)) {
				return AllFeatures.FindOne(f => f.Name.Equals(name));
			} else {
				throw new Exception(string.Format("[FeaturesStore]: No information found for feature with Name => [{0}]", name));
			}
		}
		
		public void UnregisterFeature(IFeature feature)
		{
			CheckAndConnect();
			
			if (!this.ContainsKey(feature.Id))
				throw new Exception(string.Format("[FeaturesStore]: Can't unregister feature! No such feature found {0}", feature.Id));
			
			AllFeatures.Delete(f => f.FeatureKey == feature.Id);
			Logger.Info("[FeaturesStore]: Feature [{0}] has been unregistered successfully");
		}
		
		public string FindFeature(Guid key)
		{
			CheckAndConnect();
			
			return AllFeatures.FindOne(f => f.FeatureKey == key).Name;
		}	
		
		public Guid FindFeature(string name)
		{
			CheckAndConnect();
			
			return AllFeatures.FindOne(f => f.Name.Equals(name)).FeatureKey;
		}
	}
}
