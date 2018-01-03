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
using System.Windows.Data;
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
		private Point? dragStartPoint = null;
		
		public event EventHandler SelectionChanged;	
		
		private bool _IsSelecting = false;
		
		public static readonly DependencyProperty IdProperty =
			DependencyProperty.Register("Id", typeof(Guid), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata());
		
		public Guid Id {
			get { return (Guid)GetValue(IdProperty); }
			set { SetValue(IdProperty, value); }
		}
		
		public static readonly DependencyProperty ItemIdProperty =
			DependencyProperty.Register("ItemId", typeof(Guid), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata());
		
		public Guid ItemId {
			get { return (Guid)GetValue(ItemIdProperty); }
			set { SetValue(ItemIdProperty, value); }
		}
		
		public static readonly DependencyProperty NameProperty =
			DependencyProperty.Register("Name", typeof(string), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata());
		
		public string Name {
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}
		
		public static readonly DependencyProperty ToolTitleProperty =
			DependencyProperty.Register("ToolTitle", typeof(string), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata());
		
		public string ToolTitle {
			get { return (string)GetValue(ToolTitleProperty); }
			set { SetValue(ToolTitleProperty, value); }
		}
		
		public static readonly DependencyProperty ToolCommandTypeProperty =
			DependencyProperty.Register("ToolCommandType", typeof(Type), typeof(ToolBoxItemControl),
			                            new FrameworkPropertyMetadata());
		
		public Type ToolCommandType {
			get { return (Type)GetValue(ToolCommandTypeProperty); }
			set { SetValue(ToolCommandTypeProperty, value); }
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
			
			this.ItemId = Guid.NewGuid();
			
			UpdateBindings();
			
			ToolBoxHelper.Instance.RegisterItem(this);
			
			MouseDown += ToolBoxItemControl_MouseDown;
			MouseUp += ToolBoxItemControl_MouseUp;
			PreviewMouseDown+= ToolBoxItemControl_PreviewMouseDown;
			MouseMove += ToolBoxItemControl_MouseMove;
		}

		void UpdateBindings()
		{
			Binding idBinding = new Binding("Id");
			this.SetBinding(IdProperty, idBinding);
			Binding iconUriBinding = new Binding("IconUri");
			this.SetBinding(IconUriProperty, iconUriBinding);
			Binding iconKindBinding = new Binding("IconKind");
			this.SetBinding(IconKindProperty, iconKindBinding);
			Binding nameBinding = new Binding("Name");
			this.SetBinding(NameProperty, nameBinding);
			Binding toolTitleBinding = new Binding("ToolTitle");
			this.SetBinding(ToolTitleProperty, toolTitleBinding);
			Binding toolCommandTypeBinding = new Binding("ToolCommandType");
			this.SetBinding(ToolCommandTypeProperty, toolCommandTypeBinding);
		}
		
		void ToolBoxItemControl_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton != MouseButtonState.Pressed) {
				this.dragStartPoint = null;
			}
			
			if (this.dragStartPoint.HasValue) {
			
				Point position = e.GetPosition(this);
				if ((SystemParameters.MinimumHorizontalDragDistance <= Math.Abs((double)(position.X - this.dragStartPoint.Value.X))) ||
				   (SystemParameters.MinimumVerticalDragDistance <=
				   Math.Abs((double)(position.Y - this.dragStartPoint.Value.Y)))) {
					
					DataObject dataObject = new DataObject("ITEM", "");
					if (dataObject != null) {
						DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy);
					}
				}
				e.Handled = true;
			}
		}
		
		void ToolBoxItemControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.dragStartPoint = new Point?(e.GetPosition(this));
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
			return string.Format("[ToolBoxItemControl Id={0}, IsSelected={1}]", ItemId, IsSelected);
		}

	}
}