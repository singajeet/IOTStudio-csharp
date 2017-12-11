/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 11:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;
using IOTStudio.Core.Elements.Editor;
using Newtonsoft.Json;

namespace IOTStudio.Core.Models.Editor
{
	/// <summary>
	/// Description of ProjectItem.
	/// </summary>
	[DataContract(IsReference = true)]
	public class ProjectItem : BaseProjectItemElement
	{
		public static readonly DependencyProperty
											ProjectItemsProperty = DependencyProperty
																	.Register("ProjectItems", 
			          												typeof(ObservableCollection<ProjectItem>), 
			          												typeof(ProjectItem),
			          												new PropertyMetadata(new ObservableCollection<ProjectItem>()));
		
		
		[DataMember]
		public ObservableCollection<ProjectItem> ProjectItems{
			get { return (ObservableCollection<ProjectItem>)GetValue(ProjectItemsProperty); }
			set { SetValue(ProjectItemsProperty, value); }
		}
		
		public void AddProjectItem(ProjectItem item)
		{
			this.ProjectItems.Add(item);
		}
		
		public void RemoveProjectItem(ProjectItem item)
		{
			this.ProjectItems.Remove(item);
		}
		
		public ProjectItem()
		{
		}
	}
}
