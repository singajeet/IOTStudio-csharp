/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 7:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Stores;

namespace IOTStudio.Core.Providers
{
	/// <summary>
	/// Description of FeatureManager.
	/// </summary>
	[DataContract]
	public class Features : IProvider
	{
		private Dictionary<string, IFeature> features = new Dictionary<string, IFeature>();

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
		[DataMember]
		public Dictionary<string, IFeature> AllFeatures{ get; set; }
		
		public Features()
		{
			string featureListPath = Properties.Features.Get("FeaturesListPath") as string;
			
			if (Get.i.DataStore.Exists(featureListPath))
				LoadFeaturesList();
			
			AllFeatures = AllFeatures ?? new Dictionary<string, IFeature>();
		}
		
		public void RegisterFeature(string key, IFeature feature)
		{
			if (AllFeatures.ContainsKey(key))
				throw new Exception(string.Format("Feature with key {0} is already registered", key));
			
			AllFeatures.Add(key, feature);
		}
		
		public void UnregisterFeature(string key)
		{
			if (!AllFeatures.ContainsKey(key))
				throw new Exception(string.Format("No such feature found {0}", key));
			
			AllFeatures.Remove(key);
		}
		
		public IFeature FindFeature(string key)
		{
			return AllFeatures[key];
		}
		
		private void LoadFeaturesList()
		{
			string featureListPath = Properties.Features.Get("FeaturesListPath") as string;
			
			Logger.Debug("Features list will be deserialized from the following DataStore: {0}", featureListPath);
			
			AllFeatures = Get.i.JSONSerializer.Deserialize(featureListPath, typeof(Dictionary<string, IFeature>)) as Dictionary<string, IFeature>;
		}
		
		public void SaveFeatureList()
		{
			string featureListPath = Properties.Features.Get("FeaturesListPath") as string;
			
			Logger.Debug("Features list will be serialized to the following DataStore: {0}", featureListPath);
			
			Get.i.JSONSerializer.Serialize(AllFeatures, featureListPath, typeof(Dictionary<string, IFeature>));
		
		}

		~Features()
			{
				SaveFeatureList();
			}
	
	}
}
