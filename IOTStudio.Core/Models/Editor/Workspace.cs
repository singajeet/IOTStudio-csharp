﻿/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/9/2017
 * Time: 11:48 PM
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
	/// Description of Workspace.
	/// </summary>
	[DataContract]//(MemberSerialization = MemberSerialization.OptIn)]
	public class Workspace : BaseWorkspaceElement
	{
		private ObservableCollection<Project> projects;
		
		
		[DataMember]
		public ObservableCollection<Project> Projects{
			get { return projects; }
			set { projects= value; 
				OnPropertyChanged();
			}
		}
		
		public void AddProject(Project project)
		{
			Logger.Debug("New Project [{0}] has been added to Workspace [{1}]", project.Name, this.Name);
			this.Projects.Add(project);
		}
		
		public void RemoveProject(Project project)
		{
			this.Projects.Remove(project);
			Logger.Debug("Project [{0}] will be removed from Workspace [{1}]", project.Name, this.Name);
		}
		
		public Workspace()
		{
			Projects = Projects ?? new ObservableCollection<Project>();
			Logger.Debug("Instance created successfully!");
		}
	}
}
