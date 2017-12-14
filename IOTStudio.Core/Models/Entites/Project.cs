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
using IOTStudio.Core.Elements.Editor;
using IOTStudio.Core.Providers;
using IOTStudio.Core.Providers.Logging;

namespace IOTStudio.Core.Models.Entites
{
	/// <summary>
	/// Description of Project.
	/// </summary>
	[DataContract]
	public class Project : BaseProjectElement
	{
		
		private ObservableCollection<ProjectItem> projectItems; 
		
		[DataMember]
		public ObservableCollection<ProjectItem> ProjectItems{
			get { return projectItems; }
			set { projectItems= value; 
				OnPropertyChanged();
			}
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
			Id = Guid.NewGuid();
			Name = Get.i.NameProvider.GetName("Project");
			
			ProjectItems = ProjectItems ?? new ObservableCollection<ProjectItem>();
			Logger.Info("Project instance has been created");
		}
	}
}
