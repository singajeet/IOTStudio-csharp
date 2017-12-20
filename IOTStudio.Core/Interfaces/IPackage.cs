/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 11:22 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using IOTStudio.Core.Stores.Providers;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of IPackage.
	/// </summary>
	public interface IPackage
	{
		Guid Id { get; set; }
		string Name { get; set; }
		IPackageDetails DetailsFromFile { get;set; }
		PackageRecord Info { get; set; }
		FileInfo RawPackageFile { get; set; }
		
		bool ValidateFile();
		void Install();
		void Uninstall();
		void Activate();
		void Deactivate();		
		
		event EventHandler InstallationStarted;
		event EventHandler InstallationCompleted;
		event EventHandler PackageDetailsLoaded;
		event EventHandler FeaturesDetailsLoaded;
		event EventHandler PackageRegistered;
		event EventHandler FeaturesInstalled;
		event EventHandler CleanupCompleted;
		event EventHandler CheckUpdatesCompleted;
		event EventHandler PackageActivated;
		event EventHandler PackageDeactivated;
		event EventHandler UninstallationStarted;
		event EventHandler UninstallationCompleted;
		event EventHandler PackageFileValidated;
	}
}
