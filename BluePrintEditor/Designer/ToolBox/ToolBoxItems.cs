/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 13:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Description of ToolBoxItems.
	/// </summary>
	public class ToolBoxItems : ObservableCollection<ToolBoxItem>
	{
		ILog Logger = Log.Get(typeof(ToolBoxItems));
		
		public ToolBoxItems()
		{
			Logger.CollectionCreated(this);
		}
	}
}
