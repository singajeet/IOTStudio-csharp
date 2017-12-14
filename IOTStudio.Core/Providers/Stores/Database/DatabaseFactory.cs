/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 10:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Logging;
using System.Linq;

namespace IOTStudio.Core.Providers.Stores.Database
{
	/// <summary>
	/// Description of DatabaseFactory.
	/// </summary>
	public sealed class DatabaseFactory
	{
		private static DatabaseFactory instance = new DatabaseFactory();
		
		public string DatabaseClassName{
			get;
			set;
		}
		
		public string DatabaseConfigSection{
			get;
			set;
		}
		
		public string DatabaseAssemblyPath{
			get; set;
		}
		
		public IDatabase DBInstance{
			get; private set;
		}
		
		public static DatabaseFactory Instance {
			get {
				return instance;
			}
		}
		
		private DatabaseFactory()
		{
			
		}
		
		public IDatabase LoadDatabase(string database)
		{
			DatabaseConfigSection = Properties.DB.Get(database);
			if (DatabaseConfigSection == null) {
				Logger.Error("Unable to find database configuration => [{0}]", database);
				throw new Exception("Unable to find database configuration => [{0}]", database);
			}
			
			DatabaseAssemblyPath = Properties.GetGet("AssemblyPath", DatabaseConfigSection);
			Logger.Debug("Path to Database Assembly => [{0}]", DatabaseAssemblyPath);
			
			DatabaseClassName = Properties.GetGet("DefaultLiteDatabaseType", DatabaseConfigSection);			
			Logger.Debug("Configured Database Type => [{0}]", DatabaseClassName);
			
			Assembly databaseAssembly = Get.i.Assemblies.LoadAssembly(DatabaseAssemblyPath);
			Logger.Debug("Database Assembly has been loaded successfully");
			
			DBInstance = databaseAssembly.CreateInstance(DatabaseClassName) as IDatabase;

			if (DBInstance != null)
				return DBInstance;
			else {
				Logger.Error("Unable to create database instance of type [{0}]", DatabaseClassName);
				throw new Exception("Unable to create database instance of type [{0}]", DatabaseClassName);
			}
				
		}
	}
}
