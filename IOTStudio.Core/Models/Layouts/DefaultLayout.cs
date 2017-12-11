/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 8:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using IOTStudio.Core.Elements.GUI;
using IOTStudio.Core.Providers.Properties;
using IOTStudio.Core.Serializers;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace IOTStudio.Core.Models.Layouts
{
	/// <summary>
	/// Description of DefaultLayout.
	/// </summary>
	[DataContract(IsReference=true)]
	public class DefaultLayout: BaseLayoutElement
	{
		//private DockingManager dockingManager = new DockingManager();
		private RowDefinition menuRow = new RowDefinition();
		private RowDefinition toolbarRow = new RowDefinition();
		private RowDefinition contentRow = new RowDefinition();
		private RowDefinition statusbarRow = new RowDefinition();
		
		public static readonly DependencyProperty WindowsDockingManagerProperty =
			DependencyProperty.Register("WindowsDockingManager", typeof(DockingManager), typeof(DefaultLayout),
			                            new FrameworkPropertyMetadata());
		
		public DockingManager WindowsDockingManager {
			get { return (DockingManager)GetValue(WindowsDockingManagerProperty); }
			set { SetValue(WindowsDockingManagerProperty, value); }
		}
		
		public static readonly DependencyProperty RootProperty =
			DependencyProperty.Register("Root", typeof(Grid), typeof(DefaultLayout),
			                            new FrameworkPropertyMetadata());
		[DataMember]
		public Grid Root {
			get { return (Grid)GetValue(RootProperty); }
			set { SetValue(RootProperty, value); }
		}
		
		public static readonly DependencyProperty MenuSectionProperty =
			DependencyProperty.Register("MenuSection", typeof(DockPanel), typeof(DefaultLayout),
			                            new FrameworkPropertyMetadata());
		[DataMember]
		public DockPanel MenuSection {
			get { return (DockPanel)GetValue(MenuSectionProperty); }
			set { SetValue(MenuSectionProperty, value); }
		}
		
		public static readonly DependencyProperty ToolBarSectionProperty =
			DependencyProperty.Register("ToolBarSection", typeof(DockPanel), typeof(DefaultLayout),
			                            new FrameworkPropertyMetadata());
		[DataMember]
		public DockPanel ToolBarSection {
			get { return (DockPanel)GetValue(ToolBarSectionProperty); }
			set { SetValue(ToolBarSectionProperty, value); }
		}
		
		public static readonly DependencyProperty ContentSectionProperty =
			DependencyProperty.Register("ContentSection", typeof(StackPanel), typeof(DefaultLayout),
			                            new FrameworkPropertyMetadata());
		[DataMember]
		public StackPanel ContentSection {
			get { return (StackPanel)GetValue(ContentSectionProperty); }
			set { SetValue(ContentSectionProperty, value); }
		}
		
		public static readonly DependencyProperty StatusBarSectionProperty =
			DependencyProperty.Register("StatusBarSection", typeof(StackPanel), typeof(DefaultLayout),
			                            new FrameworkPropertyMetadata());
		[DataMember]
		public StackPanel StatusBarSection {
			get { return (StackPanel)GetValue(StatusBarSectionProperty); }
			set { SetValue(StatusBarSectionProperty, value); }
		}
				
		[DataMember]
		public int MenuSectionHeight { get; set; }
		
		[DataMember]
		public int ToolBarSectionHeight { get; set; }
		
		[DataMember]
		public int StatusBarSectionHeight { get; set; }
		
		public DefaultLayout()
		{
			AssignDefaultValues();
			SetupLayout();
		}

		void AssignDefaultValues()
		{
			WindowsDockingManager = WindowsDockingManager ?? new DockingManager();
			Root = Root ?? new Grid();
			MenuSection = MenuSection ?? new DockPanel();
			ToolBarSection = ToolBarSection ?? new DockPanel();
			ContentSection = ContentSection ?? new StackPanel();
			StatusBarSection = StatusBarSection ?? new StackPanel();
		}
		void SetupLayout()
		{
			MenuSectionHeight = 28;
			ToolBarSectionHeight = 28;
			StatusBarSectionHeight = 28;
			
			Root.HorizontalAlignment = HorizontalAlignment.Stretch;
			Root.VerticalAlignment = VerticalAlignment.Stretch;
			
			menuRow.Height = new GridLength(MenuSectionHeight);
			toolbarRow.Height = new GridLength(ToolBarSectionHeight);
			
			GridLength contentHeight = new GridLength(0, GridUnitType.Star);			
			contentRow.Height = contentHeight;
			
			statusbarRow.Height = new GridLength(StatusBarSectionHeight);
			
			Root.RowDefinitions.Add(menuRow);
			Root.RowDefinitions.Add(toolbarRow);
			Root.RowDefinitions.Add(contentRow);
			Root.RowDefinitions.Add(statusbarRow);
			
			MenuSection.HorizontalAlignment = HorizontalAlignment.Stretch;
			MenuSection.VerticalAlignment = VerticalAlignment.Stretch;
			
			Root.Children.Add(MenuSection);
			Grid.SetRow(MenuSection, 0);
			
			Root.Children.Add(ToolBarSection);
			Grid.SetRow(ToolBarSection, 1);
			
			Root.Children.Add(ContentSection);
			Grid.SetRow(ContentSection, 2);
			
			Root.Children.Add(StatusBarSection);
			Grid.SetRow(StatusBarSection, 3);
			
			ContentSection.Children.Add(WindowsDockingManager);
		}
		
		public static void SaveLayout(DefaultLayout layout)
		{
			string layoutSavePath = PropertyProvider.Layout.GetProperty("SelectedLayoutSavePath") as String;
			NewtonsoftJSONSerializer.Serialize(layout, layoutSavePath + @"\DefaultLayout.json");
			
			XmlLayoutSerializer dockManagerSerializer = new XmlLayoutSerializer(layout.WindowsDockingManager);
			dockManagerSerializer.Serialize(layoutSavePath + @"\DockWindows.json");
		}
		
		public static DefaultLayout LoadLayout()
		{
			string layoutSavePath = PropertyProvider.Layout.GetProperty("SelectedLayoutSavePath") as String;
			DefaultLayout layout = NewtonsoftJSONSerializer.Deserialize(layoutSavePath + @"\DefaultLayout.json") as DefaultLayout;
			
			if (layout.WindowsDockingManager == null)
				layout.WindowsDockingManager = new DockingManager();
			XmlLayoutSerializer dockManagerSerializer = new XmlLayoutSerializer(layout.WindowsDockingManager);
			dockManagerSerializer.Deserialize(layoutSavePath + @"\DockWindows.json");
			
			return layout;
		}
	}
}
