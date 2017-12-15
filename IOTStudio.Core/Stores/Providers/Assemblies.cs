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
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Logging;

namespace IOTStudio.Core.Stores.Providers
{
	/// <summary>
	/// Description of Assemblies.
	/// </summary>
	public class Assemblies : IProvider
	{
		public Assemblies()
		{
		}

		#region IProvider implementation
		public Guid Id {
			get {
				return new Guid("EED6A94D-16D3-4553-858C-4445BD0586E5");
			}
		}
		public string Name {
			get {
				return "Assemblies";
			}
		}
		#endregion	
		
		private IList<FileInfo> Files(string path)
		{
			Logger.Debug("Looking for assembly files in the following path: {0}", path);
			
			IList<FileInfo> files = new List<FileInfo>();
			foreach (string file in Directory.GetFiles(path)) {
				Logger.Debug("File {0} found in the search path", file);
				files.Add(new FileInfo(file));
			}
			
			return files;
		}
		
		public Assembly LoadAssembly(string fileName)
		{
			Logger.Debug("Loading assembly from the following path: {0}", fileName);
			return Assembly.LoadFrom(fileName);
		}		
		
		public ObservableCollection<T> GetCollectionOfObjects<T>(string path)
		{
			Logger.Debug("Creating object instances of type {0} from the configured path", typeof(T));
			if (!Directory.Exists(path))
				throw new Exception(string.Format("Invalid Path Provided => {0}", path));
			
			ObservableCollection<T> collection = new ObservableCollection<T>();
			
			foreach (FileInfo fileInfo in Files(path)) {
				
				if (fileInfo.Extension.ToUpper().Equals(".DLL") || fileInfo.Extension.ToUpper().Equals(".EXE")) {
										
					Assembly assembly = LoadAssembly(fileInfo.FullName);
					if (assembly != null) {
						Logger.Debug("Assembly loaded successfully");
						
						Type[] types = assembly.GetTypes();
						Logger.Debug("{0} types found in the assembly", types.Length);
						
						foreach (Type type in types) {
							Logger.Debug("Checking whether type {0} matches the required type", type.FullName);
						
							if (typeof(T).IsInterface) {
								if (type.GetInterface(typeof(T).FullName) != null) {
								
									T instance = (T)Activator.CreateInstance(type);
									if (instance != null) {
										collection.Add(instance);
										Logger.Debug("Type {0} matches and is added to collection", type);
									} else {
										Logger.Warn("Unable to create instance of type {0}", type);
									}
								} else {
									Logger.Debug("Type {0} does not match with the required type and is skipped", type);
								}
							} else {
								if (typeof(T).IsClass) {
									if(type.IsSubclassOf(typeof(T))){
										T instance = (T)Activator.CreateInstance(type);
										if (instance != null) {
											collection.Add(instance);
											Logger.Debug("Type {0} matches and is added to collection", type);
										} else {
											Logger.Warn("Unable to create instance of type {0}", type);
										}
									}else {
										Logger.Debug("Type {0} does not match with the required type and is skipped", type);
									}
								} else {
									Logger.Warn("Only class or interface types are sipported as of now");
								}
							}
						}
					}
				} else {
					Logger.Debug("File {0} is not a valid assembely file and is skipped", fileInfo.Name);
				}
			}
			Logger.Debug("Total {0} objects will be returned", collection.Count);
			return collection;
		}
	}
}
