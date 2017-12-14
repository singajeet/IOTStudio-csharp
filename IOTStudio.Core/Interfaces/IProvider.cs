/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/14/2017
 * Time: 9:08 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of IProvider.
	/// </summary>
	public interface IProvider
	{
		Guid Id { get; }
		string Name { get; }		
	}
}
