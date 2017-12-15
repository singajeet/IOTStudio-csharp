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
		public string Key;
		public BaseLayoutElement LayoutElement;
		public string PathToXml;
		
		public Layout(string key, BaseLayoutElement element, string path)
		{
			Key = key;
			LayoutElement = element;
			PathToXml = path;
		}
		
		public override string ToString()
		{
			return string.Format("[Layout Key={0}, PathToXml={1}]", Key, PathToXml);
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
			dbDriver = Get.i.DBFactory.LoadDefaultDatabase(PROVIDERS_STORE);
			dbDriver.Connect();
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
			get { return dbDriver.DB.GetCollection<Layout>("layouts"); }
		}
		#endregion
		
		public bool ContainsKey(string key)
		{
			return AllLayouts.Exists(l => l.Key.Equals(key));
		}
		public void SaveLayout(BaseLayoutElement layout)
		{
			if (ContainsKey(layout.Name))
				throw new Exception(string.Format("Layout with key [{0}] already exists", layout.Name));
			Layout lay = new Layout(layout.Name, layout, null);
			AllLayouts.Insert(lay);
			
			Logger.Debug("Layout [{0}] has been saved successfully", layout);
		}
		
		public BaseLayoutElement LoadLayout(string key)
		{
			if (!ContainsKey(key))
				throw new Exception(string.Format("No layout exists with key [{0}]", key));
			
			Logger.Debug("Loading layout with key [{0}]", key);
			
			return AllLayouts.FindOne(f => f.Key.Equals(key)).LayoutElement;
		}
		
		public void DeleteLayout(BaseLayoutElement layout)
		{
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
