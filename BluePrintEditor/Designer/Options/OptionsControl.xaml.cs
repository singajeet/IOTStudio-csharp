/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 18:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using BluePrintEditor.Designer.Others;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Designer.Options
{
	/// <summary>
	/// Interaction logic for OptionsControl.xaml
	/// </summary>
	public partial class OptionsControl : UserControl
	{
		ILog Logger = Log.Get(typeof(OptionsControl));
		
		public OptionsControl()
		{
			Logger.InstanceCreated();
			
			InitializeComponent();
			this.DataContextChanged+= OptionsControl_DataContextChanged;
			this.Unloaded+= OptionsControl_Unloaded;
			this.Loaded+= OptionsControl_Loaded;
		}

		Task mousePosReporter;
		
		void OptionsControl_Loaded(object sender, RoutedEventArgs e)
		{
			mousePosReporter = Task.Run(async () => {
			                            	while (true) {
			                            		if (breakMouseUpdateLoop) {
			                            			Logger.PropertyValue("breakMouseUpdateLoop", breakMouseUpdateLoop);
			                            			break;
			                            		}
			                            		UpdateMousePosition();
			                            		await Task.Delay(100);
			                            	}
			                            });
		}
		void OptionsControl_Unloaded(object sender, RoutedEventArgs e)
		{
			breakMouseUpdateLoop = true;
			Task.Delay(100);
			Logger.DebugF("BreakMouseUpdateLoop flagged for completion; Current Task Status => [{0}]", mousePosReporter.Status);
		}
		
		void OptionsControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Logger.DataContextChanged(e);
		}
		
		bool breakMouseUpdateLoop = false;
		
		void UpdateMousePosition()
		{
			Dispatcher.Invoke(() => {
			                  	Point mousePosition = this.PointToScreen(Mouse.GetPosition(this));
			                  	XPos.Text = mousePosition.X.ToString();
			                  	YPos.Text = mousePosition.Y.ToString();
			                  });
		}
		
		public static readonly DependencyProperty ShowGridLinesProperty =
			DependencyProperty.Register("ShowGridLines", typeof(bool), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public bool ShowGridLines {
			get { return (bool)GetValue(ShowGridLinesProperty); }
			set {
				SetValue(ShowGridLinesProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty GridLinesThicknessProperty =
			DependencyProperty.Register("GridLinesThickness", typeof(int), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public int GridLinesThickness {
			get { return (int)GetValue(GridLinesThicknessProperty); }
			set {
				SetValue(GridLinesThicknessProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty GridCellSizeProperty =
			DependencyProperty.Register("GridCellSize", typeof(int), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public int GridCellSize {
			get { return (int)GetValue(GridCellSizeProperty); }
			set {
				SetValue(GridCellSizeProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty GridCellSizesSourceProperty =
			DependencyProperty.Register("GridCellSizesSource", typeof(ObservableCollection<int>), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public ObservableCollection<int> GridCellSizesSource {
			get { return (ObservableCollection<int>)GetValue(GridCellSizesSourceProperty); }
			set {
				SetValue(GridCellSizesSourceProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty GridColorProperty =
			DependencyProperty.Register("GridColor", typeof(Brush), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public Brush GridColor {
			get { return (Brush)GetValue(GridColorProperty); }
			set {
				SetValue(GridColorProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty GridColorsSourceProperty =
			DependencyProperty.Register("GridColorsSource", typeof(ObservableCollection<Brush>), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public ObservableCollection<Brush> GridColorsSource {
			get { return (ObservableCollection<Brush>)GetValue(GridColorsSourceProperty); }
			set {
				SetValue(GridColorsSourceProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty GridOpacityProperty =
			DependencyProperty.Register("GridOpacity", typeof(double), typeof(OptionsControl),
			                            new FrameworkPropertyMetadata());
		
		public double GridOpacity {
			get { return (double)GetValue(GridOpacityProperty); }
			set { SetValue(GridOpacityProperty, value); }
		}
	}
}