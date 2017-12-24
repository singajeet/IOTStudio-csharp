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
using System.Windows.Media;
using BluePrintEditor.Designer.Controls;

namespace BluePrintEditor.Designer.Actions
{
	/// <summary>
	/// Description of MoveThumb.
	/// </summary>
	public class MoveThumb : Thumb
	{
		CanvasBaseItem canvasBaseItem;
		DesignerCanvas designerCanvas;
		
		public MoveThumb()
		{
			DragStarted+= MoveThumb_DragStarted;
			DragDelta+= MoveThumb_DragDelta;
		}

		void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			this.canvasBaseItem = DataContext as CanvasBaseItem;
			if (this.canvasBaseItem != null) {
				this.designerCanvas = VisualTreeHelper.GetParent(this.canvasBaseItem) as DesignerCanvas;
			}
		}
		void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (this.canvasBaseItem != null && this.designerCanvas != null && this.canvasBaseItem.IsSelected) {
				double minLeft = double.MaxValue;
                double minTop = double.MaxValue;

                foreach (CanvasBaseItem item in this.designerCanvas.SelectedItems)
                {
                    minLeft = Math.Min(Canvas.GetLeft(item), minLeft);
                    minTop = Math.Min(Canvas.GetTop(item), minTop);
                }

                double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                double deltaVertical = Math.Max(-minTop, e.VerticalChange);

                foreach (CanvasBaseItem item in this.designerCanvas.SelectedItems)
                {
                    Canvas.SetLeft(item, Canvas.GetLeft(item) + deltaHorizontal);
                    Canvas.SetTop(item, Canvas.GetTop(item) + deltaVertical);
                }

                this.designerCanvas.InvalidateMeasure();
                e.Handled = true;
			}			
		}
	}
}
