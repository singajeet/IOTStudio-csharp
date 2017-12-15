/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 7:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Providers.Stores
{
	/// <summary>
	/// Description of FileStore.
	/// </summary>
	public abstract class BaseStore : IDataStore
	{
		protected const string PROVIDERS_STORE = "Providers.db";
		protected const string OBJECTS_STORE = "Objects.db";
	}
	
//	public class DataStoreEntry
//	{
//		public int Id { get; set; }
//		public string FullName { get; set; }
//		public string PathKey { get; set; }
//		public string Extension { get; set; }
//		public string RefPath { get; set; }
//		public DataStoreObjectType ObjectType { get; set; }
//	}
}
