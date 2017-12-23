/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/23/2017
 * Time: 10:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace BluePrintEditor.Designer.Actions
{
	/// <summary>
	/// Description of MoveThumb.
	/// </summary>
	public class MoveThumb : Thumb
	{
		public MoveThumb()
		{
			DragDelta+= MoveThumb_DragDelta;
		}

		void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			ContentControl item = this.DataContext as ContentControl;
			if (item != null) {
				
				double left = Canvas.GetLeft(item);
				double top = Canvas.GetTop(item);
				Canvas.SetLeft(item, left + e.HorizontalChange);
				Canvas.SetTop(item, top + e.VerticalChange);
			}
		}
	}
}
