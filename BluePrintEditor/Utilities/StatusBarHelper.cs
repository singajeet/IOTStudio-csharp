/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/31/2017
 * Time: 22:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using log4net;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of StatusBarHelper.
	/// </summary>
	public sealed class StatusBarHelper
	{
		ILog Logger = Log.Get(typeof(StatusBarHelper));
		
		private static StatusBarHelper instance = new StatusBarHelper();
		
		public static StatusBarHelper Instance {
			get {
				return instance;
			}
		}
		
		private StatusBarHelper()
		{
		}
		
		public void ShowMessage(string message, params object[] param)
		{
			MainWindowViewModel.Instance.StatusBarMessage = string.Format(message, param);
		}
		
		public void ShowAppStatus(string message, params object[] param)
		{
			MainWindowViewModel.Instance.StatusBarAppStatus = string.Format(message, param);
		}
		
		public void SetProgressBar(double value)
		{
			if (value <= 0) {
				MainWindowViewModel.Instance.ProgressBarVisibility = Visibility.Visible;
			}
			
			if (value >= 100) {
				MainWindowViewModel.Instance.ProgressBarVisibility = Visibility.Hidden;
			}
			
			MainWindowViewModel.Instance.ProgressBarValue = value;
		}
	}
}
