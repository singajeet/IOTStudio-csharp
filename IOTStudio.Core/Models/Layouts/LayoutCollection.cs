/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 10:53 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using IOTStudio.Core.Elements.UI;

namespace IOTStudio.Core.Models.Layouts
{
	/// <summary>
	/// Description of LayoutCollection.
	/// </summary>
	public class LayoutCollection : ObservableCollection<BaseLayoutElement>
	{
		public LayoutCollection()
		{
			
		}

		public BaseLayoutElement this[string name]{
			get {
				foreach (BaseLayoutElement layout in this.Items) {
					if (layout.Name.Equals(name))
						return layout;
				}
				return null;
			}
			
			set { 
				for (int i = 0; i < this.Items.Count; i++) {
					if (this.Items[i].Name.Equals(name))
						this.SetItem(i, value);
				}
			}
		}
		
	}
}
