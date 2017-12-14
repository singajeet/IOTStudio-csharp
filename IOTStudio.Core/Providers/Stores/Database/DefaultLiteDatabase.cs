/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 10:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Interfaces;
using LiteDB;

namespace IOTStudio.Core.Providers.Stores.Database
{
	using Logger = IOTStudio.Core.Providers.Logging.Logger;
	/// <summary>
	/// Description of DefaultDatabase.
	/// </summary>
	public class DefaultLiteDatabase : IDatabase
	{		
		private LiteDatabase database;
		
		public DefaultLiteDatabase()
		{
			ConnectionString = Properties.DBLite.Get("ConnectionString");			
			if (ConnectionString == null) {
				Logger.Error("Unable to find connection string for Database Tyep => LiteDatabase");
				throw new Exception("Unable to find connection string for Database Tyep => LiteDatabase");
			}
			
			Logger.Debug("Connection string loaded for LiteDatabase => [{0}]", ConnectionString);
		}
		
		public string ConnectionString { get; set; }
		
		public LiteDatabase DB{
			get { 
				return database ?? new LiteDatabase(ConnectionString);
			}
		}
		
		public void Connect()
		{
			database = new LiteDatabase(ConnectionString);
			Logger.Debug("Connected to LiteDatabase using Connection String [{0}]", ConnectionString);
		}
		
		public LiteCollection<T> GetCollection<T>(string collectionName)
		{
			if (database == null)
				Connect();
			
			return database.GetCollection<T>(collectionName);
		}
		
		public LiteStorage FileStore{
			get {
				if (database == null)
					Connect();
				
				return database.FileStorage; 
			}
		}
	}
}
