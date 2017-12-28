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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
//			CanvasViewModel vm = CanvasViewModel.Instance;	
//			vm.GridCellSize = 10;
//			vm.GridColor = new ColorMenuData(){SolidBrush=new SolidColorBrush(Colors.Red)};
//			vm.GridLinesThickness = 1;
//			vm.ShowGridLines = true;
//			
//			this.DataContext = vm;		
			this.DataContextChanged+= OptionsControl_DataContextChanged;			
		}

		void OptionsControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Logger.DataContextChanged(e);
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
	}
}