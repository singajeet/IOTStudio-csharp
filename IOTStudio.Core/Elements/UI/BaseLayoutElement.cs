/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 7:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Interactivity;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Elements.UI
{
	
	public class LayoutInformation
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public string Company { get; set; }
		public string Version { get; set; }
		public DateTime ReleasedDate { get; set; }
		public string URL { get; set; }
		
		public LayoutInformation(){
			Id = new Guid("9D973ECC-6A67-42E6-8F22-8455F861AFCA");
			Name = "ProvideNameForLayout";
			Description = "Enter description of the layout";
			Author = "Ajeet Singh";
			Company = "Armin Inc";
			Version = "1.0.0.0";
			ReleasedDate = DateTime.Parse("12/17/2017", new DateTimeFormat("MM/DD/YYYY").FormatProvider);
			URL = "http://adapt-tech.org";
		}
		
		public override string ToString()
		{
			return string.Format("[LayoutInformation Id={0}, Name={1}, Version={2}]", Id, Name, Version);
		}

	}
	
	/// <summary>
	/// Description of BaseLayoutElement.
	/// </summary>
	[DataContract]
	public class BaseLayoutElement : DependencyObject, INotifyPropertyChanged, IBaseElement, ILayoutElement
	{
		private string _AssemblyBasePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		private string _ConfiguredPathForLayouts = Properties.Layouts.Get("SavePath");
		private string _ClassNameAsFolder;
		private string _InfoFileName;
		private string _LayoutXmlFileName;
		private const string _InfoFileExtension = ".inf";
		private const string _LayoutFileExtension = ".xml";
		
		private LayoutInformation _layoutInfo;
		public LayoutInformation LayoutInfo{
			get {
				if(_layoutInfo == null)
					_layoutInfo = new LayoutInformation();
				return this._layoutInfo; 
			}
			set { _layoutInfo = value; }
		}
		
		public BaseLayoutElement()
		{
			_ClassNameAsFolder = this.GetType().Name;
			Logger.Debug("Layouts sub folder name => [{0}]", _ClassNameAsFolder);
			
			UpdateLayoutInfo(FileActionType.Read);		
			
			_LayoutXmlFileName = this.Name + "_Layout" + _LayoutFileExtension;
			Logger.Debug("Layout's Xml file name => [{0}]", _LayoutXmlFileName);
			
			PathToXml = System.IO.Path.Combine(
								System.IO.Path.Combine(
									System.IO.Path.Combine(_AssemblyBasePath,
										_ConfiguredPathForLayouts),
									_ClassNameAsFolder),
								_LayoutXmlFileName);
			
			Logger.Debug("Path to Xml layout file => [{0}]", PathToXml);
			UpdateLayoutXmlFile(Name);
			
			IsSelected = false;
			
		}
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
		
		protected void UpdateLayoutInfo(FileActionType action)
		{
			string baseDirForInfoFile = null;		
			
			baseDirForInfoFile = System.IO.Path.Combine(_AssemblyBasePath, _ConfiguredPathForLayouts);
			baseDirForInfoFile = System.IO.Path.Combine(baseDirForInfoFile, _ClassNameAsFolder);
			Logger.Debug("Base path for layouts => [{0}]", baseDirForInfoFile);
			
			//Check if the above path exists or not
			if (System.IO.Directory.Exists(baseDirForInfoFile)) {
				Logger.Debug("Base path for layouts found; Trying to locate *.inf files");
				
				//lookout for info file
				string[] files = System.IO.Directory.GetFiles(baseDirForInfoFile, "*" + _InfoFileExtension);
				if (files.Length > 0) {
					Logger.Debug("[{0}] layout's .inf file(s) found", files.Length);
					
					if (action == FileActionType.Read || action == FileActionType.Open) {
						Logger.Debug("Layout's information load action started...");
						
						//If path exists, check for the info file
						YamlDotNet.Serialization.Deserializer deserializer = new YamlDotNet.Serialization.Deserializer();
						Logger.Debug("Reading info data from file [{0}]", files[0]);
					
						using (System.IO.TextReader reader = System.IO.File.OpenText(files[0])) {
							LayoutInfo = deserializer.Deserialize<LayoutInformation>(reader);
						}
						
						Logger.Debug("Layout's information has been loaded successfully");
						
						baseElement.Id = LayoutInfo.Id;
						baseElement.Name = LayoutInfo.Name;
					
					} else if (action == FileActionType.Write || action == FileActionType.Save) {
						Logger.Debug("Layout's information update started...");
						
						//Save the info in YAML format
						YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
						System.IO.StringWriter strWriter = new System.IO.StringWriter();
						serializer.Serialize(strWriter, LayoutInfo);
						
						System.IO.File.Delete(files[0]);
						
						using (System.IO.TextWriter writer = System.IO.File.CreateText(files[0])) {
							writer.Write(strWriter.ToString());
						}
						
						Logger.Debug("Layout's information updated successfully to [{0}]", files[0]);
					} else {
						Logger.Debug("Invalid action specified for this operation => [{0}]", action);
					}
				} else {
					//File not found
					Logger.Debug("Unable to find layout info file; New file will be created...");
					
					//Since no info file found, assign a new Name to the layout
					if (Name == null) {
						baseElement.Id = LayoutInfo.Id = new Guid();
						baseElement.Name = LayoutInfo.Name = Get.i.Names.GetName("Layout");
						Logger.Debug("Assigned Id={0}, Name={1}", Id, Name);
					}
					
					_InfoFileName = this.Name.Replace(' ', '_') + "_Info" + _InfoFileExtension;
					Logger.Debug("Layout information save file path => [{0}]", _InfoFileName);
					
					//Save the info in YAML format
					YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
					System.IO.StringWriter strWriter = new System.IO.StringWriter();
					serializer.Serialize(strWriter, LayoutInfo);
					using (System.IO.TextWriter writer = System.IO.File.CreateText(System.IO.Path.Combine(baseDirForInfoFile, _InfoFileName))) {
						writer.Write(strWriter.ToString());
					}
					
					Logger.Debug("Layout's information file created and saved successfully");
				}
			} else {
				
				Logger.Debug("Layout's base directory not found; New directory will be created...");
				//No dir exists, try to create one
				System.IO.DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(baseDirForInfoFile);
				
				//Since no dir and info files exists, assign a new Name to layout
				if (Name == null) {
					baseElement.Id = LayoutInfo.Id = new Guid();
					baseElement.Name = LayoutInfo.Name = Get.i.Names.GetName("Layout");
					Logger.Debug("Assigned Id={0}, Name={1}", Id, Name);
				}
				
				_InfoFileName = this.Name.Replace(' ', '_') + "_Info" + _InfoFileExtension;
				Logger.Debug("Layout information save file path => [{0}]", _InfoFileName);
				
				//Save the info in YAML format
				YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
				System.IO.StringWriter strWriter = new System.IO.StringWriter();
				serializer.Serialize(strWriter, LayoutInfo);
				using (System.IO.TextWriter writer = System.IO.File.CreateText(System.IO.Path.Combine(dirInfo.FullName, _InfoFileName))) {
					writer.Write(strWriter.ToString());
				}
					
				Logger.Debug("Layout's base directory & file created and saved successfully");
			}
		}
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}

		private BaseElement baseElement = new BaseElement();
		
		[DataMember]
		public Guid Id {
			get {
				return baseElement.Id;
			}
			set {
				baseElement.Id = value;
				LayoutInfo.Id = value;
				UpdateLayoutInfo(FileActionType.Save);
				OnPropertyChanged();
			}
		}
		
		[DataMember]
		public string Name {
			get { return baseElement.Name; }
			set {
				baseElement.Name = value; 
				LayoutInfo.Name = value;
				UpdateLayoutInfo(FileActionType.Save);
				UpdateLayoutXmlFile(value);
				OnPropertyChanged();
			}
		}

		[DataMember]
		public ObservableCollection<Behavior<UIElement>> Behaviors {
			get {
				return baseElement.Behaviors;
			}
			set {
				baseElement.Behaviors = value;
				OnPropertyChanged();
			}
		}

		[DataMember]
		public ObservableCollection<TriggerAction<UIElement>> EventTriggers {
			get {
				return baseElement.EventTriggers;
			}
			set {
				baseElement.EventTriggers = value;
				OnPropertyChanged();
			}
		}

		
		public static readonly DependencyProperty LayoutTypeProperty =
			DependencyProperty.Register("LayoutType", typeof(LayoutElementType), typeof(BaseLayoutElement),
			                            new FrameworkPropertyMetadata());
		
		public LayoutElementType LayoutType {
			get { return (LayoutElementType)GetValue(LayoutTypeProperty); }
			set { SetValue(LayoutTypeProperty, value); }
		}
		
		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected", typeof(bool), typeof(BaseLayoutElement),
			                            new FrameworkPropertyMetadata());
		
		public bool IsSelected {
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}
		
		public static readonly DependencyProperty PathToXmlProperty =
			DependencyProperty.Register("PathToXml", typeof(string), typeof(BaseLayoutElement),
			                            new FrameworkPropertyMetadata());
		
		public string PathToXml {
			get { return (string)GetValue(PathToXmlProperty); }
			set { SetValue(PathToXmlProperty, value); }
		}
		
		/// <summary>
		/// Function will be called whenever Name property of layout changes
		/// </summary>
		/// <param name="name">New value of Name Property</param>
		protected void UpdateLayoutXmlFile(string name)
		{
			Logger.Debug("Layout's Xml file update started...");
			//If file already exists with old name, rename file to new name using move command
			if (System.IO.File.Exists(PathToXml)) {
				Logger.Debug("Provided path is a path to file");
				
				_LayoutXmlFileName = this.Name.Replace(' ', '_') + "_Layout" + _LayoutFileExtension;
				
				string newPath = System.IO.Path.Combine(
									System.IO.Path.Combine(
										System.IO.Path.Combine(_AssemblyBasePath,
				                                               _ConfiguredPathForLayouts),
										_ClassNameAsFolder), 
									_LayoutXmlFileName);
				
				Logger.Debug("Found an existing layout's Xml file => [{0}]", PathToXml);
				
				if (!System.IO.File.Exists(newPath)) {
					System.IO.File.Move(PathToXml, newPath);
					PathToXml = newPath;
					
					Logger.Debug("Successfully renamed existing layout's Xml to => [{0}]", newPath);
				} else {
					//If an another file already exists with the new Name, get number of files 
					//starting with "Name*" and rename this file with name as "Name_<FileCount>.xml"
					int count = System.IO.Directory.GetFiles(
									System.IO.Path.Combine(
										System.IO.Path.Combine(_AssemblyBasePath,
						                      					 _ConfiguredPathForLayouts), 
											_ClassNameAsFolder), 
									name.Replace(' ', '_') + "*" + _LayoutFileExtension).Length;
					
					Logger.Debug("Multiple layout's Xml files found => [{0}] file(s)", count);
					
					_LayoutXmlFileName = this.Name.Replace(' ', '_') + "_Layout_" + count + _LayoutFileExtension;
					
					newPath = System.IO.Path.Combine(
								System.IO.Path.Combine(
									System.IO.Path.Combine(_AssemblyBasePath,
							                       _ConfiguredPathForLayouts),
										_ClassNameAsFolder), 
								_LayoutXmlFileName);
					
					System.IO.File.Move(PathToXml, newPath);
					PathToXml = newPath;
					
					Logger.Debug("Successfully renamed existing layout's Xml to => [{0}]", newPath);
				}
			} else {
				Logger.Debug("Layout's Xml file don't exist; Creating new Xml file");
				
//				if (!System.IO.File.GetAttributes(PathToXml).HasFlag(System.IO.FileAttributes.Directory)) {
					System.IO.File.Create(PathToXml).Close();
//				} else {
//					if (System.IO.File.GetAttributes(PathToXml).HasFlag(System.IO.FileAttributes.Directory)) {
//						//If path provided is a path to directory, create new layout file
//						if (System.IO.Directory.Exists(PathToXml)) {
//								_LayoutXmlFileName = this.Name.Replace(' ', '_') + "_Layout" + _LayoutFileExtension;
//								
//								PathToXml = System.IO.Path.Combine(
//													System.IO.Path.Combine(
//														System.IO.Path.Combine(_AssemblyBasePath,
//															_ConfiguredPathForLayouts),
//														_ClassNameAsFolder),
//													_LayoutXmlFileName);
//								
//							string newPath = PathToXml;
//							if (!System.IO.File.Exists(newPath)) {
//								PathToXml = newPath;
//								System.IO.File.CreateText(newPath).Close();						
//							} else {
//								int count = System.IO.Directory.GetFiles(System.IO.Path.Combine(
//																	System.IO.Path.Combine(_AssemblyBasePath,
//								                      									 _ConfiguredPathForLayouts), 
//																					_ClassNameAsFolder), 
//																					name.Replace(' ', '_') + "*" + _LayoutFileExtension).Length;
//								
//								_LayoutXmlFileName = this.Name.Replace(' ', '_') + "_Layout_" + count + _LayoutFileExtension;
//							
//								newPath = System.IO.Path.Combine(
//											System.IO.Path.Combine(
//												System.IO.Path.Combine(_AssemblyBasePath,
//										                       _ConfiguredPathForLayouts),
//													_ClassNameAsFolder), 
//											_LayoutXmlFileName);
//								
//								PathToXml = newPath;
//								System.IO.File.CreateText(newPath).Close();
//							}
//						} else {
//							throw new Exception(string.Format("Invalid path provided for saving layout's XML files => [{0}]", PathToXml));
//						}
//					}
//				}
				
				
			}
		}
		
		public virtual void DeserializeFromXml()
		{
			
		}
		
		public virtual void SerializeToXml()
		{
			
		}
		
	}
}
