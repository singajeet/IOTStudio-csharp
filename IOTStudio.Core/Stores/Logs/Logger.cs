/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/11/2017
 * Time: 11:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using log4net;

namespace IOTStudio.Core.Stores.Logs
{
	/// <summary>
	/// Description of Logger.
	/// </summary>
	public static class Logger
	{
		private static ILog realLogger;
		
		static Logger(){
			realLogger = CreateLogger();
		}
		
		private static ILog RealLogger{
			get { 
				return realLogger ?? (realLogger = CreateLogger());
			}
		}
		
		private static ILog CreateLogger()
		{
			var frame = new StackFrame(3);
			return LogManager.GetLogger(frame.GetMethod().DeclaringType.FullName);
		}
		
		public static void Debug(string message, params object[] param)
		{
			RealLogger.Debug(string.Format(message, param));
		}
		public static void Debug(string message, Exception ex, params object[] param)
		{
			RealLogger.Debug(string.Format(message, param), ex);
		}
		public static void Info(string message, params object[] param)
		{
			RealLogger.Info(string.Format(message, param));
		}
		public static void Warn(string message, params object[] param)
		{
			RealLogger.Warn(string.Format(message, param));
		}
		public static void Warn(string message, Exception ex, params object[] param)
		{
			RealLogger.Warn(string.Format(message, param), ex);
		}
		public static void Error(string message, params object[] param)
		{
			RealLogger.Error(string.Format(message, param));
		}
		public static void Error(string message, Exception ex, params object[] param)
		{
			RealLogger.Error(string.Format(message, param), ex);
		}
	}
}
