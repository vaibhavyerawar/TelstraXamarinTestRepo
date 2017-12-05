using System;
namespace SquareNBox
{
	/// <summary>
	/// Custom attribute class for parser.
	/// </summary>
	public class ParserAttribute : Attribute
	{
		/// <summary>
		/// Property to set lable of XML tag for parsing.
		/// </summary>
		/// <value>The xml tag.</value>
		public String XmlTag { get; set;}
	}
}