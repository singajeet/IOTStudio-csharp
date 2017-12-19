/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 11:32 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of IPackageDetails.
	/// </summary>
	public interface IPackageDetails{
		Guid Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		string Author { get; set; }
		string Company { get; set; }
		string Version { get; set; }
		DateTime ReleasedDate { get; set; }
		string URL { get; set; }
	}
}
