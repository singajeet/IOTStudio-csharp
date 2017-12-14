/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 7:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IOTStudio.Core.Types
{
	/// <summary>
	/// Description of FileStoreStatus.
	/// </summary>
	public enum DataStoreStatus
	{
		Stopped,
		Running,
		BootingUp
	}
	
	public enum DataStoreObjectType
	{
		File,
		Directory,
		SpecialDirectory
	}
}
