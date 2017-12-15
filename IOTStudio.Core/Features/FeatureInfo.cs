/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 10:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Logs;
using System.Linq;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of FeatureInfo.
	/// </summary>
	[DataContract]
	public class FeatureInfo : IFeatureInfo
	{
		public FeatureInfo()
		{
			Logger.Debug("Trying to load feature info");
			string codeBasePath = typeof(FeatureInfo).Assembly.GetName().CodeBase;
			
			if (Directory.Exists(codeBasePath)) {
				string[] files = Directory.GetFiles(codeBasePath, "*.finfo");
				if (files.Count() == 1) {
					Logger.Debug("Feature info file found [{0}]", files[0]);
					UpdateInfo(null);
				} else {
					UpdateInfo(null);
					Logger.Debug("Feature info not found; Assigning default values => [Id: {0}], [Name: {1}]", this.Id, this.Name);
				}
			}
		}

		private void UpdateInfo(IFeatureInfo info)
		{
			if (info != null) {
				Id = info.Id;
				Name = info.Name;
				Description = info.Description;
				Author = info.Author;
				Company = info.Company;
				URL = info.URL;
				Version = info.Version;
				ReleasedDate = info.ReleasedDate;
			} else {
				Id = Guid.NewGuid();
				Name = Get.i.Names.GetName("Feature");
				Version = "1.0.0.0";
				ReleasedDate = DateTime.Now;
				//Get.i.JSONSerializer.Serialize(this, FILENAME);
				Logger.Debug("Default Feature info stored to {0}", Name);
			}
		}
		#region IFeatureInfo implementation

		[DataMember]
		public Guid Id {
			get ;
			internal set ;
		}

		[DataMember]
		public string Name {
			get ;
			internal set ;
		}

		[DataMember]
		public string Description {
			get ;
			internal set ;
		}

		[DataMember]
		public string Author {
			get ;
			internal set ;
		}

		[DataMember]
		public string Company {
			get ;
			internal set ;
		}

		[DataMember]
		public string URL {
			get ;
			internal set ;
		}

		[DataMember]
		public string Version {
			get ;
			internal set ;
		}

		[DataMember]
		public DateTime ReleasedDate {
			get ;
			internal set ;
		}

		
		public override string ToString()
		{
			return string.Format("[FeatureInfo Id={0}, Name={1}, Company={2}, Version={3}, ReleasedDate={4}]", Id, Name, Company, Version, ReleasedDate);
		}

		#endregion
	}
}
