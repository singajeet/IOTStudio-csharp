/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 20:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BluePrintEditor.Items.Editor
{
	/// <summary>
	/// Description of HamburgerMenuEditorViewItem.
	/// </summary>
	public class HamburgerMenuEditorViewItem: HamburgerMenuCustomBaseItem
	{
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
					OnPropertyChanged();
			}
		}
		public HamburgerMenuEditorViewItem()
		{ 
			Id = Guid.NewGuid();
			_View = new HamburgerMenuEditorView();
		}
		
		public override string ToString()
		{
			return string.Format("[HamburgerMenuEditorViewItem Id={0}]", id);
		}

	}
}
