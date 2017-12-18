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
			
		}
		#region IFeatureInfo implementation

		[DataMember]
		public Guid Key {
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
			return string.Format("[FeatureInfo Key={0}, Name={1}, Company={2}, Version={3}, ReleasedDate={4}]", Key, Name, Company, Version, ReleasedDate);
		}

		#endregion
	}
}
