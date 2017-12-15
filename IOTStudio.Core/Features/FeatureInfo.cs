﻿/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 10:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Runtime.Serialization;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers;
using IOTStudio.Core.Providers.Logging;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of FeatureInfo.
	/// </summary>
	[DataContract]
	public class FeatureInfo : IFeatureInfo
	{
		private const string FILENAME = "FeatureInfo.json";
		public FeatureInfo()
		{
			Logger.Debug("Trying to load feature info from => {0}", FILENAME);
			if (File.Exists(FILENAME)) {
				IFeatureInfo info = Get.i.JSONSerializer.Deserialize(FILENAME) as IFeatureInfo;
				UpdateInfo(info);				
				Logger.Debug("Feature info loaded successfully => [Id: {0}], [Name: {1}], [Description: {2}]", info.Id, info.Name, info.Description);
			} else {
				UpdateInfo(null);
				Logger.Debug("Feature info not found; Assigning default values => [Id: {0}], [Name: {1}]", this.Id, this.Name);
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
				Get.i.JSONSerializer.Serialize(this, FILENAME);
				Logger.Debug("Default Feature info stored to {0}", FILENAME);
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

		#endregion
	}
}
