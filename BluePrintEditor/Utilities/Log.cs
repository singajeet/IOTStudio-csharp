/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/28/2017
 * Time: 2:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows;
using log4net;

namespace BluePrintEditor.Utilities
{
	/// <summary>
	/// Description of Log4NetExtensions.
	/// </summary>
	public static class Log
	{
		public static ILog Get(Type type)
		{
			return LogManager.GetLogger(type);
		}
		
		
		public static void InstanceCreated(this ILog logger)
		{
			logger.Debug("Instance Created");
		}
		
		public static void MethodCalled(this ILog logger, [CallerMemberName]string methodName = null)
		{
			logger.DebugF("Method() Called => [{0}]", methodName);
		}
		
		public static void CallbackExecuted(this ILog logger, [CallerMemberName]string methodName = null)
		{
			logger.DebugF("Callback() Executed => [{0}]", methodName);
		}
		
		public static void DataContextChanged(this ILog logger, DependencyPropertyChangedEventArgs e)
		{
			logger.DataContextChanged(e.OldValue, e.NewValue);
		}
		
		public static void DataContextChanged(this ILog logger, object oldVal, object newVal)
		{
			logger.DebugF("DataContextChanged => [From={0}, To={1}]", oldVal ?? "Empty", newVal ?? "Empty");
		}
		
		public static void PropertyChanged(this ILog logger, string propName, object val)
		{
			logger.DebugF("[{0}] Value Changed => [{1}]", propName, val);
		}
		
		public static void PropertyChanged(this ILog logger, object val, [CallerMemberName]string propName = null)
		{
			logger.PropertyChanged(propName, val);
		}
		
		public static void CollectionCreated(this ILog logger, ICollection collection)
		{
			logger.DebugF("Collection Created => [{0}, Count={1}]", collection.GetType().Name, collection.Count);
		}
		
		public static void DebugF(this ILog logger, string message, params object[] param)
		{
			logger.Debug(string.Format(message, param));
		}
		
		public static void DebugF(this ILog logger, string message, Exception ex, params object[] param)
		{
			logger.Debug(string.Format(message, param), ex);
		}
		
		public static void InfoF(this ILog logger, string message, params object[] param)
		{
			logger.Info(string.Format(message, param));
		}
		
		public static void InfoF(this ILog logger, string message, Exception ex, params object[] param)
		{
			logger.Info(string.Format(message, param), ex);
		}
		
		public static void WarnF(this ILog logger, string message, params object[] param)
		{
			logger.Warn(string.Format(message, param));
		}
		
		public static void WarnF(this ILog logger, string message, Exception ex, params object[] param)
		{
			logger.Warn(string.Format(message, param), ex);
		}
		
		public static void ErrorF(this ILog logger, string message, params object[] param)
		{
			logger.Error(string.Format(message, param));
		}
		
		public static void ErrorF(this ILog logger, string message, Exception ex, params object[] param)
		{
			logger.Error(string.Format(message, param), ex);
		}
	}
}
