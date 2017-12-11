/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 11:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows;
using IOTStudio.Core.Elements;
using IOTStudio.Core.Elements.Editor;
using IOTStudio.Core.Providers.Logging;
using Newtonsoft.Json;

namespace IOTStudio.Core.Models.Editor
{
	/// <summary>
	/// Description of Project.
	/// </summary>
	[DataContract]
	public class Project : BaseProjectElement
	{
		
		public static readonly DependencyProperty
											ProjectItemsProperty = DependencyProperty
																	.Register("ProjectItems", 
			          												typeof(ObservableCollection<ProjectItem>), 
			          												typeof(BaseElement),
			          												new PropertyMetadata(new ObservableCollection<ProjectItem>()));
		
		
		[DataMember]
		public ObservableCollection<ProjectItem> ProjectItems{
			get { return (ObservableCollection<ProjectItem>)GetValue(ProjectItemsProperty); }
			set { SetValue(ProjectItemsProperty, value); }
		}
		
		public void AddProjectItem(ProjectItem item)
		{
			this.ProjectItems.Add(item);
			Logger.Debug("New ProjectItem [{0}] has been added to Project [{1}]", item.Name, this.Name);
		}
		
		public void RemoveProjectItem(ProjectItem item)
		{
			Logger.Debug("ProjectItem [{0}] will be removed from Project [{1}]", item.Name, this.Name);
			this.ProjectItems.Remove(item);
		}
		
		public Project()
		{
			Logger.Info("Project instance has been created");
		}
	}
}
