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
	
	public class Layout
	{
		public ObjectId Id { get; set; }
		public string Key { get; set; }
		//public BaseLayoutElement LayoutElement { get; set; }
		public string PathToXml { get; set; }
		public bool IsSelected { get; set; }
		
		public Layout()
		{
		}
		
		public Layout(string key, bool isSelected, string path)
		{
			Key = key;
			//LayoutElement = element;
			PathToXml = path;
			IsSelected = isSelected;
		}
		
		public override string ToString()
		{
			return string.Format("[Layout Id={0}, Key={1}, IsSelected={2}, PathToXml={3}]", Id, Key, IsSelected, PathToXml);
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

		public LiteCollection<Layout> AllLayouts{
			get { return dbDriver.DB.GetCollection<Layout>(LAYOUTS_COLLECTION); }
		}
		#endregion
		
		public bool ContainsKey(string key)
		{
			CheckAndConnect();
			
			return AllLayouts.Exists(l => l.Key.Equals(key));
		}
		
		public void InsertLayout(BaseLayoutElement layout)
		{
			CheckAndConnect();
			
			if (ContainsKey(layout.Name))
				throw new Exception(string.Format("Layout with key [{0}] already exists", layout.Name));
			Layout layoutObject = new Layout(layout.Name, layout.IsSelected, null);
			AllLayouts.Insert(layoutObject);
			
			Logger.Debug("Layout [{0}] has been inserted successfully", layoutObject);
		}
		
		public void SaveLayout(BaseLayoutElement layout)
		{
			if (ContainsKey(layout.Name)) {
				Layout layoutObject = AllLayouts.FindOne(l => l.Key.Equals(layout.Name));
				layoutObject.IsSelected = layout.IsSelected;
				layoutObject.PathToXml = null;
				
				AllLayouts.Update(layoutObject);
				Logger.Debug("Layout [{0}] has been updated successfully", layoutObject);
			} else {
				Layout layoutObject = new Layout(layout.Name, layout.IsSelected, null);
				AllLayouts.Insert(layoutObject);
				
				Logger.Debug("Layout [{0}] has been inserted successfully", layoutObject);
			}
		}
		
		public Layout LoadLayout(string key)
		{
			CheckAndConnect();
			
			if (!ContainsKey(key))
				throw new Exception(string.Format("No layout exists with key [{0}]", key));
			
			Logger.Debug("Loading layout with key [{0}]", key);
			
			return AllLayouts.FindOne(f => f.Key.Equals(key));
		}
		
		public void DeleteLayout(BaseLayoutElement layout)
		{
			CheckAndConnect();
			
			if (!ContainsKey(layout.Name))
				throw new Exception(string.Format("No layout exists with key [{0}]", layout.Name));
			
			AllLayouts.Delete(l => l.Key.Equals(layout.Name));
			Logger.Debug("Layout [{0}] has been deleted successfully", layout);
		}
		
		public override string ToString()
		{
			return string.Format("[LayoutsStore Id={0}, Name={1}]", Id, Name);
		}

	}
}
