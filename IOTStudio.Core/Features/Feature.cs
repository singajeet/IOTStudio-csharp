/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 5:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Packages;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Stores.Pipes;
using IOTStudio.Core.Stores.Providers;
using IOTStudio.Core.Types;
using NAppUpdate.Framework;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of Feature.
	/// </summary>
	[DataContract]
	public class Feature : IFeature, INotifyPropertyChanged
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Package ParentPackage { get; set; }
		public IFeatureDetails DetailsFromFile { get; set; }
		public FeatureRecord Record { get; set; }
		public FileInfo RawFeatureInfFile { get; set; }
		
		public IFeature ParentFeature { get; set; }
		public FeatureCollection ChildFeatures { get; set; }
		
		public InputPipe InPipe { get; set; }
		public OutputPipe OutPipe { get; set; }	
		
		public IUIFeatureOptionsElement UIOptionsElement { get; set; }
		public IUIRootElement RootElement { get; set; }
		public ObservableCollection<INavigationElement> NavigationElements { get; set; }
		
		private bool _ThrowExceptionOnInvalidFeature;
		private string _AppBasePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		private string _TempPath;		
		private string _FeatureBasePath;	
		private string _FeatureInfPath;
		
		public Feature()
		{
			Logger.Info("[Feature]: Feature instance has been created");
		}

		public Feature(Package parentPackage, DirectoryInfo featureFolder): this()
		{
			ParentPackage = parentPackage;
			_FeatureBasePath = featureFolder.FullName;
			
			RawFeatureInfFile = new FileInfo(Path.Combine(_FeatureBasePath, featureFolder.Name +".inf"));
			_FeatureInfPath = RawFeatureInfFile.FullName;
			_TempPath = Path.Combine(_AppBasePath, "Temp");
			
			if (!Directory.Exists(_TempPath))
				Directory.CreateDirectory(_TempPath);
			
			if (!Boolean.TryParse(Properties.Features.Get("ThrowExceptionOnInvalidFeature"), out _ThrowExceptionOnInvalidFeature)) {
				_ThrowExceptionOnInvalidFeature = false;
			}
		}
		
		public bool ValidateFile()
		{
			if(!RawFeatureInfFile.Exists || string.IsNullOrEmpty(RawFeatureInfFile.FullName)){
				Logger.Error("[Feature]: Unable to find feature file => [{0}]", RawFeatureInfFile.Name);
				
				if(_ThrowExceptionOnInvalidFeature)
					throw new FileNotFoundException(string.Format("[Feature]: Unable to find Feature file => [{0}]", RawFeatureInfFile.Name));
				else
					return false;
			}
			   
			if (RawFeatureInfFile.Length <= 0) {
				Logger.Error("[Feature]: Input file is invalid; File size is equal to Zero or less then Zero => [{0}]", RawFeatureInfFile.Name);
				if(_ThrowExceptionOnInvalidFeature)
					throw new FileFormatException(string.Format("[Feature]: Input file is invalid; File size is equal to Zero or less then Zero => [{0}]", RawFeatureInfFile.Name));
				else
					return false;
			}
			
			if (RawFeatureInfFile.IsReadOnly) {
				Logger.Error("[Feature]: Input file is readonly and can't be processed further => [{0}]", RawFeatureInfFile.Name);
				if(_ThrowExceptionOnInvalidFeature)
					throw new FileFormatException(string.Format("[Feature]: Input file is readonly and can't be processed further => [{0}]", RawFeatureInfFile.Name));
				else
					return false;
			}
			
			this.Record = new FeatureRecord();
			
			this.Record.Name = RawFeatureInfFile.Name.Substring(0, RawFeatureInfFile.Name.Length - RawFeatureInfFile.Extension.Length);
			this.Record.FeatureInfFileName = RawFeatureInfFile.Name;
			this.Record.FeatureInfFilePath = RawFeatureInfFile.Directory.FullName;
			this.Record.FeatureInfFileSize = RawFeatureInfFile.Length;
			this.Record.FeatureBasePath = _FeatureBasePath;
			
			#if DEBUG
			Logger.Debug("[Feature]: Feature file has been validated succesfully");
			Logger.Debug("[Feature.Info Name={0}, FeatureFileName={1}, FeatureFilePath={2}, FeatureFileSize={3}, InstalledFeatureLocation={4}]", Record.Name, Record.FeatureInfFileName, Record.FeatureInfFilePath, Record.FeatureInfFileSize, Record.FeatureBasePath);
			#endif
			
			if(FeatureFileValidated!=null)
				FeatureFileValidated(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
			
			return true;
		}
		
		public void Install()
		{
			try{
			#if DEBUG
			Logger.Debug("[Feature]: Trying to install feature => [{0}]", RawFeatureInfFile.Name);
			#endif
			if(InstallationStarted!= null)
				InstallationStarted(this, 
				                    new FeatureInstallEventArgs{
						FeatureInfFileName = RawFeatureInfFile.Name,
						Record=this.Record,
						DetailsFromFile=this.DetailsFromFile
					});
			
			string zippedFeatureFile = Path.Combine(_FeatureBasePath, this.Record.Name + ".fea");
			if (File.Exists(zippedFeatureFile)) {
				#if DEBUG
					Logger.Debug("[Feature]: Compressed feature file found => [{0}]", zippedFeatureFile);
				#endif
				
				ZipFile.ExtractToDirectory(zippedFeatureFile, _FeatureBasePath);
				
				#if DEBUG
					Logger.Debug("[Feature]: [{0}] has been extracted to temp folder => [{1}]", this.Record.Name + ".fea", _FeatureBasePath);
				#endif
			} else {
				#if DEBUG
					Logger.Debug("[Feature]: No compressed feature file exists!");
				#endif
			}
						
			LoadFeatureDetails();						
			RegisterAsActiveFeature();					
			
			if(InstallationCompleted!=null)
				InstallationCompleted(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
			} catch (Exception ex) {
				if (_ThrowExceptionOnInvalidFeature) {
					Logger.Error("[Feature]: Error while installing feature => [{0}] => [{1}]", RawFeatureInfFile.Name, ex.Message, ex);
					throw new Exception(string.Format("[Feature]: Error while installing feature => [{0}] => [{1}]", RawFeatureInfFile.Name, ex.Message), ex);
				} else {
					Logger.Error("[Feature]: Error while installing feature => [{0}] => [{1}]", RawFeatureInfFile.Name, ex.Message, ex);
				}
			}
		}
		
		public void Uninstall()
		{
			if(UninstallationStarted!=null)
				UninstallationStarted(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
			
			
			
			if(UninstallationCompleted!=null)
				UninstallationCompleted(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		public void Activate()
		{
			this.Record.IsActive = true;
			Get.i.Features.SaveFeature(this);
			
			#if DEBUG
				Logger.Debug("[Feature]: Feature [{0}] activated", this);
			#endif
			if(FeatureActivated!=null)
				FeatureActivated(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		public void Deactivate()
		{
			this.Record.IsActive = false;
			Get.i.Features.SaveFeature(this);
			
			#if DEBUG
				Logger.Debug("[Feature]: Feature [{0}] deactivated", this);
			#endif
			if(FeatureDeactivated!=null)
				FeatureDeactivated(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void LoadFeatureDetails()
		{			
			RawFeatureInfFile = new FileInfo(_FeatureInfPath);		
			this.Record.Directories = Directory.GetDirectories(_FeatureBasePath);
			this.Record.Files = Directory.GetFiles(_FeatureBasePath);
			
			YamlDotNet.Serialization.Deserializer deserializer = new YamlDotNet.Serialization.Deserializer();
			using (TextReader reader = File.OpenText(RawFeatureInfFile.FullName)) {
				this.DetailsFromFile = deserializer.Deserialize<FeatureDetails>(reader);
			}
			
			#if DEBUG
			Logger.Debug("[Feature]: Feature details loaded successfully => [{0}]", this.DetailsFromFile);
			#endif
			if(FeatureDetailsLoaded!=null)
				FeatureDetailsLoaded(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}
		
		private void RegisterAsActiveFeature()
		{
			this.Id = this.DetailsFromFile.Id;
			this.Name = this.DetailsFromFile.Name;
			this.Record.FeatureInfFilePath = RawFeatureInfFile.Directory.FullName;
			Record.FeatureKey = this.Id;
			Record.Name = this.Name;
			Record.IsActive = false;
			Record.IsInstalled = false;
			this.Record.InstalledOn = DateTime.Now;
			
			if (Get.i.Features.ContainsKey(this.Id)) {
				Get.i.Features.SaveFeature(this);
			} else {
				Get.i.Features.RegisterFeature(this);
			}
			#if DEBUG
			Logger.Debug("[Feature]: Feature registered successfully in the system => [{0}]", this);
			#endif
			if(FeatureRegistered!=null)
				FeatureRegistered(this, new FeatureInstallEventArgs{
					FeatureInfFileName = RawFeatureInfFile.Name,
					Record = this.Record,
					DetailsFromFile = this.DetailsFromFile
				});
		}

		public object Run(object parameter)
		{
			return null;
		}
		
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}

		
		public event PropertyChangedEventHandler PropertyChanged;	
		
		public event EventHandler UIOptionsElementLoaded;
		public event EventHandler UIRootElementLoaded;
		public event EventHandler NavigationElementLoaded;
		public event EventHandler OutputProducing;
		public event EventHandler OutputProduced;
		public event EventHandler InputConsuming;
		public event EventHandler InputConsumed;
		public event EventHandler FlagDetailsLoaded;
		
		public event EventHandler InstallationStarted;
		public event EventHandler InstallationCompleted;
		public event EventHandler FeatureDetailsLoaded;
		public event EventHandler ChildFeaturesDetailsLoaded;
		public event EventHandler ParentFeatureDetailsLoaded;
		public event EventHandler FeatureRegistered;
		public event EventHandler FeatureActivated;
		public event EventHandler FeatureDeactivated;
		public event EventHandler UninstallationStarted;
		public event EventHandler UninstallationCompleted;
		public event EventHandler FeatureFileValidated;
		
		public override string ToString()
		{
			return string.Format("[Feature Id={0}, Name={1}, IsActive={2}, InputFlagName={3}, OutputFlagName={4}]", Id, Name, Record.IsActive, Record.InputFlagName, Record.OutputFlagName);
		}

	}
	
	public class InputOutputPipeDataEventArgs : EventArgs
	{
		public Stack Data;
		public StackType DataType;
		public bool IsOperationCompleted;
		
		public InputOutputPipeDataEventArgs(Stack data, StackType dataType, bool isOpsCompleted)
		{
			Data = data;
			DataType = dataType;
			IsOperationCompleted = isOpsCompleted;
		}
	}
	
	public class UIRootElementLoadedEventArgs : EventArgs
	{
		public IUIRootElement RootElement;
		
		public UIRootElementLoadedEventArgs(IUIRootElement root)
		{
			RootElement = root;
		}
	}
	
	public class NavigationElementLoadedEventArgs : EventArgs
	{
		public ObservableCollection<INavigationElement> NavigationElements;
		
		public NavigationElementLoadedEventArgs(ObservableCollection<INavigationElement> elements)
		{
			NavigationElements = elements;
		}
	}
	
	public class FlagDetailsLoadedEventArgs : EventArgs
	{
		public FlagType FlagType;
		public string FlagName;
		
		public FlagDetailsLoadedEventArgs(FlagType type, string name)
		{
			FlagType = type;
			FlagName = name;
		}
	}
	
	public class FeatureInstallEventArgs: EventArgs
	{
		public string FeatureInfFileName { get; set; }
		public FeatureRecord Record { get; set; }
		public IFeatureDetails DetailsFromFile { get; set; }
	}
}
