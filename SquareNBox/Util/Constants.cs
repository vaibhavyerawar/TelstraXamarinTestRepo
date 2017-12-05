using System;
namespace SquareNBox
{
	public class Constants
	{
		/// <summary>
		/// The base URL.
		/// </summary>
		public readonly static String BASE_URL = "http://www.colourlovers.com/api";

		/// <summary>
		/// The circle data URL.
		/// </summary>
		public readonly static String CIRCLE_DATA_URL = BASE_URL + "/colors/random";

		/// <summary>
		/// The square data URL.
		/// </summary>
		public readonly static String SQUARE_DATA_URL = BASE_URL + "/patterns/random";


		/// <summary>
		/// The min limit for shape.
		/// </summary>
		public readonly static int MIN_LIMIT_FOR_SHAPE = 0;

		/// <summary>
		/// The max limit for shape.
		/// </summary>
		public readonly static int MAX_LIMIT_FOR_SHAPE = 2;

		/// <summary>
		/// The max limit for shape.
		/// </summary>
		public readonly static int SHAPE_DIMENSION_LIMIT = 4;

		/// <summary>
		/// The color of the max limit for.
		/// </summary>
		public readonly static int MAX_LIMIT_FOR_COLOR = 5;

		/// <summary>
		/// The max limit color code.
		/// </summary>
		public readonly static int MAX_LIMIT_COLOR_CODE = 255;


		/// <summary>
		///  Network error messages
		/// </summary>
		public readonly static String NETWORK_CONNECTION_ERROR_MSG = "Check Network Connectivity.";
		public readonly static String BAD_REQUEST_MSG = "Unable to process your request.";
   	    public readonly static String INTERNAL_SERVER_ERROR_MSG = "Unable to process your request.";
   	    public readonly static String SERVICE_UNAVAILABLE_ERROR_MSG = "Service Unavailable. Please try again.";

	}
}