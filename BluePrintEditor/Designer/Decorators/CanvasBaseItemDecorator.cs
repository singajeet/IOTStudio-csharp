using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using BluePrintEditor.Designer.Adorners;

namespace BluePrintEditor.Designer.Decorators
{
    public class CanvasBaseItemDecorator : Control
    {
        private Adorner adorner;

        public bool ShowDecorator
        {
            get { return (bool)GetValue(ShowDecoratorProperty); }
            set { SetValue(ShowDecoratorProperty, value); }
        }

        public static readonly DependencyProperty ShowDecoratorProperty =
            DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(CanvasBaseItemDecorator),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ShowDecoratorProperty_Changed)));

        public CanvasBaseItemDecorator()
        {
            Unloaded += new RoutedEventHandler(this.CanvasBaseItemDecorator_Unloaded);
        }

        private void HideAdorner()
        {
            if (this.adorner != null)
            {
                this.adorner.Visibility = Visibility.Hidden;
            }
        }

        private void ShowAdorner()
        {
            if (this.adorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);

                if (adornerLayer != null)
                {
                    ContentControl canvasBaseItem = this.DataContext as ContentControl;
                    Canvas canvas = VisualTreeHelper.GetParent(canvasBaseItem) as Canvas;
                    this.adorner = new ResizeAdorner(canvasBaseItem);
					adornerLayer.Add(this.adorner);

                    if (this.ShowDecorator)
                    {
                        this.adorner.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.adorner.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                this.adorner.Visibility = Visibility.Visible;
            }
        }

        private void CanvasBaseItemDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.adorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.adorner);
                }

                this.adorner = null;
            }
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CanvasBaseItemDecorator decorator = (CanvasBaseItemDecorator)d;
            bool showDecorator = (bool)e.NewValue;

            if (showDecorator)
            {
                decorator.ShowAdorner();
            }
            else
            {
                decorator.HideAdorner();
            }
        }
    }
}
