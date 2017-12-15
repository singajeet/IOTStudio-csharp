/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 11:13 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Providers.Pipes;

namespace IOTStudio.Core.Providers.Pipes
{
	/// <summary>
	/// Description of IOutputConsumer.
	/// </summary>
	public interface IOutputConsumer
	{
		Guid Id { get; set; }
		string Name { get; set; }
		void OutputAvailableNotification(OutputPipe output);
	}
}
