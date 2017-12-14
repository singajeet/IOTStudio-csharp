/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 7:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using IOTStudio.Core.Interfaces;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Providers.FileStore
{
	/// <summary>
	/// Description of FileStore.
	/// </summary>
	public class DataStore : IDataStore
	{
		
		public DataStore()
		{
		}

		#region IFileStore implementation

		public void MarkAsSpecialFolder(string path)
		{
			throw new NotImplementedException();
		}

		public void MarkAsSpecialFolder(DirectoryInfo dir)
		{
			throw new NotImplementedException();
		}

		public FileInfo CreateFile(string path)
		{
			throw new NotImplementedException();
		}

		public FileInfo OpenFile(string path)
		{
			throw new NotImplementedException();
		}

		public FileInfo GetFileInfo(string path)
		{
			throw new NotImplementedException();
		}

		public DirectoryInfo CreateDirectory(string path)
		{
			throw new NotImplementedException();
		}

		public DirectoryInfo OpenDirectory(string path)
		{
			throw new NotImplementedException();
		}

		public DirectoryInfo GetDirectoryInfo(string path)
		{
			throw new NotImplementedException();
		}

		public FileInfo DataStoreMetaDataFile {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public DirectoryInfo DataStoreBaseDirectory {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public bool IsBackedUp {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public bool IsCompressed {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public bool IsSecurityRequired {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public bool IsSecured {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public bool IsSpecialFolder {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		public DataStoreStatus Status {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}

		#endregion
	}
	
	public class DataStoreEntry
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string PathKey { get; set; }
		public string Extension { get; set; }
		public string RefPath { get; set; }
		public DataStoreObjectType ObjectType { get; set; }
	}
}
