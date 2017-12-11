/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 10:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Models.Editor;
using IOTStudio.Core.Models.Layouts;
using IOTStudio.Core.Serializers;

namespace IOTStudio.Test
{
	class Program
	{
		[STAThread()]
		public static void Main(string[] args)
		{
			Workspace wks = new Workspace();
			wks.Name = "MyWorkspace";
			wks.Id = Guid.NewGuid();
			
			Project prj = new Project();
			prj.Name = "MyFirstProject";
			prj.Id = Guid.NewGuid();
			
			wks.AddProject(prj);		
			
			ProjectItem srcFolder = new ProjectItem();
			srcFolder.Name = "Src Folder";
			srcFolder.Id = Guid.NewGuid();
			
			prj.AddProjectItem(srcFolder);
			
			ProjectItem cfile = new ProjectItem();
			cfile.Name = "Program.cs";
			cfile.Id = Guid.NewGuid();
			
			srcFolder.AddProjectItem(cfile);
			
			LayoutSelector layoutSelector = new LayoutSelector();
			
			NewtonsoftJSONSerializer.Serialize(wks, "Workspace.json");
			NewtonsoftJSONSerializer.Serialize(layoutSelector, "DefaultLayout.json");
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}