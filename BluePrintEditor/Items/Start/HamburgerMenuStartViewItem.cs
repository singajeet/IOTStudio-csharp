/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 18:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Items.Start
{
	/// <summary>
	/// Description of HamburgerMenuStartViewItem.
	/// </summary>
	public class HamburgerMenuStartViewItem : HamburgerMenuCustomBaseItem
	{
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
					OnPropertyChanged();
			}
		}
		
		public HamburgerMenuStartViewItem()
		{
			Id = Guid.NewGuid();
			_View = new HamburgerMenuStartView();
		}
		
		public override string ToString()
		{
			return string.Format("[HamburgerMenuStartViewItem Id={0}]", id);
		}

	}
}
