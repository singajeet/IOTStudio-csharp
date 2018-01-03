/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/21/2017
 * Time: 6:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using BluePrintEditor.Utilities;
using log4net;

namespace BluePrintEditor.Items.Start
{
	/// <summary>
	/// Interaction logic for HamburgerMenuStartView.xaml
	/// </summary>
	public partial class HamburgerMenuStartView : UserControl
	{
		ILog Logger = Log.Get(typeof(HamburgerMenuStartView));
		
		Guid id;
		
		public Guid Id {
			get { return id; }
			set { 
					id = value; 
				Logger.PropertyChanged(value);
			}
		}
		public HamburgerMenuStartView()
		{
			InitializeComponent();
			Id = Guid.NewGuid();
		}
		
		public override string ToString()
		{
			return string.Format("[HamburgerMenuStartView Id={0}]", id);
		}

	}
}