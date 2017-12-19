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
		protected const string PROVIDERS_STORE_SCHEMA = "Providers.db";
		protected const string OBJECTS_STORE_SCHEMA = "Objects.db";
		protected const string PROPERTIES_STORE_SCHEMA = "Properties.db";
		protected const string FILE_STORE_SCHEMA = "Files.db";
		
		protected const string LAYOUTS_COLLECTION = "LayoutsCollection";
		protected const string FLAGS_COLLECTION = "FlagsCollection";
		protected const string NAMES_COLLECTION = "NamesCollection";
		protected const string FEATURES_COLLECTION = "FeaturesCollection";
		protected const string PACKAGES_INFO_COLLECTION = "PackagesInfoCollection";
		
		protected const string OBJECTS_COLLECTION = "ObjectsCollection";
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
