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

namespace IOTStudio.Core.Providers.Assemblies
{
	/// <summary>
	/// Description of AssemblyLoader.
	/// </summary>
	public static class AssemblyLoader
	{
		private static IList<FileInfo> Files(string path)
		{
			IList<FileInfo> files = new List<FileInfo>();
			foreach (string file in Directory.GetFiles(path)) {
				files.Add(new FileInfo(file));
			}
			
			return files;
		}
		
		public static Assembly LoadAssembly(string fileName)
		{
			return Assembly.LoadFrom(fileName);
		}		
		
		public static ObservableCollection<T> GetCollectionOfObjects<T>(string path)
		{
			if (!Directory.Exists(path))
				throw new Exception(string.Format("Invalid Path Provided => {0}", path));
			
			ObservableCollection<T> collection = new ObservableCollection<T>();
			
			foreach (FileInfo fileInfo in Files(path)) {
				
				if (fileInfo.Extension.ToUpper().Equals("DLL") || fileInfo.Extension.ToUpper().Equals("EXE")) {
					
					Assembly assembly = LoadAssembly(fileInfo.FullName);
					if (assembly != null) {
						Type[] types = assembly.GetTypes();
						foreach (Type type in types) {
							if (type == typeof(T)) {
								T instance = Activator.CreateInstance<T>();
								collection.Add(instance);
							}
						}
					}
				}
			}
			return collection;
		}
	}
}
