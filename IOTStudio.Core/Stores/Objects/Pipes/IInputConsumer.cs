/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/13/2017
 * Time: 11:14 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IOTStudio.Core.Providers.Pipes;

namespace IOTStudio.Core.Providers.Pipes
{
	/// <summary>
	/// Description of IInputConsumer.
	/// </summary>
	public interface IInputConsumer
	{
		Guid Id { get; set; }
		string Name { get; set; }
		void InputAvailableNotification(InputPipe input);
	}
}
