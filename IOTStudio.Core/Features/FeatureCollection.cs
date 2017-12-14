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
using IOTStudio.Core.Interfaces;

namespace IOTStudio.Core.Features
{
	/// <summary>
	/// Description of FeatureCollection.
	/// </summary>
	public class FeatureCollection : ObservableCollection<IFeature>
	{
		public FeatureCollection()
		{
		}
		
		public IFeature this[string name]{
			get {
				foreach (IFeature feature in this.Items) {
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
