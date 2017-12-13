/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/12/2017
 * Time: 11:56 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using IOTStudio.Core.Features.Interfaces;

namespace IOTStudio.Core.Providers.Pipes
{
	/// <summary>
	/// Description of FeatureCollection.
	/// </summary>
	public class OutputConsumerCollection : ObservableCollection<IOutputConsumer>
	{
		public OutputConsumerCollection()
		{
		}
		
		public IOutputConsumer this[string name]{
			get {
				foreach (IOutputConsumer feature in this.Items) {
					if (feature.Name.Equals(name))
						return feature;
				}
				return null;
			}
			
			set { 
				for (int i = 0; i < this.Items.Count; i++) {
					if (this.Items[i].Name.Equals(name))
						this.SetItem(i, value);
				}
			}
		}
	}
}
