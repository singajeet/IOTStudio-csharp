/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/19/2017
 * Time: 6:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Interfaces;

namespace IOTStudio.Core.Packages
{
	/// <summary>
	/// Description of PackageDetails.
	/// </summary>
	public class PackageDetails : IPackageDetails
	{
		public PackageDetails()
		{
		}

		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string Author { get; set; }

		public string Company { get; set; }

		public string Version { get; set; }

		public DateTime ReleasedDate { get; set; }

		public string URL { get; set; }
	
		public override string ToString()
		{
			return string.Format("[PackageDetails Id={0}, Name={1}, Version={2}, URL={3}]", Id, Name, Version, URL);
		}

	}
}
