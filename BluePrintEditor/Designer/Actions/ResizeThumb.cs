/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/23/2017
 * Time: 12:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using BluePrintEditor.Designer.Adorners;
using BluePrintEditor.Designer.Controls;

namespace BluePrintEditor.Designer.Actions
{
	/// <summary>
	/// Description of ResizeThumb.
	/// </summary>
	public class ResizeThumb : Thumb
	{
		private CanvasBaseItem canvasBaseItem;
		private DesignerCanvas designerCanvas;
		private Adorner adorner;
		
		public ResizeThumb()
		{
			DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
			DragStarted += new DragStartedEventHandler(ResizeThumb_DragStarted);
			DragCompleted += new DragCompletedEventHandler(ResizeThumb_DragCompleted);
		}

		void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			canvasBaseItem = this.DataContext as CanvasBaseItem;
			if (canvasBaseItem != null) {
				designerCanvas = VisualTreeHelper.GetParent(canvasBaseItem) as DesignerCanvas;
//				if (canvas != null) {
//					AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.canvas);
//					if (adornerLayer != null) {
//						adorner = new SizeAdorner(this.canvasBaseItem);
//						adornerLayer.Add(adorner);
//					}
//				}
			}
		}

		void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
//			if (this.adorner != null) {
//				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.canvas);
//				if (adornerLayer != null) {
//					adornerLayer.Remove(adorner);
//				}
//				this.adorner = null;
//			}
		}
		private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			if (this.canvasBaseItem != null && this.designerCanvas != null && this.canvasBaseItem.IsSelected)
            {
                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;
                double minDeltaHorizontal = double.MaxValue;
                double minDeltaVertical = double.MaxValue;
                double dragDeltaVertical, dragDeltaHorizontal;

                foreach (CanvasBaseItem item in this.designerCanvas.SelectedItems)
                {
                    minLeft = Math.Min(Canvas.GetLeft(item), minLeft);
                    minTop = Math.Min(Canvas.GetTop(item), minTop);

                    minDeltaVertical = Math.Min(minDeltaVertical, item.ActualHeight - item.MinHeight);
                    minDeltaHorizontal = Math.Min(minDeltaHorizontal, item.ActualWidth - item.MinWidth);
                }

                foreach (CanvasBaseItem item in this.designerCanvas.SelectedItems)
                {
                    switch (VerticalAlignment)
                    {
                        case VerticalAlignment.Bottom:
                            dragDeltaVertical = Math.Min(-e.VerticalChange, minDeltaVertical);
                            item.Height = item.ActualHeight - dragDeltaVertical;
                            break;
                        case VerticalAlignment.Top:
                            dragDeltaVertical = Math.Min(Math.Max(-minTop, e.VerticalChange), minDeltaVertical);
                            Canvas.SetTop(item, Canvas.GetTop(item) + dragDeltaVertical);
                            item.Height = item.ActualHeight - dragDeltaVertical;
                            break;
                    }

                    switch (HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            dragDeltaHorizontal = Math.Min(Math.Max(-minLeft, e.HorizontalChange), minDeltaHorizontal);
                            Canvas.SetLeft(item, Canvas.GetLeft(item) + dragDeltaHorizontal);
                            item.Width = item.ActualWidth - dragDeltaHorizontal;
                            break;
                        case HorizontalAlignment.Right:
                            dragDeltaHorizontal = Math.Min(-e.HorizontalChange, minDeltaHorizontal);
                            item.Width = item.ActualWidth - dragDeltaHorizontal;
                            break;
                    }
                }

                e.Handled = true;
            }
		}
	}
}
