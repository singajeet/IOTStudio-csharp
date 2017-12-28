/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/27/2017
 * Time: 12:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using BluePrintEditor.Utilities;
using log4net;
using MahApps.Metro;
using System.Linq;
using MahApps.Metro.Controls;

namespace BluePrintEditor.Designer.Options
{
	/// <summary>
	/// Description of OptionsControlViewModel.
	/// </summary>
	public sealed class CanvasViewModel : INotifyPropertyChanged
	{
		private static CanvasViewModel instance = new CanvasViewModel();
		ILog Logger = Log.Get(typeof(CanvasViewModel));
		
		public static CanvasViewModel Instance {
			get {
				return instance;
			}
		}
		
		private CanvasViewModel()
		{
			Logger.InstanceCreated();
			
			this.GridColorsSource = new ObservableCollection<Brush>( 
			                                        ThemeManager.Accents
													.Select(a => a.Resources["AccentColorBrush"] as Brush)
													.ToList());
			
			Logger.CollectionCreated(GridColorsSource);				
			
			this.GridCellSizesSource = new ObservableCollection<int>();
			this.GridCellSizesSource.Add(5);
			this.GridCellSizesSource.Add(10);
			this.GridCellSizesSource.Add(15);
			this.GridCellSizesSource.Add(20);
			this.GridCellSizesSource.Add(25);
			
			Logger.CollectionCreated(GridCellSizesSource);								
		}
		
		int mouseY;
		
		public int MouseY {
			get { return mouseY; }
			set { 
					mouseY = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		int mouseX;
		
		public int MouseX {
			get { return mouseX; }
			set { 
					mouseX = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		int gridZIndex;
		
		public int GridZIndex {
			get { return gridZIndex; }
			set { 
					gridZIndex = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		int columnCount;
		
		public int ColumnCount {
			get { return columnCount; }
			set { 
					columnCount = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		int rowCount;
		
		public int RowCount {
			get { return rowCount; }
			set { 
					rowCount = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		Brush gridColor;
		
		public Brush GridColor {
			get { return gridColor; }
			set { 
					gridColor = value; 
					OnPropertyChanged();					
				Logger.PropertyChanged(value);
			}
		}
		
		bool showGridLines;
		
		public bool ShowGridLines {
			get { return showGridLines; }
			set { 
					showGridLines = value; 
					OnPropertyChanged();				
				Logger.PropertyChanged(value);
			}
		}
		
		int gridLinesThickness;
		
		public int GridLinesThickness {
			get { return gridLinesThickness; }
			set { 
					gridLinesThickness = value; 
					OnPropertyChanged();				
				Logger.PropertyChanged(value);
			}
		}
		
		int gridCellSize;
		
		public int GridCellSize {
			get { return gridCellSize; }
			set { 
					gridCellSize = value; 
					OnPropertyChanged();				
				Logger.PropertyChanged(value);
			}
		}
		
		ObservableCollection<int> gridCellSizesSource;
		
		public ObservableCollection<int> GridCellSizesSource {
			get { return gridCellSizesSource; }
			set { 
					gridCellSizesSource = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}
		
		ObservableCollection<Brush> gridColorsSource;
		
		public ObservableCollection<Brush> GridColorsSource {
			get { return gridColorsSource; }
			set { 
					gridColorsSource = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;
		
		protected void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
		}
		
		#endregion
	}
}
