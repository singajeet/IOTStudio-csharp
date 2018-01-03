/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 22:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Description of ToolBoxHelper.
	/// </summary>
	public sealed class ToolBoxHelper
	{
		ILog Logger = Log.Get(typeof(ToolBoxHelper));
		
		private static ToolBoxHelper instance = new ToolBoxHelper();
		
		public static ToolBoxHelper Instance {
			get {
				return instance;
			}
		}
		
		private Dictionary<Guid, ToolBoxItemControl> toolBoxItems = new Dictionary<Guid, ToolBoxItemControl>();
		
		public Dictionary<Guid, ToolBoxItemControl> ToolBoxItems{
			get { return toolBoxItems; }
		}
		
		private ToolBoxItemControl selectedItem;
		
		public ToolBoxItemControl SelectedItem {
			get { return selectedItem; }
		}
		
		private ToolBoxHelper()
		{
			Logger.InstanceCreated();
		}
		
		public void RegisterItem(ToolBoxItemControl item)
		{
			if (!ToolBoxItems.ContainsKey(item.Id)) {
				ToolBoxItems.Add(item.Id, item);
				Logger.DebugF("ToolBoxItem with Guid [{0}] registered", item.Id);
			} else {
				Logger.WarnF("ToolBoxItem with Guid [{0}] is already registered", item.Id);
			}
		}
		
		public void SelectItem(ToolBoxItemControl item)
		{
			Logger.MethodCalled();
			
			if (!ToolBoxItems.ContainsKey(item.Id)) {
				Logger.ErrorF("Invalid item provided for selection => [{0}]. Item is not registered yet", item.Id);
				throw new ArgumentException(string.Format("Invalid item provided for selection => [{0}]. Item is not registered yet", item.Id));
			}
			
			if (SelectedItem == null) {
				selectedItem = item;
				SelectedItem.IsSelected = true;	
				
				Logger.SelectionChanged(item);
				
			} else if (SelectedItem.Id == item.Id) {
				Logger.DebugF("Item [{0}, Type={1}] is already selected", item, item.GetType().Name);
				return;
			} else {
				
				UnselectAllItems();
				selectedItem = item;
				SelectedItem.IsSelected = true;
				
				Logger.SelectionChanged(item);
			}
		}
		
		public ToolBoxItemControl GetSelectedItem()
		{
			var item = ToolBoxItems
						.Where(i => i.Value.IsSelected == true)
						.ToList().FirstOrDefault().Value;
			
			Logger.DebugF("Returning SelectItem => {0}", item.Id);
			return item;
		}
		
		public void UnselectAllItems()
		{
			ToolBoxItems
					.Where(i => i.Value.IsSelected == true)
					.ToList().ForEach(t => t.Value.IsSelected = false);
			
			Logger.Debug("All items Unselected");
		}
		
		public void SelectAllItems()
		{
			ToolBoxItems
					.ToList().ForEach(t => t.Value.IsSelected = true);
			
			Logger.Debug("All items are Selected");
		}
		
		public override string ToString()
		{
			return string.Format("[ToolBoxHelper]");
		}

	}
}
