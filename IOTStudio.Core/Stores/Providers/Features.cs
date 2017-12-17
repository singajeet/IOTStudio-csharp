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
	
	public class Feature
	{
		public ObjectId Id { get; set; }
		public string Key { get; set; }
		public IFeature Value { get; set; }
		
		public Feature()
		{
		}
		
		public Feature(string key, IFeature value)
		{
			Key = key;
			Value = value;
		}
		
		public override string ToString()
		{
			return string.Format("[Feature Id={0}, Key={1}, Value={2}]", Id, Key, Value);
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
		
		private LiteCollection<Feature> AllFeatures{
			get {
				return dbDriver.DB.GetCollection<Feature>(FEATURES_COLLECTION);
			}
		}
		
		public void RegisterFeature(string key, IFeature feature)
		{
			CheckAndConnect();
			
			if (this.ContainsKey(key))
				throw new Exception(string.Format("Feature with key {0} is already registered", key));
			
			Feature featureObject = new Feature(key, feature);
			AllFeatures.Insert(featureObject);
			
			Logger.Debug("[{0}] has been registered successfully", featureObject);
		}
		
		public bool ContainsKey(string key)
		{
			CheckAndConnect();
			
			return AllFeatures.Exists(f => f.Key.Equals(key));
		}
		
		public void UnregisterFeature(string key)
		{
			CheckAndConnect();
			
			if (!this.ContainsKey(key))
				throw new Exception(string.Format("No such feature found {0}", key));
			
			AllFeatures.Delete(f => f.Key.Equals(key));
			Logger.Debug("Feature [{0}] unregistered successfully");
		}
		
		public IFeature FindFeature(string key)
		{
			CheckAndConnect();
			
			return AllFeatures.FindOne(f => f.Key.Equals(key)).Value;
		}	
	}
}
