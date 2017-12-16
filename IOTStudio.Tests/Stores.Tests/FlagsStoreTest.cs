/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 11:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Stores;
using NUnit.Framework;

namespace IOTStudio.Tests.Stores.Tests
{
	/// <summary>
	/// Description of FlagsTest.
	/// </summary>
	[TestFixture(Description="Test FlagProvider's functionality")]
	public class FlagsStoreTest
	{
		Get m = Get.i;
		public FlagsStoreTest()
		{
		}
		
		[Test]
		public void TestFlagProviderInitialize()
		{
			Assert.AreEqual(0, Get.i.Flags.AllFlags.Count);
		}
		
		[Test]
		public void TestKeyRegistration()
		{
			Get.i.Flags.RegisterFlag("TestFlag", false);
			Assert.AreEqual(1, Get.i.Flags.AllFlags.Count);
			Assert.AreEqual(false, Get.i.Flags.GetFlagStatus("TestFlag"));
		}
	}
}
