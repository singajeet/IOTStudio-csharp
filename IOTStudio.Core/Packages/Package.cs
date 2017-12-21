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
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using IOTStudio.Core.Features;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Stores.Providers;
using System.Linq;
using NAppUpdate.Framework;
using NAppUpdate.Framework.Sources;

namespace IOTStudio.Core.Packages
{
	/// <summary>
	/// Description of Package.
	/// </summary>
	public class Package : IPackage
	{
		private const string PACKAGE_DETAILS_FILE = "PackageDetails.inf";
		private const string FEATURES_WILDCARD_FILTER = "Feature*";
		
		private UpdateManager updManager;
		
		public Guid Id { get; set; }
		public string Name { get; set; }
		public IPackageDetails DetailsFromFile { get; set; }		
		public PackageRecord Record { get; set; }
		public FileInfo RawPackageFile { get; set; }
		public FeatureCollection Features { get; set; }
		
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
			Logger.Info("[Package]: Package instance has been created");
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
				Logger.Error("[Package]: Unable to find Package file => [{0}]", RawPackageFile.Name);
				
				if(_ThrowExceptionOnInvalidPackage)
					throw new FileNotFoundException(string.Format("[Package]: Unable to find Package file => [{0}]", RawPackageFile.Name));
				else
					return false;
			}
			   
			if (RawPackageFile.Length <= 0) {
				Logger.Error("Input file is invalid; File size is equal to Zero or less then Zero => [{0}]", RawPackageFile.Name);
				if(_ThrowExceptionOnInvalidPackage)
					throw new FileFormatException(string.Format("[Package]: Input file is invalid; File size is equal to Zero or less then Zero => [{0}]", RawPackageFile.Name));
				else
					return false;
			}
			
			if (RawPackageFile.IsReadOnly) {
				Logger.Error("[Package]: Input file is readonly and can't be processed further => [{0}]", RawPackageFile.Name);
				if(_ThrowExceptionOnInvalidPackage)
					throw new FileFormatException(string.Format("[Package]: Input file is readonly and can't be processed further => [{0}]", RawPackageFile.Name));
				else
					return false;
			}
			
			this.Record = new PackageRecord();
			
			this.Record.Name = RawPackageFile.Name.Substring(0, RawPackageFile.Name.Length - RawPackageFile.Extension.Length);
			this.Record.PackageFileName = RawPackageFile.Name;
			this.Record.PackageFilePath = _InActivePackagesLocation;
			this.Record.PackageFileSize = RawPackageFile.Length;
			this.Record.InstalledPackageLocation = Path.Combine(_InstalledPackagesLocation, RawPackageFile.Name.Substring(0, RawPackageFile.Name.Length - RawPackageFile.Extension.Length));
			
			#if DEBUG
			Logger.Debug("[Package]: Package file has been validated succesfully");
			Logger.Debug("[Package.Record Name={0}, PackageFileName={1}, PackageFilePath={2}, PackageFileSize={3}, InstalledPackageLocation={4}]", Record.Name, Record.PackageFileName, Record.PackageFilePath, Record.PackageFileSize, Record.InstalledPackageLocation);
			#endif
			
			if(PackageFileValidated!=null)
				PackageFileValidated(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
			
			return true;			
		}
		
