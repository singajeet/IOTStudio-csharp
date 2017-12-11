/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/11/2017
 * Time: 1:13 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;
using IOTStudio.Core.Elements.Interfaces;

namespace IOTStudio.Core.Models.Layouts
{
	/// <summary>
	/// Description of LayoutSelector.
	/// </summary>
	[DataContract(IsReference = true)]
	public class LayoutSelector: DependencyObject, INotifyPropertyChanged
	{
		
		public static readonly DependencyProperty 
											SelectedLayoutProperty = DependencyProperty
																	.Register("SelectedLayout", 
			          												typeof(ILayoutElement), 
			          												typeof(LayoutSelector),
			                                                        new PropertyMetadata());
		
		[DataMember]
		public ILayoutElement SelectedLayout{
			get { return (ILayoutElement)GetValue(SelectedLayoutProperty); }
			set { SetValue(SelectedLayoutProperty, value); }
		}
		public LayoutSelector()
		{
			SelectedLayout = new DefaultLayout();
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName]string memberName = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, new PropertyChangedEventArgs(memberName));
			}
			
		}
		#endregion
	}
}
