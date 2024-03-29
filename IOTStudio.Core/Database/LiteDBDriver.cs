﻿/*
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

namespace IOTStudio.Core.Database
{
	using Logger = IOTStudio.Core.Stores.Logs.Logger;
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
		public ConnectionStatus ConnectionStatus { get; private set; }
		
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
			
			if (database == null) {
				ConnectionString = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Properties.DBLite.Get("ConnectionString"));
				if (ConnectionString == null) {
					Logger.Error("Unable to find connection string for Database Tyep => LiteDatabase");
					throw new Exception("Unable to find connection string for Database Tyep => LiteDatabase");
				}
			
				Logger.Debug("Connection string loaded for LiteDatabase => [{0}]", ConnectionString);
				database = new LiteDatabase(System.IO.Path.Combine(ConnectionString, Schema));
				Logger.Debug("Connected to LiteDatabase Schema [{0}] using Connection String [{1}]", Schema, ConnectionString);
			} else {
				Logger.Debug("Connection to LiteDatabase with schema [{0}] already exists", Schema);
			}
			ConnectionStatus = ConnectionStatus.Connected;
		}
		
		public override string ToString()
		{
			return string.Format("[LiteDBDriver ConnectionString={0}, Schema={1}, ConnectionStatus={2}]", ConnectionString, Schema, ConnectionStatus);
		}



	}
}
