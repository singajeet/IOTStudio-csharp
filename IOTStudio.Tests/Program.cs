/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 10:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using IOTStudio.Core;
using IOTStudio.Core.Stores;
using IOTStudio.Core.Stores.Logs;
using IOTStudio.Tests.Database.Tests;
using IOTStudio.Tests.Layouts.Tests;
using IOTStudio.Tests.Models.Tests;
using IOTStudio.Tests.Stores.Tests;

namespace IOTStudio.Test
{
	class Program
	{
		[STAThread()]
		public static void Main(string[] args)
		{
//			foreach (Assembly assy in AppDomain.CurrentDomain.GetAssemblies()) {
//				Console.WriteLine("---------------Assembly------------------");
//				//Console.WriteLine(assy.FullName);
//				AssemblyName name = assy.GetName();
//				Console.WriteLine(name.Name);
//				Console.WriteLine(name.CodeBase);
//				Console.WriteLine(name.FullName);
//				Console.WriteLine(name.Version.ToString());
//			}
			
			Logger.Debug("______________________________ Starting BootManager __________________________");
			BootManager boot = BootManager.Instance;
			boot.StartServices();
			Logger.Debug("______________________________ BootManager startup completed __________________________");
			
			Logger.Debug("______________________________ Starting Testing Program __________________________");
			
			Logger.Debug("++++++++++++++++++++++++ Database Test Case +++++++++++++++++++++++++++");
			DatabaseTest dbTest = new DatabaseTest();
			dbTest.TestDatabaseInit();
			dbTest.TestLoadDatabaseAndDriverConnect();
			dbTest.TestDatabaseDeleteQuery();
			dbTest.TestDatabaseInsertQuery();
			dbTest.TestDatabaseFindOneQuery();
			dbTest.TestDatabaseFindCount();
			dbTest.TestDatabaseFindQuery();			
			dbTest.TestDatabaseParentChildInsertQuery();
			dbTest.TestDatabaseParentChildFindOneQuery();
			
			Logger.Debug("++++++++++++++++++++++++ Create Models Test Case +++++++++++++++++++++++++++");
			CreateModelsTest modelsTest = new CreateModelsTest();
			modelsTest.TestCreateModels();
			modelsTest.TestModelsRelationships();
			
			Logger.Debug("++++++++++++++++++++++++ Layouts Store Test Case +++++++++++++++++++++++++++");
			LayoutsStoreTest layoutTest = new LayoutsStoreTest();
			layoutTest.TestLoadLayouts();
			layoutTest.TestInstanceOfLayout();
			layoutTest.TestSelectLayout();
			
			Logger.Debug("++++++++++++++++++++++++ Flags Store Test Case +++++++++++++++++++++++++++");
			FlagsStoreTest flagTest = new FlagsStoreTest();
			flagTest.TestFlagProviderInitialize();
			flagTest.TestKeyRegistration();	
			flagTest.TestKeyUnregister();
			
			Logger.Debug("______________________________ End Testing Program __________________________");
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}