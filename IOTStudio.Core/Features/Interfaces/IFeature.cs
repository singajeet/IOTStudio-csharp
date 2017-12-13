/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 7:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using IOTStudio.Core.Providers.Stacks;

namespace IOTStudio.Core.Features.Interfaces
{
	/// <summary>
	/// Description of IFeature.
	/// </summary>
	public interface IFeature
	{
		Guid Id { get; set; }
		string Name { get; set; }
		void NotifyInputAvailable(InputStack source);
	}
}
