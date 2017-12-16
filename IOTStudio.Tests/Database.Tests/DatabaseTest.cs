/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/15/2017
 * Time: 11:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using IOTStudio.Core.Database;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Types;
using LiteDB;
using NUnit.Framework;

namespace IOTStudio.Tests.Database.Tests
{
	public class DbTestData
	{
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		
		public DbTestData()
		{
		}
		
		public DbTestData(string name)
		{
			Name = name;
		}
		
		public override string ToString()
		{
			return string.Format("[DbTestData Id={0}, Name={1}]", Id, Name);
		}

	}
	
	public class Parent
	{
		public ObjectId Id { get; set; }
		public string ParentName { get; set; }
		public IList<Child> ChildItems { get; set; }
		
		public Parent()
		{
			ChildItems = new List<Child>();
		}
		
		public Parent(string name) : this()
		{
			ParentName = name;
		}
		
		public void AddChild(Child item)
		{
			ChildItems.Add(item);
			Console.WriteLine("[{0}] added to Parent [{1}]", item, ParentName);
		}
		
		public override string ToString()
		{
			return string.Format("[Parent Id={0}, ParentName={1}]", Id, ParentName);
		}

	}
	
	public class Child
	{
		public ObjectId Id { get; set; }
		public string ChildName { get; set; }
		public Child ParentItem { get; set; }
		public IList<Child> ChildItems { get; set; }
		public int Level { get; set; }
		
		public Child()
		{
			ChildItems = new List<Child>();
			Level = 0;
		}
		
		public Child(string name) : this()
		{
			ChildName = name;			
		}
		
		public void AddChild(Child item)
		{
			item.Level = Level + 1;
			ChildItems.Add(item);
			Console.WriteLine("[{0}] added to Parent [{1}]", item, ChildName);
		}
		
		public override string ToString()
		{
			return string.Format("[Child Id={0}, ChildName={1}, Level={2}]", Id, ChildName, Level);
		}

	}
	/// <summary>
	/// Description of DatabaseTest.
	/// </summary>
	[TestFixture(Description="Database interface test case")]
	public class DatabaseTest
	{
		DatabaseFactory factory;
		IDBDriver dbDriver;
		
		public DatabaseTest()
		{
		}
		
		[Test, Order(1)]
		public void TestDatabaseInit()
		{
			factory = DatabaseFactory.Instance;
			Assert.AreEqual("DatabaseFactory", factory.Name);
		}
		
		[Test, Order(2)]
		public void TestLoadDatabase()
		{
			dbDriver = factory.LoadDefaultDatabase("Test.db");
			Assert.AreEqual(ConnectionStatus.Disconnected, dbDriver.ConnectionStatus);
			
			dbDriver.Connect();
			Assert.AreEqual(ConnectionStatus.Connected, dbDriver.ConnectionStatus);
			
			Assert.AreEqual("Test.db", dbDriver.Schema);
		}
		
		[SetUp]
		public void  Setup()
		{
		}
		
		[Test, Order(3)]
		public void TestDatabaseDeleteQuery()
		{
			DbTestData testData = new DbTestData("MyFirstTestData");
			dbDriver.DB.GetCollection<DbTestData>("collection").Insert(testData);
			
			int count = dbDriver.DB.GetCollection<DbTestData>("collection")
									.Count(f => f.Name.StartsWith("My"));
			int deletedItems = dbDriver.DB.GetCollection<DbTestData>("collection")
							.Delete(f => f.Name.StartsWith("My"));
			
			Assert.AreEqual(count, deletedItems);
		}
		
		[Test, Order(4)]
		public void TestDatabaseInsertQuery()
		{
			DbTestData testData = new DbTestData("MyFirstTestData");
			dbDriver.DB.GetCollection<DbTestData>("collection").Insert(testData);
			
			DbTestData testData2 = new DbTestData("MySecondTestData");
			dbDriver.DB.GetCollection<DbTestData>("collection").Insert(testData2);
			
			Assert.AreEqual(2, dbDriver.DB.GetCollection<DbTestData>("collection")
			                .Count(f=>f.Name.StartsWith("My")));			
		}
		
		[Test, Order(5)]
		public void TestDatabaseFindOneQuery()
		{
			DbTestData testData = dbDriver.DB
				.GetCollection<DbTestData>("collection")
				.FindOne(t => t.Name.Equals("MyFirstTestData"));
			
			Console.WriteLine(testData);
			Assert.AreEqual("MyFirstTestData", testData.Name);
		}
		
		[Test, Order(6)]
		public void TestDatabaseFindCount()
		{
			var count = dbDriver.DB
				.GetCollection<DbTestData>("collection")
				.Count(f=>f.Name.StartsWith("My"));
			
			Assert.AreEqual(2, count);
		}
		
		[Test, Order(7)]
		public void TestDatabaseFindQuery()
		{
			var testCollection = dbDriver.DB
				.GetCollection<DbTestData>("collection")
				.Find(f => f.Name.StartsWith("My"));
			
			foreach (var data in testCollection) {
				Console.WriteLine(data);
				Assert.IsInstanceOf(typeof(DbTestData), data);
			}
		}
		
		public void DeleteAllItems()
		{
			dbDriver.DB.GetCollection<DbTestData>("collection")
				.Delete(f => f.Name.StartsWith("My"));
		}
		
		[Test, Order(8)]
		public void TestDatabaseParentChildInsertQuery()
		{
			DeleteAllItems();
			Parent p = new Parent("Parent1");
			Child c1 = new Child("Child1");
			Child c2 = new Child("Child2");
			
			p.AddChild(c1);
			c1.AddChild(c2);
			
			dbDriver.DB.GetCollection<Parent>("ParentCollection")
				.Insert(p);
			
			Assert.AreEqual(1, dbDriver.DB.GetCollection<Parent>("ParentCollection")
			                .Count(pc => pc.ParentName.Equals("Parent1")));
		}
		
		[Test, Order(9)]
		public void TestDatabaseParentChildFindOneQuery()
		{
			Parent p = dbDriver.DB.GetCollection<Parent>("ParentCollection")
							.FindOne(pc => pc.ParentName.Equals("Parent1"));
			Console.WriteLine(p);
			Console.WriteLine(p.ChildItems[0]);
			Console.WriteLine(p.ChildItems[0].ChildItems[0]);
			Assert.AreEqual("Parent1", p.ParentName);
			Assert.AreEqual(1, p.ChildItems.Count);
			Assert.AreEqual(1, p.ChildItems[0].ChildItems.Count);
		}
	}
}
