using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SquareNBox
{
	/// <summary>
	/// Class for parsing XML response from network call.
	/// </summary>
	public class Parser
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SquareNBox.Parser"/> class.
		/// </summary>
		public Parser(){}

		/// <summary>
		/// Method for parsing XML response.
		/// </summary>
		/// <returns>XML parsed response in object.</returns>
		/// <param name="xmlString">Xml string for parsing</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T ParseResponse<T>(String xmlString) where T : new()
		{
			XmlReader reader = XmlReader.Create(new StringReader(xmlString));

			T resultObject = new T();

			var propertiesList = GetClassProperties<T>();

			foreach (var propertyInfo in propertiesList)
			{
				var attribute = propertyInfo.GetCustomAttribute<ParserAttribute>();

				reader.ReadToFollowing(attribute.XmlTag);
				String xmlTagContent = reader.ReadElementContentAsString();
				propertyInfo.SetValue(resultObject, xmlTagContent, null);
			}

			return resultObject;
		}

		/// <summary>
		/// Method for getting list properties of a class.
		/// </summary>
		/// <returns>List of class properties.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private PropertyInfo[] GetClassProperties<T>()
		{
			var classProperties = typeof(T).GetProperties();
			return classProperties;
		}
	}
}