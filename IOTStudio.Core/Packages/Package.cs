/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 11:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Stores.Providers;
using System.Linq;

namespace IOTStudio.Core.Packages
{
	/// <summary>
	/// Description of Package.
	/// </summary>
	public class Package : IPackage
	{
		private const string PACKAGE_DETAILS_FILE = "PackageDetails.inf";
		private const string FEATURES_WILDCARD_FILTER = "Feature*";
		
		public Guid Id { get; set; }
		public string Name { get; set; }
		public IPackageDetails DetailsFromFile { get; set; }		
		public PackageRecord Info { get; set; }
		public FileInfo RawPackageFile { get; set; }
		
		private bool _ThrowExceptionOnInvalidPackage;
		private string _AppBasePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		private string _TempPath;
		private string _ActivePackagesLocation;
		private string _InActivePackagesLocation;
		private string _InstalledPackagesLocation;
		private string _PackageTempDirectory;
		private DirectoryInfo _CurrentPackageInstalledLocation;
		
		public Package()
		{
			Logger.Debug("Package instance has been created");
		}
		
		public Package(FileInfo pkgFile) : this()
		{
			RawPackageFile = pkgFile;
			_TempPath = Path.Combine(_AppBasePath, "Temp");
			if (!Directory.Exists(_TempPath))
				Directory.CreateDirectory(_TempPath);
			
			_ActivePackagesLocation = Path.Combine(_AppBasePath, Properties.PackageManager.Get("ActivePackagesLocation"));
			if (!Directory.Exists(_ActivePackagesLocation))
				Directory.CreateDirectory(_ActivePackagesLocation);
			
			_InActivePackagesLocation = Path.Combine(_AppBasePath, Properties.PackageManager.Get("InActivePackagesLocation"));
			if (!Directory.Exists(_InActivePackagesLocation))
				Directory.CreateDirectory(_InActivePackagesLocation);
			
			_InstalledPackagesLocation = Path.Combine(_AppBasePath, Properties.PackageManager.Get("InstalledPackagesLocation"));
			if (!Directory.Exists(_InstalledPackagesLocation))
				Directory.CreateDirectory(_InstalledPackagesLocation);
			
			_PackageTempDirectory = Path.Combine(_TempPath, RawPackageFile.Name.Substring(0, RawPackageFile.Name.Length - RawPackageFile.Extension.Length));
			if (!Directory.Exists(_PackageTempDirectory))
				Directory.CreateDirectory(_PackageTempDirectory);
			
			if (!Boolean.TryParse(Properties.PackageManager.Get("ThrowExceptionOnInvalidPackage"), out _ThrowExceptionOnInvalidPackage)) {
				_ThrowExceptionOnInvalidPackage = false;
			}
		}
		
