/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/18/2017
 * Time: 4:39 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace IOTStudio.Core.Interfaces
{
	/// <summary>
	/// Description of ITask.
	/// </summary>
	public interface ITask
	{
		Guid Id { get; set; }
		string Name { get; set; }
		int Priority { get; set; }
	}
}
