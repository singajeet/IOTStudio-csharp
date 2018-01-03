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
using System.Windows.Media;
using BluePrintEditor.Designer.ToolBox;
using BluePrintEditor.Designer.ToolBox.Interfaces;
using BluePrintEditor.Utilities;
using log4net;
using MahApps.Metro;
using System.Linq;

namespace BluePrintEditor.Designer.Options
{
	/// <summary>
	/// Description of OptionsControlViewModel.
	/// </summary>
	public sealed class CanvasViewModel : INotifyPropertyChanged
	{
		private static CanvasViewModel instance = new CanvasViewModel();
		ILog Logger = Log.Get(typeof(CanvasViewModel));
		
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
					OnPropertyChanged();
			}
		}
		
		public static CanvasViewModel Instance {
			get {
				return instance;
			}
		}
		
		private CanvasViewModel()
		{
			Logger.InstanceCreated();
			Id = Guid.NewGuid();
			
			this.GridColorsSource = new ObservableCollection<Brush>( 
			                                        ThemeManager.Accents
													.Select(a => a.Resources["AccentColorBrush"] as Brush)
													.ToList());
			
			Logger.CollectionCreated(GridColorsSource);				
			
			this.GridCellSizesSource = new ObservableCollection<int>();
			string cellSizes = Config.GetGridSetting("GridCellSizes");
			Logger.PropertyValue("GridCellSizes", cellSizes);
			
			foreach (string cellSize in cellSizes.Split(',')) {
				GridCellSizesSource.Add(int.Parse(cellSize));
			}
			
			Logger.CollectionCreated(GridCellSizesSource);		
			
			ObservableCollection<IToolItem> items = new ObservableCollection<IToolItem>(ToolBoxItemLoader
			                                                                            .Instance
			                                                                            .ToolBoxItems);
			
			
			ObservableCollection<string> sections = ToolBoxItemLoader.Instance.Sections;
			
			this.ToolBoxSections = new ToolBoxSections();
			foreach (string section in sections) {
				if (!string.IsNullOrEmpty(section)) {
					var sectionObject = new ToolBoxSection();
					sectionObject.SectionName = section;
					Logger.PropertyValue("SectionName", section);
				
					var categorizedItems = items.Where((i => i.Category != null && i.Category.Equals(section))).ToList();
					foreach (IToolItem item in categorizedItems) {
						if (!item.GetType().Name.Equals("BaseToolBoxItem"))
							sectionObject.ToolBoxItems.Add(item);
					}
				
					this.ToolBoxSections.Add(sectionObject);
				}
			}
		}
		
		ToolBoxSections toolBoxSections;
		
		public ToolBoxSections ToolBoxSections {
			get {return toolBoxSections; 
			}
			set { 
					toolBoxSections = value; 
					OnPropertyChanged();
				Logger.PropertyChanged(value);
			}
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
		
		double gridOpacity;
		
		public double GridOpacity {
			get { return gridOpacity; }
			set { 
					gridOpacity = value; 
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
		
		public override string ToString()
		{
			return string.Format("[CanvasViewModel Id={0}]", id);
		}

	}
}
