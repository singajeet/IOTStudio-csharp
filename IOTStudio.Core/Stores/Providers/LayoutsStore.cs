/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/15/2017
 * Time: 3:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Elements.UI;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Providers.Stores;
using LiteDB;

namespace IOTStudio.Core.Stores.Providers
{
using Logger=IOTStudio.Core.Stores.Logs.Logger;
	
	public class LayoutRecord
	{
		public ObjectId Id { get; set; }
		public Guid LayoutKey { get; set; }
		public string Name { get; set; }		
		public string PathToXml { get; set; }
		public bool IsSelected { get; set; }
		
		public LayoutRecord()
		{
		}
		
		public LayoutRecord(Guid layoutKey, string name, bool isSelected, string path)
		{
			LayoutKey = layoutKey;
			Name = name;			
			PathToXml = path;
			IsSelected = isSelected;
		}
		
		public override string ToString()
		{
			return string.Format("[LayoutRecord Id={0}, LayoutKey={1}, Name={2}, IsSelected={3}, PathToXml={4}]", Id, LayoutKey, Name, IsSelected, PathToXml);
		}

	}
	/// <summary>
	/// Description of Layouts.
	/// </summary>
	public class LayoutsStore : BaseStore, IProvider
	{
		IDBDriver dbDriver;
		
		public LayoutsStore()
		{
			
		}
		
		private void CheckAndConnect()
		{
			if (dbDriver == null) {
				dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE_SCHEMA);
				dbDriver.Connect();
			}
		}

		#region IProvider implementation

		public Guid Id {
			get {
				return new Guid("138770D0-1958-4019-BF18-BD25B3D60CF8");
			}
		}

		public string Name {
			get {
				return "LayoutsStore";
			}
		}

		public LiteCollection<LayoutRecord> AllLayouts{
			get { return dbDriver.DB.GetCollection<LayoutRecord>(LAYOUTS_COLLECTION); }
		}
		#endregion
		
		public bool ContainsKey(Guid layoutKey)
		{
			CheckAndConnect();
			
			return AllLayouts.Exists(l => l.LayoutKey == layoutKey);
		}
		
		public bool ContainsName(string name)
		{
			CheckAndConnect();
			return AllLayouts.Exists(l => l.Name.Equals(name));
		}
		
		public void InsertLayout(BaseLayoutElement layout)
		{
			CheckAndConnect();
			
			if (ContainsKey(layout.Id))
				throw new Exception(string.Format("Layout [{0}] with key [{1}] already exists", layout.Name, layout.Id));
			
			LayoutRecord layoutObject = new LayoutRecord(layout.Id, layout.Name, layout.IsSelected, layout.PathToXml);
			AllLayouts.Insert(layoutObject);
			
			Logger.Debug("Layout [{0}] has been inserted successfully", layoutObject);
		}
		
		public void SaveLayout(BaseLayoutElement layout)
		{
			if (ContainsKey(layout.Id)) {
				LayoutRecord layoutObject = AllLayouts.FindOne(l => l.LayoutKey == layout.Id);
				layoutObject.Name = layout.Name;
				layoutObject.IsSelected = layout.IsSelected;
				layoutObject.PathToXml = layout.PathToXml;
				
				AllLayouts.Update(layoutObject);
				Logger.Debug("Layout [{0}] has been updated successfully", layoutObject);
			} else {
				LayoutRecord layoutObject = new LayoutRecord(layout.Id, layout.Name, layout.IsSelected, layout.PathToXml);
				AllLayouts.Insert(layoutObject);
				
				Logger.Debug("Layout [{0}] has been inserted successfully", layoutObject);
			}
		}
		
		public LayoutRecord LoadLayout(Guid key)
		{
			CheckAndConnect();
			
			if (!ContainsKey(key))
				throw new Exception(string.Format("No layout exists with key [{0}]", key));
			
			Logger.Debug("Loading layout with key [{0}]", key);
			
			return AllLayouts.FindOne(f => f.LayoutKey == key);
		}
		
		public LayoutRecord LoadLayout(string name)
		{
			CheckAndConnect();
			
			if (!ContainsName(name))
				throw new Exception(string.Format("No layout exists with Name = [{0}]", name));
			
			Logger.Debug("Loading layout with Name = [{0}]", name);
			
			return AllLayouts.FindOne(f => f.Name.Equals(name));
		}
		
		public void DeleteLayout(BaseLayoutElement layout)
		{
			CheckAndConnect();
			
			if (!ContainsKey(layout.Id))
				throw new Exception(string.Format("No layout exists with key [{0}]", layout.Id));
			
			AllLayouts.Delete(l => l.LayoutKey == layout.Id);
			Logger.Debug("Layout [{0}] has been deleted successfully", layout);
		}
		
		public void UnselectAll()
		{
			foreach (LayoutRecord layout in AllLayouts.FindAll()) {
				layout.IsSelected = false;
				AllLayouts.Update(layout);
			}
		}
		
		public override string ToString()
		{
			return string.Format("[LayoutsStore Id={0}, Name={1}]", Id, Name);
		}

	}
}