		public void Install()
		{
			try{
				#if DEBUG
				Logger.Debug("[Package]: Trying to install Package => [{0}]", RawPackageFile.Name);
				#endif
				if(InstallationStarted!= null)
					InstallationStarted(this, 
					                    new PackageInstallEventArgs{
							PackageFileName = RawPackageFile.Name,
							Record=this.Record,
							DetailsFromFile=this.DetailsFromFile
						});
			
				RawPackageFile.MoveTo(Path.Combine(_InActivePackagesLocation, RawPackageFile.Name));
				#if DEBUG
				Logger.Debug("[Package]: Package file moved to InActivePackages location => [{0}]", _InActivePackagesLocation);
				#endif
				
				RawPackageFile.CopyTo(Path.Combine(_PackageTempDirectory, RawPackageFile.Name));
				#if DEBUG
				Logger.Debug("[Package]: Copy of PackageFile has been created in Temp folder => [{0}]", _TempPath);
				#endif
				
				ZipFile.ExtractToDirectory(Path.Combine(_PackageTempDirectory, RawPackageFile.Name), _PackageTempDirectory);
				#if DEBUG
				Logger.Debug("[Package]: [{0}] has been extracted to temp folder => [{1}]", RawPackageFile.Name, _PackageTempDirectory);
				#endif
				
				LoadPackageDetails();
				LoadFeatureDetails();						
				RegisterAsActivePackage();
				InstallFeatures();
				Cleanup();
				CheckForUpdates();
				
				if(InstallationCompleted!=null)
				InstallationCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
				
			} catch (Exception ex) {
				if (_ThrowExceptionOnInvalidPackage) {
					Logger.Error("[Package]: Error while installing package => [{0}] => [{1}]", RawPackageFile.Name, ex.Message, ex);
					throw new Exception(string.Format("Error while installing package => [{0}] => [{1}]", RawPackageFile.Name, ex.Message), ex);
				} else
					Logger.Error("[Package]: Error while installing package => [{0}] => [{1]]", RawPackageFile.Name, ex.Message, ex);
			}
		}
		public void Uninstall()
		{
			if(UninstallationStarted!=null)
				UninstallationStarted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
			
			
			
			if(UninstallationCompleted!=null)
				UninstallationCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		public void Activate()
		{
			this.Record.IsActive = true;
			Get.i.PackagesStore.SavePackage(this);
			
			#if DEBUG
				Logger.Debug("[Package]: Package [{0}] activated", this);
			#endif
			if(PackageActivated!=null)
				PackageActivated(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		public void Deactivate()
		{
			this.Record.IsActive = false;
			Get.i.PackagesStore.SavePackage(this);
			
			#if DEBUG
				Logger.Debug("[Package]: Package [{0}] deactivated", this);
			#endif
			if(PackageDeactivated!=null)
				PackageDeactivated(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void LoadPackageDetails()
		{			
			RawPackageFile = new FileInfo(Path.Combine(_InActivePackagesLocation, RawPackageFile.Name));
			RawPackageFile.MoveTo(Path.Combine(_ActivePackagesLocation, RawPackageFile.Name));
			
			if (!Directory.Exists(this.Record.InstalledPackageLocation))
				Directory.CreateDirectory(this.Record.InstalledPackageLocation);
			
			RawPackageFile.CopyTo(Path.Combine(this.Record.InstalledPackageLocation, RawPackageFile.Name));		
			
			ZipFile.ExtractToDirectory(RawPackageFile.FullName, this.Record.InstalledPackageLocation);
			
			_CurrentPackageInstalledLocation = new DirectoryInfo(this.Record.InstalledPackageLocation);
			
			this.Record.Directories = Directory.GetDirectories(_CurrentPackageInstalledLocation.FullName);
			this.Record.Files = Directory.GetFiles(_CurrentPackageInstalledLocation.FullName);
			
			//Find and read the PackageDetails.inf files for more metadata info
			FileInfo pkgDetailsFile = new FileInfo(this.Record.Files.ToList().Where(f => f.EndsWith(PACKAGE_DETAILS_FILE, StringComparison.CurrentCulture)).ToList().Single());
			
			YamlDotNet.Serialization.Deserializer deserializer = new YamlDotNet.Serialization.Deserializer();
			using (TextReader reader = File.OpenText(pkgDetailsFile.FullName)) {
				this.DetailsFromFile = deserializer.Deserialize<PackageDetails>(reader);
			}
			
			#if DEBUG
			Logger.Debug("[Package]: Package details loaded successfully => [{0}]", this.DetailsFromFile);
			#endif
			if(PackageDetailsLoaded!=null)
				PackageDetailsLoaded(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void LoadFeatureDetails()
		{
			this.Record.Features = Directory.GetDirectories(_CurrentPackageInstalledLocation.FullName, FEATURES_WILDCARD_FILTER);
			#if DEBUG
			Logger.Debug("[Package]: Details of [{0}] features under [{1}] Package have been loaded successfully", this.Record.Features.Length, RawPackageFile.Name);
			#endif
			if(FeaturesDetailsLoaded!= null)
				FeaturesDetailsLoaded(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void RegisterAsActivePackage()
		{
			this.Id = this.DetailsFromFile.Id;
			this.Name = this.DetailsFromFile.Name;
			this.Record.PackageFilePath = _ActivePackagesLocation;
			Record.PackageKey = this.Id;
			Record.Name = this.Name;
			Record.IsActive = false;
			Record.IsInstalled = false;
			this.Record.InstalledOn = DateTime.Now;
			
			if (Get.i.PackagesStore.ContainsKey(this.Id)) {
				Get.i.PackagesStore.SavePackage(this);
			} else {
				Get.i.PackagesStore.RegisterPackage(this);
			}
			#if DEBUG
			Logger.Debug("[Package]: Package registered successfully in the system => [{0}]", this);
			#endif
			if(PackageRegistered!=null)
				PackageRegistered(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void InstallFeatures()
		{
			#if DEBUG
				Logger.Debug("[Package]: Trying to install all features under this package: Total[{0}]", Record.Features.Length);
			#endif
			
			Features = new FeatureCollection();
			
			foreach (string feature in this.Record.Features) {
				#if DEBUG
					Logger.Debug("[Package]: Trying to install feature on path => [{0}]", feature);
				#endif
				DirectoryInfo featureDir = new DirectoryInfo(feature);
				IFeature featureObject = new Feature(this, featureDir);
				featureObject.ValidateFile();
				featureObject.Install();
				Features.Add(featureObject);
			}
			
			#if DEBUG
				Logger.Debug("[Package]: All features have been installed successfully");
			#endif
			
			if(FeaturesInstalled!=null)
				FeaturesInstalled(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void Cleanup()
		{
			Directory.Delete(_PackageTempDirectory, true);
			#if DEBUG
			Logger.Debug("[Package]: Temp directory for package cleaned successfully");
			#endif
			
			if(CleanupCompleted!=null)
				CleanupCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void CheckForUpdates()
		{
			updManager = UpdateManager.Instance;
			
			updManager.ReinstateIfRestarted();
			UpdateManager.Instance.CleanUp();
			
			if (!Directory.Exists(Path.Combine(_TempPath, "UpdateService")))
				Directory.CreateDirectory(Path.Combine(_TempPath, "UpdateService"));
			updManager.Config.TempFolder = Path.Combine(_TempPath, "UpdateService");
			
			#if DEBUG
				Logger.Debug("[Package]: Temporary folder to be used by UpdateService: {0}", updManager.Config.TempFolder);
			#endif
			
			updManager.UpdateSource = new SimpleWebSource(Properties.UpdateServiceFromWeb.Get("URL" + "?PkgId=" + Id));
			
			#if DEBUG
				if(Debugger.IsAttached){
					//For testing purpose only; This will not be included in the release mode code
					string feedxml = File.ReadAllText(Path.Combine(_AppBasePath, Properties.UpdateServiceFromWeb.Get("Test_SampleUpdateFeed")));
					updManager.UpdateSource = new MemorySource(feedxml);
				}
			#endif
			
			try{
				updManager.CheckForUpdates();
				#if DEBUG
					Logger.Debug("[Package]: check updates completed successfully");
				#endif
			} catch (Exception ex) {
				if (ex is NAppUpdateException) {
					Logger.Error("[Package]: Error in app updater due to NAppUpdate=>[{0}]", ex.Message, ex);
				} else {
				Logger.Error("[Package]: Error in app updater due to=> [{0}]", ex.Message, ex);
				}
			}
			
			if (updManager.UpdatesAvailable > 0) {
				Logger.Info("[Package]: Updates are available for this package");
				this.Record.IsUpdateAvailable = true;
			} else {
				Logger.Info("[Package]: No updates available for this package");
				this.Record.IsUpdateAvailable = false;
			}
			
			if(CheckUpdatesCompleted!=null)
				CheckUpdatesCompleted(this, new PackageInstallEventArgs{
					PackageFileName = RawPackageFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		public override string ToString()
		{
			return string.Format("[Package Id={0}, Name={1}, IsActive={2}, IsInstalled={3}]", Id, Name, Record.IsActive, Record.IsInstalled);
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
		public PackageRecord Record { get; set; }
		public IPackageDetails DetailsFromFile { get; set; }
	}
}
