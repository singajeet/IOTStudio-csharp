/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 10:58 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Packages;
using IOTStudio.Core.Providers.Stores;
using LiteDB;

namespace IOTStudio.Core.Stores.Providers
{
	using Logger = IOTStudio.Core.Stores.Logs.Logger;
	
	public class PackageRecord
	{
		public ObjectId Id { get; set; }
		public Guid PackageKey { get; set; }
		public string Name { get; set; }
		public string PackageFilePath { get; set; }
		public string PackageFileName { get; set; }
		public long PackageFileSize { get; set; }
		public bool IsActive { get; set; }
		public bool IsInstalled { get; set; }
		public string InstalledPackageLocation { get; set; }
		public string[] Files { get; set; }
		public string[] Directories { get; set; }
		public string[] Features { get; set; }
		public bool IsUpdateAvailable { get; set; }
		public DateTime InstalledOn { get; set; }
		
		public PackageRecord()
		{
		}
		
		public PackageRecord(Guid packageKey, string name)
		{
			PackageKey = packageKey;
			Name = name;
		}
		
		public override string ToString()
		{
			return string.Format("[PackageRecord Id={0}, PackageKey={1}, Name={2}, IsActive={3}, IsInstalled={4}]", Id, PackageKey, Name, IsActive, IsInstalled);
		}

	}
	
	/// <summary>
	/// Description of PackagesStore.
	/// </summary>
	public class PackagesStore : BaseStore, IProvider
	{
		IDBDriver dbDriver;
		
		public PackagesStore()
		{
		}

		public Guid Id {
			get {
				return new Guid("C21F54D2-E157-451E-B1CA-FFA129C6BF44");
			}
		}

		public string Name {
			get {
				return "PackagesStore";
			}
		}
		
		private void CheckAndConnect(){
			if (dbDriver == null) {
				dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE_SCHEMA);
				dbDriver.Connect();
			}
		}
		
		public LiteCollection<PackageRecord> AllPackages{
			get { 
				return dbDriver.DB.GetCollection<PackageRecord>(PACKAGES_INFO_COLLECTION);
			}
		}
		
		public bool ContainsKey(Guid key)
		{
			CheckAndConnect();
			return AllPackages.Exists(p => p.PackageKey == key);
		}
		
		public bool ContainsName(string name)
		{
			CheckAndConnect();
			return AllPackages.Exists(p => p.Name.Equals(name));
		}
		
		public void RegisterPackage(Package pkg)
		{
			if (!ContainsKey(pkg.Id)) {
				PackageRecord info = pkg.Info;
				AllPackages.Insert(info);
			} else {
				throw new Exception(string.Format("[PackagesStore]: Package with key [{0}] already exists", pkg.Id));
			}
			
			Logger.Info("[PackagesStore]: Package registration has been completed successfully => [{0}]", pkg);
		}
		
		public void SavePackage(Package pkg)
		{
			#if DEBUG
			Logger.Debug("[PackagesStore]: Trying to save Package => [{0}]", pkg);
			#endif
			
			AllPackages.Upsert(pkg.Info);
			
			#if DEBUG
			Logger.Debug("[PackagesStore]: Package has been updated/inserted successfully");
			#endif
		}
		
		public PackageRecord LoadPackage(Guid key)
		{
			if (ContainsKey(key)) {
				return AllPackages.FindOne(p => p.PackageKey == key);
			} else {
				throw new Exception(string.Format("[PackagesStore]: No information found for Package with key => [{0}]", key));
			}
		}
		
		public PackageRecord LoadPackage(string name)
		{
			if (ContainsName(name)) {
				return AllPackages.FindOne(p => p.Name.Equals(name));
			} else {
				throw new Exception(string.Format("[PackagesStore]: No information found for Package with Name => [{0}]", name));
			}
		}
		
		public void UnregisterPackage(Package pkg)
		{
			if (ContainsKey(pkg.Id)) {
				AllPackages.Delete(p => p.PackageKey == pkg.Id);
			} else {
				throw new Exception(string.Format("[PackagesStore]: Can't unregister package! No such Package found => [{0}]", pkg));
			}
			
			Logger.Info("[PackagesStore]: Package has been unregistered successfully => [{0}]", pkg);
		}
		
		public override string ToString()
		{
			return string.Format("[PackagesStore Id={0}, Name={1}]", Id, Name);
		}

	}
}
