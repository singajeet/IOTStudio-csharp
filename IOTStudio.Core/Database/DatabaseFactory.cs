/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 10:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Logging;
using System.Linq;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Config;

namespace IOTStudio.Core.Database
{
	/// <summary>
	/// Description of DatabaseFactory.
	/// </summary>
	public sealed class DatabaseFactory
	{
		private static DatabaseFactory instance = new DatabaseFactory();
		private Dictionary<string, IDBDriver> drivers = new Dictionary<string, IDBDriver>();
		
		public Guid Id{
			get { return new Guid("8B491B57-8276-4C3A-A7F3-F26F99823893"); }
		}
		
		public string Name{
			get { return "DatabaseFactory"; }
		}
		
		public string DBDriverClassName{
			get;
			set;
		}
		
		public string DBDriverAssemblyName{
			set;
			get;
		}
		
		public Assembly DBDriverAssembly{
			set;
			get;
		}
		
		public string DatabaseConfigSection{
			get;
			set;
		}
		
		public string AssemblyPath{
			get; set;
		}
		
		public string DBAssemblyName{
			get;
			set;
		}
		
		public Assembly DBAssembly{
			get;
			set;
		}
		
		public string DBRootNamespace{
			get;
			set;
		}
		
		public Dictionary<string, IDBDriver> DBDrivers{
			get { return drivers; }
			private set { drivers = value; }
		}
		
		public static DatabaseFactory Instance {
			get {
				return instance;
			}
		}
		
		private DatabaseFactory()
		{
			
		}
		
		/// <summary>
		/// Loads the database and return driver instance
		/// </summary>
		/// <param name="database">
		/// Database name configured in the app.config file.
		/// Pass value "DefaultDatabase" for loading default DB
		/// </param>
		/// <param name="schema">
		/// schema or database file identifier
		/// </param>
		/// <returns>Returns IDBDriver</returns>
		public IDBDriver LoadDatabase(string database, string schema) 
		{
			if (DBDrivers.ContainsKey(schema))
				return DBDrivers[schema];
			
			DatabaseConfigSection = Properties.DB.Get(database);
			if (DatabaseConfigSection == null) {
				Logger.Error("Unable to find database configuration => [{0}]", database);
				throw new Exception(string.Format("Unable to find database configuration => [{0}]", database));
			}
			
			AssemblyPath = Properties.GetGet("AssemblyPath", DatabaseConfigSection);
			Logger.Debug("Path to Database Assembly => [{0}]", AssemblyPath);
			
			DBDriverClassName = Properties.GetGet("LiteDatabaseDriver", DatabaseConfigSection);			
			Logger.Debug("Configured Database Driver => [{0}]", DBDriverClassName);
			
			DBDriverAssemblyName= Properties.GetGet("DriverAssembly", DatabaseConfigSection); 
			Logger.Debug("Driver Assembly=> [{0}]", DBDriverAssembly);
			
			DBAssemblyName = Properties.GetGet("DatabaseAssembly", DatabaseConfigSection); 
			Logger.Debug("Database Assembly=> [{0}]", DBAssembly);
			
			DBDriverAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(w => w.GetName().Name.Equals(DBDriverAssemblyName)).ToList().SingleOrDefault();
			if (DBDriverAssembly == null) {
			
				DBDriverAssembly = Get.i.Assemblies.LoadAssembly(AssemblyPath + @"\" + DBDriverAssembly + ".dll");
				Logger.Debug("Driver Assembly has been loaded successfully");
			
				DBDrivers[schema] = DBDriverAssembly.CreateInstance(DBDriverClassName) as IDBDriver;
			} else {
				Logger.Debug("Driver assembly is already loaded");
				DBDrivers[schema] = DBDriverAssembly.CreateInstance(DBDriverClassName) as IDBDriver;
			}
			
			DBAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(w => w.GetName().Name.Equals(DBAssemblyName)).ToList().SingleOrDefault();
			if (DBAssembly == null) {
				DBAssembly = Get.i.Assemblies.LoadAssembly(AssemblyPath + @"\" + DBAssemblyName + ".dll");
				Logger.Debug("Database Assembly [{0}] has been loaded successfully", DBAssembly.GetName().Name);
			} else {
				Logger.Debug("Database assembly is already loaded");
			}
			
			DBRootNamespace = DBAssembly.GetType().Namespace;
			Logger.Debug("Root Namespace for database assembly => [{0}]", DBRootNamespace);
			
			if (DBDrivers[schema] != null) {
				DBDrivers[schema].SetSchema(schema);
				return DBDrivers[schema];
			} else {
				Logger.Error("Unable to create database instance of type [{0}]", DBDriverClassName);
				throw new Exception(string.Format( "Unable to create database instance of type [{0}]", DBDriverClassName));
			}
				
		}
		
		public IDBDriver LoadDefaultDatabase(string schema)
		{
			return LoadDatabase("DefaultDatabase", schema);
		}
		
		public override string ToString()
		{
			return string.Format("[DatabaseFactory DBDriverClassName={0}, DBDriverAssemblyName={1}, DBAssemblyName={2}, DBRootNamespace={3}]", DBDriverClassName, DBDriverAssemblyName, DBAssemblyName, DBRootNamespace);
		}

	}
}
