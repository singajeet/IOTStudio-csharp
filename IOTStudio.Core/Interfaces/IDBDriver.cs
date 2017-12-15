/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 10:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Types;
using LiteDB;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of Idb.
	/// </summary>
	public interface IDBDriver
	{
		string ConnectionString { get; }
		string Schema { get; }
		void SetSchema(string schema);
		LiteDatabase DB{ get; }
		void Connect();
		ConnectionStatus ConnectionStatus { get; }
//		LiteCollection<T> GetCollection<T>(string collectionName);
//		LiteStorage FileStore{ get; }
	}
}
