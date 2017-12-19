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
		public string Name { get; set; }		
		
		public FeatureRecord()
		{
		}
		
		public FeatureRecord(IFeature feature)
		{
			FeatureKey = feature.Key;
			Name = feature.Name;
		}
		
		public FeatureRecord(Guid featureId)
		{
			FeatureKey = featureId;
		}
		
		public override string ToString()
		{
			return string.Format("[FeatureRecord Id={0}, Name={1}, FeatureKey={2}]", Id, Name, FeatureKey);
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
			
			if (this.ContainsKey(feature.Key))
				throw new Exception(string.Format("Feature with key {0} is already registered", feature.Key));
			
			FeatureRecord featureObject = new FeatureRecord(feature);
			AllFeatures.Insert(featureObject);
			
			Logger.Debug("[{0}] has been registered successfully", featureObject);
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
		
		public void UnregisterFeature(IFeature feature)
		{
			CheckAndConnect();
			
			if (!this.ContainsKey(feature.Key))
				throw new Exception(string.Format("No such feature found {0}", feature.Key));
			
			AllFeatures.Delete(f => f.FeatureKey == feature.Key);
			Logger.Debug("Feature [{0}] unregistered successfully");
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
