/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 10:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IOTStudio.Core.Elements.Interfaces
{
	/// <summary>
	/// Description of IFeatureInfo.
	/// </summary>
	public interface IFeatureInfo
	{
		Guid Id { get; }
		string Name { get; }
		string Description { get; }
		string Author { get; }
		string Company { get; }
		string URL { get; }
		string Version { get; }
		DateTime ReleasedDate { get; }
	}
}
