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
using IOTStudio.Core.Stores.Config;
using IOTStudio.Core.Types;
using LiteDB;

namespace IOTStudio.Core.Providers.Stores.Database
{
	using Logger = IOTStudio.Core.Providers.Logging.Logger;
	/// <summary>
	/// Description of DefaultDatabase.
	/// </summary>
	public class LiteDBDriver : IDBDriver
	{		
		private LiteDatabase database;
		
		public LiteDBDriver()
		{
			ConnectionStatus = ConnectionStatus.Disconnected;
		}
		
		public string ConnectionString { get; private set; }
		public string Schema { get; private set; }
		ConnectionStatus ConnectionStatus { get; private set; }
		
		public LiteDatabase DB{
			get { 
				return database ?? new LiteDatabase(ConnectionString);
			}
		}
		
		public void SetSchema(string schema)
		{
			Schema = schema;
		}
		
		public void Connect()
		{
			ConnectionStatus = ConnectionStatus.Connecting;
			
			ConnectionString = Properties.DBLite.Get("ConnectionString");			
			if (ConnectionString == null) {
				Logger.Error("Unable to find connection string for Database Tyep => LiteDatabase");
				throw new Exception("Unable to find connection string for Database Tyep => LiteDatabase");
			}
			
			Logger.Debug("Connection string loaded for LiteDatabase => [{0}]", ConnectionString);
			database = new LiteDatabase(ConnectionString + @"\" + Schema);
			Logger.Debug("Connected to LiteDatabase Schema [{0}] using Connection String [{1}]", Schema, ConnectionString);
			
			ConnectionStatus = ConnectionStatus.Connected;
		}
		
//		public LiteCollection<T> GetCollection<T>(string collectionName)
//		{
//			if (database == null)
//				Connect();
//			
//			return database.GetCollection<T>(collectionName);
//		}
//		
//		public LiteStorage FileStore{
//			get {
//				if (database == null)
//					Connect();
//				
//				return database.FileStorage; 
//			}
//		}
		
		public override string ToString()
		{
			return string.Format("[LiteDBDriver ConnectionString={0}, Schema={1}, ConnectionStatus={2}]", ConnectionString, Schema, ConnectionStatus);
		}



	}
}
