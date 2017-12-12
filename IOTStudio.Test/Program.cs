/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 10:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using IOTStudio.Core.Elements.Interfaces;
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
			
			//================ JSON Serialization tests =====================
			Console.WriteLine("=================TEST: JSON Serialization Started=================");
			Workspace wks = new Workspace();
			wks.Name = "MyWorkspace";
			wks.Id = Guid.NewGuid();
			
			Project prj = new Project();
			prj.Name = "MyFirstProject";
			prj.Id = Guid.NewGuid();
			
			Project prj2 = new Project();
			prj.Name = "MySecondProject";
			prj.Id = Guid.NewGuid();
			
			wks.AddProject(prj);
			wks.AddProject(prj2);
			
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
			
			Console.WriteLine("=================TEST: JSON Serialization Completed=================");
			
			//================ LayoutSelector test ========================
			
			Console.WriteLine("=================TEST: LayoutSelector Started=================");
			
			foreach (ILayoutElement layout in layoutSelector.Layouts) {
				Console.WriteLine("Layout: {0}", layout.ToString());
			}
			
			Console.WriteLine("=================TEST: LayoutSelector Completed=================");
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}