		public bool ValidateFile()
		{
			if(!RawPackageFile.Exists || string.IsNullOrEmpty(RawPackageFile.FullName)){
				Logger.Error("Unable to find Package file => [{0}]", RawPackageFile.Name);
				
				if(_ThrowExceptionOnInvalidPackage)
					throw new FileNotFoundException(string.Format("Unable to find Package file => [{0}]", RawPackageFile.Name));
				else
					return false;
			}
			   
			if (RawPackageFile.Length <= 0) {
				Logger.Error("Input file is invalid; File size is equal to Zero or less then Zero => [{0}]", RawPackageFile.Name);
				if(_ThrowExceptionOnInvalidPackage)
					throw new FileFormatException(string.Format("Input file is invalid; File size is equal to Zero or less then Zero => [{0}]", RawPackageFile.Name));
				else
					return false;
			}
			
			if (RawPackageFile.IsReadOnly) {
				Logger.Error("Input file is readonly and can't be processed further => [{0}]", RawPackageFile.Name);
				if(_ThrowExceptionOnInvalidPackage)
					throw new FileFormatException(string.Format("Input file is readonly and can't be processed further => [{0}]", RawPackageFile.Name));
				else
					return false;
			}
			
			this.Info = new PackageRecord();
			
			this.Info.Name = RawPackageFile.Name.Substring(0, RawPackageFile.Name.Length - RawPackageFile.Extension.Length);
			this.Info.PackageFileName = RawPackageFile.Name;
			this.Info.PackageFilePath = _InActivePackagesLocation;
			this.Info.PackageFileSize = RawPackageFile.Length;
			this.Info.InstalledPackageLocation = Path.Combine(_InstalledPackagesLocation, RawPackageFile.Name.Substring(0, RawPackageFile.Name.Length - RawPackageFile.Extension.Length));
			
			Logger.Debug("Package file has been validated succesfully");
			Logger.Debug("[Package.Info Name={0}, PackageFileName={1}, PackageFilePath={2}, PackageFileSize={3}, InstalledPackageLocation={4}]", Info.Name, Info.PackageFileName, Info.PackageFilePath, Info.PackageFileSize, Info.InstalledPackageLocation);
			
			if(PackageFileValidated!=null)
				PackageFileValidated(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
			
			return true;
		}
		
		public void Install()
		{
			Logger.Debug("Trying to install Package => [{0}]", RawPackageFile.Name);
			if(InstallationStarted!= null)
				InstallationStarted(this, 
				                    new PackageInstallEventArgs{
						PackageFileName = RawPackageFile.Name,
						Info=this.Info,
						DetailsFromFile=this.DetailsFromFile
					});
			
			
			RawPackageFile.MoveTo(Path.Combine(_InActivePackagesLocation, RawPackageFile.Name));
			Logger.Debug("Package file moved to InActivePackages location => [{0}]", _InActivePackagesLocation);
			
			RawPackageFile.CopyTo(Path.Combine(_PackageTempDirectory, RawPackageFile.Name));
			Logger.Debug("A copy of the PackageFile has been created in Temp folder => [{0}]", _TempPath);
			
			ZipFile.ExtractToDirectory(Path.Combine(_PackageTempDirectory, RawPackageFile.Name), _PackageTempDirectory);
			Logger.Debug("Package [{0}] has been extracted to temp folder => [{1}]", RawPackageFile.Name, _PackageTempDirectory);
			
			LoadPackageDetails();
			LoadFeatureDetails();						
			RegisterAsActivePackage();
			InstallFeatures();
			Cleanup();
			CheckForUpdates();
			
			if(InstallationCompleted!=null)
				InstallationCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		public void Uninstall()
		{
			if(UninstallationStarted!=null)
				UninstallationStarted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
			
			
			
			if(UninstallationCompleted!=null)
				UninstallationCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		public void Activate()
		{
			this.Info.IsActive = true;
			Get.i.PackagesStore.SavePackage(this);
			
			if(PackageActivated!=null)
				PackageActivated(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		public void Deactivate()
		{
			this.Info.IsActive = false;
			Get.i.PackagesStore.SavePackage(this);
			
			if(PackageDeactivated!=null)
				PackageDeactivated(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void LoadPackageDetails()
		{			
			RawPackageFile = new FileInfo(Path.Combine(_InActivePackagesLocation, RawPackageFile.Name));
			RawPackageFile.MoveTo(Path.Combine(_ActivePackagesLocation, RawPackageFile.Name));
			
			if (!Directory.Exists(this.Info.InstalledPackageLocation))
				Directory.CreateDirectory(this.Info.InstalledPackageLocation);
			
			RawPackageFile.CopyTo(Path.Combine(this.Info.InstalledPackageLocation, RawPackageFile.Name));		
			
			ZipFile.ExtractToDirectory(RawPackageFile.FullName, this.Info.InstalledPackageLocation);
			
			_CurrentPackageInstalledLocation = new DirectoryInfo(this.Info.InstalledPackageLocation);
			
			this.Info.Directories = Directory.GetDirectories(_CurrentPackageInstalledLocation.FullName);
			this.Info.Files = Directory.GetFiles(_CurrentPackageInstalledLocation.FullName);
			
			//Find and read the PackageDetails.inf files for more metadata info
			FileInfo pkgDetailsFile = new FileInfo(this.Info.Files.ToList().Where(f => f.EndsWith(PACKAGE_DETAILS_FILE, StringComparison.CurrentCulture)).ToList().Single());
			
			YamlDotNet.Serialization.Deserializer deserializer = new YamlDotNet.Serialization.Deserializer();
			using (TextReader reader = File.OpenText(pkgDetailsFile.FullName)) {
				this.DetailsFromFile = deserializer.Deserialize<PackageDetails>(reader);
			}
			
			Logger.Debug("Package details loaded successfully => [{0}]", this.DetailsFromFile);
			if(PackageDetailsLoaded!=null)
				PackageDetailsLoaded(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void LoadFeatureDetails()
		{
			this.Info.Features = Directory.GetDirectories(_CurrentPackageInstalledLocation.FullName, FEATURES_WILDCARD_FILTER);
			
			Logger.Debug("Details of [{0}] features under [{1}] Package have been loaded successfully", this.Info.Features.Length, RawPackageFile.Name);
			if(FeaturesDetailsLoaded!= null)
				FeaturesDetailsLoaded(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void RegisterAsActivePackage()
		{
			this.Id = this.DetailsFromFile.Id;
			this.Name = this.DetailsFromFile.Name;
			this.Info.PackageFilePath = _ActivePackagesLocation;
			Info.PackageKey = this.Id;
			Info.Name = this.Name;
			Info.IsActive = false;
			Info.IsInstalled = false;
			this.Info.InstalledOn = DateTime.Now;
			
			if (Get.i.PackagesStore.ContainsKey(this.Id)) {
				Get.i.PackagesStore.SavePackage(this);
			} else {
				Get.i.PackagesStore.InsertPackage(this);
			}
			Logger.Debug("Package registered successfully in the system => [{0}]", this);
			if(PackageRegistered!=null)
				PackageRegistered(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void InstallFeatures()
		{
			if(FeaturesInstalled!=null)
				FeaturesInstalled(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void Cleanup()
		{
			Directory.Delete(_PackageTempDirectory, true);
			Logger.Debug("Temp directory for package cleaned successfully");
			if(CleanupCompleted!=null)
				CleanupCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void CheckForUpdates()
		{
			if(CheckUpdatesCompleted!=null)
				CheckUpdatesCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Info = this.Info,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		public override string ToString()
		{
			return string.Format("[Package Id={0}, Name={1}, IsActive={2}, IsInstalled={3}]", Id, Name, Info.IsActive, Info.IsInstalled);
		}
		
		public event EventHandler InstallationStarted;
		public event EventHandler InstallationCompleted;
		public event EventHandler PackageDetailsLoaded;
		public event EventHandler FeaturesDetailsLoaded;
		public event EventHandler PackageRegistered;
		public event EventHandler FeaturesInstalled;
		public event EventHandler CleanupCompleted;
		public event EventHandler CheckUpdatesCompleted;
		public event EventHandler PackageActivated;
		public event EventHandler PackageDeactivated;
		public event EventHandler UninstallationStarted;
		public event EventHandler UninstallationCompleted;
		public event EventHandler PackageFileValidated;
	}
	
	public class PackageInstallEventArgs: EventArgs
	{
		public string PackageFileName { get; set; }
		public PackageRecord Info { get; set; }
		public IPackageDetails DetailsFromFile { get; set; }
	}
}
