/*
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
using Newtonsoft.Json;

namespace IOTStudio.Core.Models.Editor
{
	/// <summary>
	/// Description of Workspace.
	/// </summary>
	[DataContract]//(MemberSerialization = MemberSerialization.OptIn)]
	public class Workspace : BaseWorkspaceElement
	{
		public static readonly DependencyProperty
											ProjectsProperty = DependencyProperty
																	.Register("Projects", 
			          												typeof(ObservableCollection<Project>), 
			          												typeof(BaseElement),
			          												new PropertyMetadata(new ObservableCollection<Project>()));
		
		
		[DataMember]
		public ObservableCollection<Project> Projects{
			get { return (ObservableCollection<Project>)GetValue(ProjectsProperty); }
			set { SetValue(ProjectsProperty, value); }
		}
		
		public void AddProject(Project project)
		{
			this.Projects.Add(project);
		}
		
		public void RemoveProject(Project project)
		{
			this.Projects.Remove(project);
		}
		
		public Workspace()
		{
		}
	}
}
