/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/15/2017
 * Time: 10:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Stores;
using System.Linq;
using LiteDB;

namespace IOTStudio.Core.Stores.Objects
{
	using Logger = IOTStudio.Core.Stores.Logs.Logger;
	/// <summary>
	/// Description of DefaultObjectStore.
	/// </summary>
	public class DefaultObjectsStore : BaseStore, IProvider
	{
		IDBDriver dbDriver;
		public Guid Id {
			get {
				return new Guid("D846AF30-248D-43E6-A8D7-A8CE6B3FED18");
			}
		}
	
		public string Name {
			get {
				return "DefaultObjectStore";
			}
		}

		public DefaultObjectsStore()
		{
			dbDriver = Get.i.DBFactory.LoadDefaultDatabase(OBJECTS_STORE_SCHEMA);
			dbDriver.Connect();
		}
		
		public T Load<T>(string key)
		{
			object instance = dbDriver.DB.GetCollection<T>(OBJECTS_COLLECTION).FindOne(Query.EQ("Name", key));
			
			if (instance != null) {
				Logger.Debug("Object [{0}] found and same will be returned", (T)instance);
			} else {
				Logger.Debug("No object exists with Key [{0}] of type [{1}]", key, typeof(T).FullName);
			}
			
			return (T)instance;
		}
		
		public void Save<T>(T instance)
		{
			dbDriver.DB.GetCollection<T>("collection").Insert(instance);
			
			Logger.Debug("Object [{0}] has been saved successfully", instance);
		}
		
		public override string ToString()
		{
			return string.Format("[DefaultObjectStore Id={0}, Name={1}]", Id, Name);
		}
	}
}
