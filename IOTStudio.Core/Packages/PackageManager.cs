/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 5:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Stores.Logs;

namespace IOTStudio.Core.Packages
{
	/// <summary>
	/// Description of PackageManager.
	/// </summary>
	public sealed class PackageManager
	{
		private const string PACKAGE_DROPIN_LOCATION = "PackageDropInLocation";
		private const string PACKAGE_INSTALLED_LOCATION = "ActivePackagesLocation";
		private const string PACKAGE_FILE_EXTENSION = ".pkg";
		
		private static PackageManager instance = new PackageManager();
		private FileSystemWatcher fileSystemWatcher;		
		private string pkgDropInLocation;
		private string pkgInstalledLocation;
		
		public static PackageManager Instance {
			get {
				return instance;
			}
		}
				
		public string PackageInstalledLocation{
			get { return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pkgInstalledLocation); }
		}
		public string PackageDropInLocation{
			get { return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pkgDropInLocation); }
		}
		
		private PackageManager()
		{	
			Logger.Debug("PackageManager worker instance created");			
		}		
		
		public void StartFileSystemWatcher()
		{
			pkgDropInLocation = Properties.PackageManager.Get(PACKAGE_DROPIN_LOCATION);
			pkgInstalledLocation = Properties.PackageManager.Get(PACKAGE_INSTALLED_LOCATION);
			Logger.Debug("Pkg drop in will be watched at [{0}] and will be installed to [{1}]", PackageDropInLocation, PackageInstalledLocation);
			
			fileSystemWatcher = new FileSystemWatcher();
			fileSystemWatcher.Path = PackageDropInLocation;
			
			fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
			fileSystemWatcher.Filter = "*" + PACKAGE_FILE_EXTENSION;	

			fileSystemWatcher.Created += fileSystemWatcher_Created;	
			fileSystemWatcher.Changed += fileSystemWatcher_Changed;
			fileSystemWatcher.Deleted += fileSystemWatcher_Deleted;			
			fileSystemWatcher.Renamed += fileSystemWatcher_Renamed;
			fileSystemWatcher.Error += fileSystemWatcher_Error;			
			
			fileSystemWatcher.IncludeSubdirectories = true;
			fileSystemWatcher.EnableRaisingEvents = true;
			
			Logger.Debug("File system watcher has been started");
		}

		void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			Logger.Debug("FileSystemWatcher's (ChangeType={0}) event fired; Following file have been {0} => [{1}]", e.ChangeType, e.Name);
			
			if (e.Name.EndsWith(PACKAGE_FILE_EXTENSION, StringComparison.CurrentCulture)) {
				Logger.Debug("Package file have been found and same will be processed now for installation");
				FileInfo pkgFile = new FileInfo(e.FullPath);
				
				Package pkg = new Package(pkgFile);
				pkg.ValidateFile();
				pkg.Install();
			}
		}
		
		void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
		{
			//Logger.Debug("FileSystemWatcher's change event fired (ChangeType={0}); Following files has been created => [{1}]", e.ChangeType, e.Name);
		}

		void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			//Logger.Debug("FileSystemWatcher's change event fired (ChangeType={0}); Following files has been created => [{1}]", e.ChangeType, e.Name);
		}

		void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			//Logger.Debug("FileSystemWatcher's change event fired (ChangeType={0}); Following files has been created => [{1}]", e.ChangeType, e.Name);
		}

		void fileSystemWatcher_Error(object sender, ErrorEventArgs e)
		{
			
		}
	}
}
