/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 11:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Providers;
using NUnit.Framework;

namespace IOTStudio.Tests.Providers.Tests
{
	/// <summary>
	/// Description of FlagsTest.
	/// </summary>
	[TestFixture(Description="Test FlagProvider's functionality")]
	public class FlagProviderTest
	{
		Get m = Get.i;
		public FlagProviderTest()
		{
		}
		
		[Test]
		public void TestFlagProviderInitialize()
		{
			Assert.AreEqual(0, Get.i.FlagProvider.Flags.Count);
		}
		
		[Test]
		public void TestKeyRegistration()
		{
			Get.i.FlagProvider.RegisterFlag("TestFlag", false);
			Assert.AreEqual(1, Get.i.FlagProvider.Flags.Count);
			Assert.AreEqual(false, Get.i.FlagProvider.GetFlagStatus("TestFlag"));
		}
	}
}
