/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 12:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using IOTStudio.Core.Models.Entites;
using NUnit.Framework;

namespace IOTStudio.Tests.Models.Tests
{
	/// <summary>
	/// Description of CreateModelsTest.
	/// </summary>
	[TestFixture]
	[Apartment(ApartmentState.STA)]
	public class CreateModelsTest
	{
		Workspace wks;
		Project prj;
		ProjectItem srcFolder;
		ProjectItem cfile;
		
		public CreateModelsTest()
		{
		}
		
		[Test(Description="Create basic models")]
		public void TestCreateModels()
		{
			wks = new Workspace();
			wks.Name = "MyWorkspace";
			wks.Id = Guid.NewGuid();
			Assert.AreEqual("MyWorkspace", wks.Name);
			
			prj = new Project();
			prj.Name = "MyFirstProject";
			prj.Id = Guid.NewGuid();
			Assert.AreEqual("MyFirstProject", prj.Name);
			
			wks.AddProject(prj);		
			
			srcFolder = new ProjectItem();
			srcFolder.Name = "Src Folder";
			srcFolder.Id = Guid.NewGuid();
			Assert.AreEqual("Src Folder", srcFolder.Name);
			
			prj.AddProjectItem(srcFolder);
			
			cfile = new ProjectItem();
			cfile.Name = "Program.cs";
			cfile.Id = Guid.NewGuid();
			Assert.AreEqual("Program.cs", cfile.Name);
			
			srcFolder.AddProjectItem(cfile);
		}
		
		[Test(Description = "Check relationships between models")]
		public void TestModelsRelationships()
		{
			Assert.AreEqual(1, wks.Projects.Count);
			Assert.AreEqual(1, prj.ProjectItems.Count);
			Assert.AreEqual(1, srcFolder.ProjectItems.Count);
			Assert.AreEqual(0, cfile.ProjectItems.Count);
		}
	}
}
