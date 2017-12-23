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

namespace BluePrintEditor.Designer.Actions
{
	/// <summary>
	/// Description of ResizeThumb.
	/// </summary>
	public class ResizeThumb : Thumb
	{
		private ContentControl canvasBaseItem;
		private Canvas canvas;
		private Adorner adorner;
		
		public ResizeThumb()
		{
			DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
			DragStarted += new DragStartedEventHandler(ResizeThumb_DragStarted);
			DragCompleted += new DragCompletedEventHandler(ResizeThumb_DragCompleted);
		}

		void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
		{
			canvasBaseItem = this.DataContext as ContentControl;
			if (canvasBaseItem != null) {
				canvas = VisualTreeHelper.GetParent(canvasBaseItem) as Canvas;
				if (canvas != null) {
					AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.canvas);
					if (adornerLayer != null) {
						adorner = new SizeAdorner(this.canvasBaseItem);
						adornerLayer.Add(adorner);
					}
				}
			}
		}

		void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
		{
			if (this.adorner != null) {
				AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.canvas);
				if (adornerLayer != null) {
					adornerLayer.Remove(adorner);
				}
				this.adorner = null;
			}
		}
		private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
		{
			//Control item = this.DataContext as Control;

			if (canvasBaseItem != null)
			{
				double deltaVertical, deltaHorizontal;

				switch (VerticalAlignment)
				{
					case VerticalAlignment.Bottom:
						deltaVertical = Math.Min(-e.VerticalChange,
						                         canvasBaseItem.ActualHeight - canvasBaseItem.MinHeight);
						canvasBaseItem.Height -= deltaVertical;
						break;
					case VerticalAlignment.Top:
						deltaVertical = Math.Min(e.VerticalChange,
						                         canvasBaseItem.ActualHeight - canvasBaseItem.MinHeight);
						Canvas.SetTop(canvasBaseItem, Canvas.GetTop(canvasBaseItem) + deltaVertical);
						canvasBaseItem.Height -= deltaVertical;
						break;
					default:
						break;
				}

				switch (HorizontalAlignment)
				{
					case HorizontalAlignment.Left:
						deltaHorizontal = Math.Min(e.HorizontalChange,
						                           canvasBaseItem.ActualWidth - canvasBaseItem.MinWidth);
						Canvas.SetLeft(canvasBaseItem, Canvas.GetLeft(canvasBaseItem) + deltaHorizontal);
						canvasBaseItem.Width -= deltaHorizontal;
						break;
					case HorizontalAlignment.Right:
						deltaHorizontal = Math.Min(-e.HorizontalChange,
						                           canvasBaseItem.ActualWidth - canvasBaseItem.MinWidth);
						canvasBaseItem.Width -= deltaHorizontal;
						break;
					default:
						break;
				}
			}

			e.Handled = true;
		}
	}
}
