/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/11/2017
 * Time: 9:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using IOTStudio.Core.Elements.Interfaces;
using IOTStudio.Core.Models.Layouts;
using NUnit.Framework;

namespace IOTStudio.Tests.Layouts.Tests
{
	/// <summary>
	/// Description of LoadLayoutTest.
	/// </summary>
	[TestFixture]
	[Apartment(ApartmentState.STA)]
	public class LoadLayoutTest
	{
		LayoutSelector layouts = new LayoutSelector();
		
		public LoadLayoutTest()
		{
		}
		
		[Test(Description="Check number of layouts loaded from the assembly")]
		public void TestLoadLayouts()
		{
			Assert.AreEqual(2, layouts.Layouts.Count);
		}
		
		[Test(Description="Check base type of layouts loaded and should match to ILayoutElement")]
		public void TestInstanceOfLayout()
		{
			Assert.IsInstanceOf(typeof(ILayoutElement), layouts.Layouts[0]);
		}
	}
}
