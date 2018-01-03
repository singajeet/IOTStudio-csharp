/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 1/1/2018
 * Time: 1:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using BluePrintEditor.Designer.ToolBox.Interfaces;
using BluePrintEditor.Designer.ToolBox.Items;
using BluePrintEditor.Utilities;
using System.Linq;
using IOTStudio.Core.Stores.Providers;
using log4net;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Description of ToolBoxItemLoader.
	/// </summary>
	public sealed class ToolBoxItemLoader
	{
		private ILog Logger = Log.Get(typeof(ToolBoxItemLoader));
		private static ToolBoxItemLoader instance = new ToolBoxItemLoader();
		
		public static ToolBoxItemLoader Instance {
			get {
				return instance;
			}
		}
		
		private ToolBoxItemLoader()
		{
			ToolBoxItemsPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			ToolBoxItemsPath = System.IO.Path.Combine(ToolBoxItemsPath,
			                                          Config.GetSetting("ToolItemsPath"));
		}
		
		private Assembly interfaceAssembly;		
		public Assembly InterfaceAssembly {
			get { return interfaceAssembly; }
			set { 
					interfaceAssembly = value;
				Logger.PropertyChanged(value);
			}
		}
		
		private string interfaceAssemblyName;		
		public string InterfaceAssemblyName {
			get { return interfaceAssemblyName; }
			set { 
					interfaceAssemblyName = value;
				Logger.PropertyChanged(value);
			}
		}
		
		private string toolBoxItemsPath;		
		public string ToolBoxItemsPath {
			get { return toolBoxItemsPath; }
			set { toolBoxItemsPath = value;
				Logger.PropertyChanged(value);
			}
		}
		
		ObservableCollection<IToolItem> toolBoxItems;		
		public ObservableCollection<IToolItem> ToolBoxItems {
			get {
				toolBoxItems = ObjectFactory.Instance.GetCollectionOfObjects<IToolItem>(ToolBoxItemsPath);
				return toolBoxItems; 
			}
		}
		
		ObservableCollection<string> sections;
		public ObservableCollection<string> Sections{
			get { 
				sections = new ObservableCollection<string>(ToolBoxItems
				                                 .Select(a => a.Category)
				                                 .Distinct());
				return sections;
			}
		}
	}
}
