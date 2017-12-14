/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 6:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using IOTStudio.Core.Types;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of IFileStore.
	/// </summary>
	public interface IDataStore
	{
		FileInfo DataStoreMetaDataFile { get; set; }
		DirectoryInfo DataStoreBaseDirectory { get; set; }
		
		bool IsBackedUp { get; set; }
		bool IsCompressed { get; set; }
		bool IsSecurityRequired { get; set; }
		bool IsSecured { get; set; }
		bool IsSpecialFolder { get; set; }
		
		DataStoreStatus Status { get; set; }
		
		void MarkAsSpecialFolder(string path);
		void MarkAsSpecialFolder(DirectoryInfo dir);
		
		FileInfo CreateFile(string path);
		FileInfo OpenFile(string path);
		FileInfo GetFileInfo(string path);
		
		DirectoryInfo CreateDirectory(string path);
		DirectoryInfo OpenDirectory(string path);
		DirectoryInfo GetDirectoryInfo(string path);
	}
}
