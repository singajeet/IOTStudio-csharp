/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/30/2017
 * Time: 13:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BluePrintEditor.Utilities;
using log4net;
using MahApps.Metro.IconPacks;

namespace BluePrintEditor.Designer.ToolBox
{
	/// <summary>
	/// Interaction logic for ToolBoxItemControl.xaml
	/// </summary>
	public partial class ToolBoxItemControl : UserControl
	{
		ILog Logger = Log.Get(typeof(ToolBoxItemControl));
		
		public event EventHandler SelectionChanged;	
		
		private bool _IsSelecting = false;
		
		public static readonly DependencyProperty IdProperty =
			DependencyProperty.Register("Id", typeof(Guid), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata());
		
		public Guid Id {
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		
		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected", typeof(bool), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata(false));
		
		public bool IsSelected {
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}
		
		public static readonly DependencyProperty IconUriProperty =
			DependencyProperty.Register("IconUri", typeof(string), typeof(ToolBoxControl),
			                            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIconUriChanged)));
		
		public string IconUri {
			get { return (string)GetValue(IconUriProperty); }
			set { SetValue(IconUriProperty, value); }
		}

		static void OnIconUriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ToolBoxItemControl ctrl = (ToolBoxItemControl)d;
			
			ctrl.Logger.MethodCalled();
			ctrl.Logger.PropertyValue("IconUri", ctrl.IconUri);
			ctrl.Logger.PropertyValue("IconKind", ctrl.IconKind);
			
			if (!string.IsNullOrEmpty(ctrl.IconUri)) {
				ctrl.Logger.Debug("IconUri contains value; PackIcon will be collapsed & ImageIcon will be shown");
				
				PackIconModern packIcon = (PackIconModern)ctrl.FindName("PackIcon");
				if (packIcon != null) {					
					packIcon.Visibility = Visibility.Collapsed;
					ctrl.Logger.Debug("PackIcon collapsed");
				}
				
				Image icon = (Image)ctrl.FindName("ImageIcon");
				if (icon != null) {
					icon.Visibility = Visibility.Visible;
					ctrl.Logger.Debug("ImageIcon shown");
				}
			} else {
				ctrl.Logger.Debug("IconUri is empty; ImageIcon will be collapsed & PackIcon will be shown");
				
				PackIconModern packIcon = (PackIconModern)ctrl.FindName("PackIcon");
				if (packIcon != null) {
					packIcon.Visibility = Visibility.Visible;
					ctrl.Logger.Debug("PackIcon shown");
				}
				
				Image icon = (Image)ctrl.FindName("ImageIcon");
				if (icon != null) {
					icon.Visibility = Visibility.Collapsed;
					ctrl.Logger.Debug("ImageIcon collapsed");
				}
			}
		}
		
		public static readonly DependencyProperty IconKindProperty =
			DependencyProperty.Register("IconKind", typeof(PackIconModernKind), typeof(ToolBoxControl),
			                            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIconKindChanged)));
		
		public PackIconModernKind IconKind {
			get { return (PackIconModernKind)GetValue(IconKindProperty); }
			set { SetValue(IconKindProperty, value); }
		}

		static void OnIconKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ToolBoxItemControl ctrl = (ToolBoxItemControl)d;
			ctrl.Logger.MethodCalled();
			ctrl.Logger.PropertyValue("IconUri", ctrl.IconUri);
			ctrl.Logger.PropertyValue("IconKind", ctrl.IconKind);
			
			if (string.IsNullOrEmpty(ctrl.IconUri) && ctrl.IconKind != null) {
				ctrl.Logger.Debug("IconUri is empty & IconKind is not null");
				
				PackIconModern packIcon = (PackIconModern)ctrl.FindName("PackIcon");
				if (packIcon != null) {
					packIcon.Visibility = Visibility.Visible;
					ctrl.Logger.Debug("PackIcon shown");
				}
				
				Image icon = (Image)ctrl.FindName("ImageIcon");
				if (icon != null) {
					icon.Visibility = Visibility.Collapsed;
					ctrl.Logger.Debug("ImageIcon shown");
				}
			} else {
				ctrl.Logger.Debug("Either IconUri is not null or IconKind is null");
			}
		}
		
		public ToolBoxItemControl()
		{
			Logger.InstanceCreated();
			
			InitializeComponent();
			
			this.Id = Guid.NewGuid();
			
			ToolBoxHelper.Instance.RegisterItem(this);
			
			MouseDown += ToolBoxItemControl_MouseDown;
			MouseUp += ToolBoxItemControl_MouseUp;
		}

		void ToolBoxItemControl_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Logger.MethodCalled();
			((UIElement)e.Source).CaptureMouse();
			
			if (!_IsSelecting && ((UIElement)e.Source).IsMouseCaptured) {
				_IsSelecting = true;
			}
		}

		void ToolBoxItemControl_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Logger.MethodCalled();
			
			if (_IsSelecting && ((UIElement)e.Source).IsMouseCaptureWithin) {
				ToolBoxHelper.Instance.SelectItem(this);
				_IsSelecting = false;	
				
				if (SelectionChanged != null)
					SelectionChanged(this, new EventArgs());
			}			
			
			((UIElement)e.Source).ReleaseMouseCapture();
		}
		
		public override string ToString()
		{
			return string.Format("[ToolBoxItemControl Id={0}, IsSelected={1}]", Id, IsSelected);
		}

	}
}