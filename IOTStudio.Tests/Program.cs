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
using IOTStudio.Core.Providers.Logging;
using IOTStudio.Tests.Layouts.Tests;
using IOTStudio.Tests.Models.Tests;
using IOTStudio.Tests.Providers.Tests;

namespace IOTStudio.Test
{
	class Program
	{
		[STAThread()]
		public static void Main(string[] args)
		{
			foreach (Assembly assy in AppDomain.CurrentDomain.GetAssemblies()) {
				Console.WriteLine("---------------Assembly------------------");
				//Console.WriteLine(assy.FullName);
				AssemblyName name = assy.GetName();
				Console.WriteLine(name.Name);
				Console.WriteLine(name.CodeBase);
				Console.WriteLine(name.FullName);
				Console.WriteLine(name.Version.ToString());
			}
//			Logger.Debug("______________________________ Starting Testing Program __________________________");
//			
//			Logger.Debug("++++++++++++++++++++++++ Create Models Test Case +++++++++++++++++++++++++++");
//			CreateModelsTest modelsTest = new CreateModelsTest();
//			modelsTest.TestCreateModels();
//			modelsTest.TestModelsRelationships();
//			
//			Logger.Debug("++++++++++++++++++++++++ Load Layouts Test Case +++++++++++++++++++++++++++");
//			LoadLayoutTest layoutTest = new LoadLayoutTest();
//			layoutTest.TestLoadLayouts();
//			layoutTest.TestInstanceOfLayout();
//			
//			Logger.Debug("++++++++++++++++++++++++ Flag Provider Test Case +++++++++++++++++++++++++++");
//			FlagProviderTest flagTest = new FlagProviderTest();
//			flagTest.TestFlagProviderInitialize();
//			flagTest.TestKeyRegistration();
//			
//			Logger.Debug("______________________________ End Testing Program __________________________");
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}