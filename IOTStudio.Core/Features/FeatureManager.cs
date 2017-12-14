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
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Core.Providers.Properties;
using IOTStudio.Core.Serializers;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of FeatureManager.
	/// </summary>
	[DataContract]
	public class FeatureManager 
	{
		private Dictionary<string, IFeature> features = new Dictionary<string, IFeature>();
		
		[DataMember]
		public Dictionary<string, IFeature> Features{ get; set; }
		
		public FeatureManager()
		{
			string featureListPath = PropertyProvider.Features.GetProperty("FeaturesListPath") as string;
			
			if (System.IO.File.Exists(featureListPath + @"\FeaturesList.json"))
				LoadFeaturesList();
			
			Features = Features ?? new Dictionary<string, IFeature>();
		}
		
		public void RegisterFeature(string key, IFeature feature)
		{
			if (Features.ContainsKey(key))
				throw new Exception(string.Format("Feature with key {0} is already registered", key));
			
			Features.Add(key, feature);
		}
		
		public void UnregisterFeature(string key)
		{
			if (!Features.ContainsKey(key))
				throw new Exception(string.Format("No such feature found {0}", key));
			
			Features.Remove(key);
		}
		
		public IFeature FindFeature(string key)
		{
			return Features[key];
		}
		
		private void LoadFeaturesList()
		{
			string featureListPath = PropertyProvider.Features.GetProperty("FeaturesListPath") as string;
			
			Logger.Debug("Features list will be deserialized from the following file: {0}", featureListPath + @"\FeaturesList.json");
			
			Features = NewtonsoftJSONSerializer.Deserialize(featureListPath + @"\FeaturesList.json") as Dictionary<string, IFeature>;
		}
		
		public void SaveFeatureList()
		{
			string featureListPath = PropertyProvider.Features.GetProperty("FeaturesListPath") as string;
			
			Logger.Debug("Features list will be serialized to the following file: {0}", featureListPath + @"\FeaturesList.json");
			
			NewtonsoftJSONSerializer.Serialize(Features, featureListPath + @"\FeaturesList.json");
		
		}

		~FeatureManager()
			{
				SaveFeatureList();
			}
	
	}
}
