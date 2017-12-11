/*
 * Created by SharpDevelop.
 * User: Admin
 * Date: 12/10/2017
 * Time: 5:55 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Markup;
using System.Xml;

namespace IOTStudio.Core.Serializers
{
	/// <summary>
	/// Description of XAMLSerializer.
	/// </summary>
	public static class XAMLSerializer
	{
		private static XmlWriterSettings xmlWriterSettings;
		private static XmlReaderSettings xmlReaderSettings;
		private static XmlWriter xmlWriter;
		private static XmlReader xmlReader;
		
		
		public static void Serialize(object instance, string filename)
		{
			xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;
			xmlWriter = XmlWriter.Create(filename, xmlWriterSettings);
			
			using (xmlWriter) {
				XamlWriter.Save(instance, xmlWriter);
			}
		}
		
		public static object Deserialize(string filename)
		{
			object instance = null;
			xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.IgnoreComments = true;
			xmlReader = XmlReader.Create(filename, xmlReaderSettings);
			
			using (xmlReader) {
				instance = XamlReader.Load(xmlReader);
			}
			
			return instance;
		}
	}
}
