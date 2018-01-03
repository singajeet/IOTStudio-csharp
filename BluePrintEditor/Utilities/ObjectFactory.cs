/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/11/2017
 * Time: 6:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Linq;
using BluePrintEditor.Utilities;
using log4net;

namespace IOTStudio.Core.Stores.Providers
{
	/// <summary>
	/// Description of Assemblies.
	/// </summary>
	public class ObjectFactory
	{
		ILog Logger = Log.Get(typeof(ObjectFactory));
		
		private static ObjectFactory instance;
		
		private ObjectFactory()
		{
			Logger.InstanceCreated();
		}
		
		public static ObjectFactory Instance{
			get { 
				if (instance == null)
					instance = new ObjectFactory();
				
				return instance;
			}
		}

		#region IProvider implementation
		public Guid Id {
			get {
				return new Guid("855E8F19-68F1-40C3-A7E1-22BF466FA155");
			}
		}
		public string Name {
			get {
				return "ObjectFactory";
			}
		}
		#endregion	
		
		private IList<FileInfo> Files(string path)
		{
			Logger.DebugF("Looking for assembly files in the following path: {0}", path);
			
			IList<FileInfo> files = new List<FileInfo>();
			foreach (string file in Directory.GetFiles(path)) {
				Logger.DebugF("File {0} found in the search path", file);
				files.Add(new FileInfo(file));
			}
			
			return files;
		}
		
		public Assembly LoadAssembly(string fileName)
		{
			Logger.DebugF("Loading assembly from the following path: {0}", fileName);
			return Assembly.LoadFrom(fileName);
		}		
				
		public ObservableCollection<T> GetCollectionOfObjects<T>(string path)
		{
			Logger.DebugF("Creating object instances of type {0} from the configured path", typeof(T));
			if (!Directory.Exists(path))
				throw new Exception(string.Format("Invalid Path Provided => {0}", path));
			
			ObservableCollection<T> collection = new ObservableCollection<T>();
			
			foreach (FileInfo fileInfo in Files(path)) {
				
				if (fileInfo.Extension.ToUpper().Equals(".DLL") || fileInfo.Extension.ToUpper().Equals(".EXE")) {
					
					Assembly assembly = null;
					string executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
					
					if (fileInfo.Name.Equals(executingAssemblyName + ".exe")) {
						assembly = Assembly.GetExecutingAssembly();
						Logger.Debug("Current file is same as executing assembly; File Skipped; " +
						             "Executing assembly will be used instead");
					} else {
						assembly = LoadAssembly(fileInfo.FullName);
						Logger.Debug("Assembly loaded successfully");
					}
					
					if (assembly != null) {					
						
						Type[] types = assembly.GetTypes();
						Logger.DebugF("{0} types found in the assembly", types.Length);
						
						foreach (Type type in types) {
							Logger.DebugF("Checking whether type {0} matches the required type", type.FullName);
						
							if (typeof(T).IsInterface) {
								Logger.PropertyValue("IsInterface", typeof(T).IsInterface);
								
								if (type.GetInterface(typeof(T).FullName) != null) {
								
									object obj = Activator.CreateInstance(type);	
																		
									T instance = (T)obj;
									
									if (instance != null) {
										collection.Add(instance);
										Logger.DebugF("Type {0} matches and is added to collection", type);
									} else {
										Logger.WarnF("Unable to create instance of type {0}", type);
									}
								} else {
									Logger.DebugF("Type {0} does not match with the required type and is skipped", type);
								}
							} else {
								if (typeof(T).IsClass) {
									if(type.IsSubclassOf(typeof(T))){
										T instance = (T)Activator.CreateInstance(type);
										if (instance != null) {
											collection.Add(instance);
											Logger.DebugF("Type {0} matches and is added to collection", type);
										} else {
											Logger.WarnF("Unable to create instance of type {0}", type);
										}
									}else {
										Logger.DebugF("Type {0} does not match with the required type and is skipped", type);
									}
								} else {
									Logger.WarnF("Only class or interface types are sipported as of now");
								}
							}
						}
					}
				} else {
					Logger.DebugF("File {0} is not a valid assembely file and is skipped", fileInfo.Name);
				}
			}
			Logger.DebugF("Total {0} objects will be returned", collection.Count);
			return collection;
		}
		
		public override string ToString()
		{
			return string.Format("[ObjectFactory Id={0}, Name={1}]", Id, Name);
		} 
	}
}
