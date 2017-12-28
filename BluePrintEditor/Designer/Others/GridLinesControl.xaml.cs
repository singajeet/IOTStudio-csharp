/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/26/2017
 * Time: 19:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using BluePrintEditor.Designer.Options;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Designer.Others
{
	/// <summary>
	/// Interaction logic for GridLinesControl.xaml
	/// </summary>
	public partial class GridLinesControl : UserControl
	{
		ILog Logger = Log.Get(typeof(GridLinesControl));
		
		public static readonly DependencyProperty GridColorProperty =
			DependencyProperty.Register("GridColor", typeof(Brush), typeof(GridLinesControl),
				new FrameworkPropertyMetadata(
			                            	Brushes.Red,
			                            	new PropertyChangedCallback(OnGridColorChanged) ));
		
		public Brush GridColor {
			get { return (Brush)GetValue(GridColorProperty); }		
			set {
				SetValue(GridColorProperty, value);
				Logger.PropertyChanged(value);
			}
		}

		private static void OnGridColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((GridLinesControl)d).UpdateGridLinesColor();
			((GridLinesControl)d).Logger.CallbackExecuted();
		}
		
		public static readonly DependencyProperty GridCellSizeProperty =
			DependencyProperty.Register("GridCellSize", typeof(int), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata(20,
			                                                          new PropertyChangedCallback(OnGridCellSizeChanged)
			                                                         ));
		
		public int GridCellSize {
			get { return (int)GetValue(GridCellSizeProperty); }
			set {
				SetValue(GridCellSizeProperty, value);
				Logger.PropertyChanged(value);
			}
		}

		static void OnGridCellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((GridLinesControl)d).UpdateGridData();
			((GridLinesControl)d).Logger.CallbackExecuted();
		}
		
		public static readonly DependencyProperty ShowGridLinesProperty =
			DependencyProperty.Register("ShowGridLines", typeof(bool), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata(true,
			                                                          new PropertyChangedCallback(OnShowGridLinesChanged)
			                                                         ));
		
		public bool ShowGridLines {
			get { return (bool)GetValue(ShowGridLinesProperty); }
			set {
				SetValue(ShowGridLinesProperty, value);
				Logger.PropertyChanged(value);
			}
		}

		static void OnShowGridLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if(((bool)e.NewValue))
				((GridLinesControl)d).ChangeGridVisibility(Visibility.Visible);
			else
				((GridLinesControl)d).ChangeGridVisibility(Visibility.Hidden);
			
			((GridLinesControl)d).Logger.CallbackExecuted();
		}
		
		public static readonly DependencyProperty CellsProperty =
			DependencyProperty.Register("Cells", typeof(ObservableCollection<GridCell>), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata());
		
		public ObservableCollection<GridCell> Cells {
			get { return (ObservableCollection<GridCell>)GetValue(CellsProperty); }
			set {
				SetValue(CellsProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty RowCountProperty =
			DependencyProperty.Register("RowCount", typeof(int), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata());
		
		public int RowCount {
			get { return (int)GetValue(RowCountProperty); }
			set {
				SetValue(RowCountProperty, value);
				Logger.PropertyChanged(value);
			}
		}
		
		public static readonly DependencyProperty ColumnCountProperty =
			DependencyProperty.Register("ColumnCount", typeof(int), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata());
		
		public int ColumnCount {
			get { return (int)GetValue(ColumnCountProperty); }
			set {
				SetValue(ColumnCountProperty, value);
				Logger.PropertyChanged(value);
			}
		}

		public static readonly DependencyProperty GridZIndexProperty =
			DependencyProperty.Register("GridZIndex", typeof(int), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata(1000,
			                                                          new PropertyChangedCallback(OnZIndexChanged)
			                                                         ));
		
		public int GridZIndex {
			get { return (int)GetValue(GridZIndexProperty); }
			set {
				SetValue(GridZIndexProperty, value);
				Logger.PropertyChanged(value);
			}
		}

		static void OnZIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((GridLinesControl)d).UpdateGridZIndex();
			((GridLinesControl)d).Logger.CallbackExecuted();
		}
		
		public static readonly DependencyProperty GridLinesThicknessProperty =
			DependencyProperty.Register("GridLinesThickness", typeof(int), typeof(GridLinesControl),
			                            new FrameworkPropertyMetadata(2,
			                                                          new PropertyChangedCallback(OnGridLinesThicknessChanged)
			                                                         ));
		
		public int GridLinesThickness {
			get { return (int)GetValue(GridLinesThicknessProperty); }
			set {
				SetValue(GridLinesThicknessProperty, value);
				Logger.PropertyChanged(value);
			}
		}

		static void OnGridLinesThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((GridLinesControl)d).UpdateGridLinesThickness();
			((GridLinesControl)d).Logger.CallbackExecuted();
		}
		
		private Line[] HorizontalLines;
		private Line[] VerticalLines;
		private Rectangle GridBorder;
		
		public GridLinesControl()
		{
			Logger.InstanceCreated();
			
			InitializeComponent();	
			this.DataContextChanged+= GridLinesControl_DataContextChanged;
			UpdateBindings();
		}

		void GridLinesControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Logger.DataContextChanged(e);
		}
		
		private void UpdateBindings()
		{
			Binding gridColorBinding = new Binding("GridColor");
			this.SetBinding(GridColorProperty, gridColorBinding);
			
			Binding gridCellSizeBinding = new Binding("GridCellSize");
			this.SetBinding(GridCellSizeProperty, gridCellSizeBinding);
			
			Binding showGridLinesBinding = new Binding("ShowGridLines");
			this.SetBinding(ShowGridLinesProperty, showGridLinesBinding);
			
			Binding cellsBinding = new Binding("Cells");
			this.SetBinding(CellsProperty, cellsBinding);
			
			Binding rowCountBinding = new Binding("RowCount");
			this.SetBinding(RowCountProperty, rowCountBinding);
			
			Binding columnCountBinding = new Binding("ColumnCount");
			this.SetBinding(ColumnCountProperty, columnCountBinding);
			
			Binding gridZIndexBinding = new Binding("GridZIndex");
			this.SetBinding(GridZIndexProperty, gridZIndexBinding);
			
			Binding gridLinesThicknessBinding = new Binding("GridLinesThickness");
			this.SetBinding(GridLinesThicknessProperty, gridLinesThicknessBinding);
			
			Logger.Debug("Bindings have been set on all fields");
		}
		
		private void ClearCanvas()
		{
			if (GridLinesCanvas != null) {
				if (GridLinesCanvas.Children.Count > 1) {
					Logger.DebugF("Canvas has [{0}] children and will be cleared now", GridLinesCanvas.Children.Count);
					GridLinesCanvas.Children.Clear();
					
				}
			}
		}
		
		private void UpdateGridData()
		{
			ClearCanvas();
			
			DateTime startTime = DateTime.Now;
			
			Logger.Debug("==========================Grid Data update has been started==========================");
			
			double GridHeight = Double.IsNaN(Height) ? (Double.IsNaN(ActualHeight) ? RenderSize.Height : ActualHeight) : Height;
			double GridWidth = Double.IsNaN(Width) ? (Double.IsNaN(ActualWidth) ? RenderSize.Width : ActualWidth) : Width;
			
			if (this.GridBorder == null) {
				this.GridBorder = new Rectangle();
			}
				
			this.GridBorder.Stroke = GridColor ?? Brushes.Red;
			this.GridBorder.StrokeThickness = GridLinesThickness;
			this.GridBorder.Height = GridHeight;
			this.GridBorder.Width = GridWidth;
			this.GridBorder.Visibility = (ShowGridLines ? Visibility.Visible : Visibility.Hidden);
			Panel.SetZIndex(GridBorder, GridZIndex);
			
			if(!GridLinesCanvas.Children.Contains(GridBorder))
				GridLinesCanvas.Children.Add(GridBorder);
		
			RowCount = (int)Math.Floor(GridHeight / GridCellSize);
			ColumnCount = (int)Math.Floor(GridWidth / GridCellSize);
			
			if (Cells != null)
				Cells.Clear();
			else
				Cells = new ObservableCollection<GridCell>();
				
			HorizontalLines = new Line[RowCount];
			VerticalLines = new Line[ColumnCount];
			for (int i = 1; i <= ColumnCount; i++) {
				for (int j = 1; j <= RowCount; j++) {
					GridCell cell = new GridCell(i-1, j-1, GridCellSize);
					Cells.Add(cell);
					Line hline = new Line();
					hline.X1 = 0;
					hline.Y1 = j * GridCellSize; 
					hline.X2 = GridWidth;
					hline.Y2 = (j * GridCellSize);			
					
					hline.Stroke = GridColor ?? Brushes.Red;
					hline.StrokeThickness = GridLinesThickness;
					hline.Visibility = (ShowGridLines ? Visibility.Visible : Visibility.Hidden);
					Panel.SetZIndex(hline, GridZIndex);
					
					HorizontalLines[j-1] = hline;
					GridLinesCanvas.Children.Add(hline);
				}
				Line vline = new Line();
				vline.X1 = i * GridCellSize;
				vline.Y1 = 0;
				vline.X2 = (i * GridCellSize);
				vline.Y2 = GridHeight;
				
				vline.Stroke = GridColor ?? Brushes.Red;
				vline.StrokeThickness = GridLinesThickness;
				vline.Visibility = (ShowGridLines ? Visibility.Visible : Visibility.Hidden);
				Panel.SetZIndex(vline, GridZIndex);
				
				VerticalLines[i-1] = vline;
				GridLinesCanvas.Children.Add(vline);
			}		
			
			Logger.Debug("==========================Grid Data update completed!==========================");
			Logger.DebugF("Time taken by update: [{0}]", DateTime.Now.Subtract(startTime).ToString());
		}
		
		private void ChangeGridVisibility(Visibility visibility)
		{
			if(this.GridBorder!=null)
			this.GridBorder.Visibility = visibility;
			
			for (int j = 0; j < RowCount; j++) {
				HorizontalLines[j].Visibility = visibility;				
			}
			
			for (int i = 0; i < ColumnCount; i++) {
				VerticalLines[i].Visibility = visibility;
			}
			
			Logger.DebugF("Grid Visibility Changed => [{0}]", visibility.ToString());
		}
		
		private void UpdateGridLinesColor()
		{
			if (this.GridBorder != null)
				this.GridBorder.Stroke = GridColor ?? Brushes.Red;
			
			for (int j = 0; j < RowCount; j++) {
				HorizontalLines[j].Stroke = GridColor ?? Brushes.Red;
			}
			
			for (int i = 0; i < ColumnCount; i++) {
				VerticalLines[i].Stroke = GridColor ?? Brushes.Red;
			}
			
			Logger.DebugF("GridLines Color Changed => [{0}]", (GridColor ?? Brushes.Red).ToString());
		}
		
		private void UpdateGridLinesThickness()
		{
			if (this.GridBorder != null)
				this.GridBorder.StrokeThickness = GridLinesThickness;
			
			for (int j = 0; j < RowCount; j++) {
				if(HorizontalLines[j]!=null)
					HorizontalLines[j].StrokeThickness = GridLinesThickness;				
			}
			
			for (int i = 0; i < ColumnCount; i++) {
				if(VerticalLines[i]!= null)
					VerticalLines[i].StrokeThickness = GridLinesThickness;
			}
			
			Logger.DebugF("GridLines Thickness Changed => [{0}]", GridLinesThickness);
		}
		
		private void UpdateGridZIndex()
		{
			if(this.GridBorder!=null)
			Panel.SetZIndex(this.GridBorder, GridZIndex);
			
			for (int j = 0; j < RowCount; j++) {
				Panel.SetZIndex(HorizontalLines[j], GridZIndex);
			}
			
			for (int i = 0; i < ColumnCount; i++) {
				Panel.SetZIndex(VerticalLines[i], GridZIndex);
			}
			
			Logger.DebugF("Grid's Z Index Changed => [{0}]", GridZIndex);
		}
		
		void GridLinesLayer_Loaded(object sender, RoutedEventArgs e)
		{	
			Logger.MethodCalled();
			UpdateGridData();
		}
		
		void GridLinesLayer_Unloaded(object sender, RoutedEventArgs e)
		{
			Logger.MethodCalled();
			ClearCanvas();
			Cells.Clear();
		}
		void GridLinesLayer_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Logger.MethodCalled();
			UpdateGridData();
		}		
		
	}
}