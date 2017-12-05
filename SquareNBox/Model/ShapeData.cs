using System;

namespace SquareNBox
{
	/// <summary>
	/// Enity model classs for shape data.
	/// </summary>
	public class ShapeData
	{
		/// <summary>
		/// The title.
		/// </summary>
		private String title;

		/// <summary>
		/// The image URL.
		/// </summary>
		private String imageUrl;

		/// <summary>
		/// The badge URL.
		/// </summary>
		private String badgeUrl;

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[ParserAttribute(XmlTag = "title")]
		public String Title { set { title = value; } get { return title; } }

		/// <summary>
		/// Gets or sets the image URL.
		/// </summary>
		/// <value>The image URL.</value>
		[ParserAttribute(XmlTag = "imageUrl")]
		public String ImageUrl { set { imageUrl = value; } get { return imageUrl; } }

		/// <summary>
		/// Gets or sets the badge URL.
		/// </summary>
		/// <value>The badge URL.</value>
		[ParserAttribute(XmlTag = "badgeUrl")]
		public String BadgeUrl { set { badgeUrl = value; } get { return badgeUrl; } }
	}
}