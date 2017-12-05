using System;
using Android.Content;
using Android.Net;

namespace SquareNBox
{
	/// <summary>
	/// Class for providing utility functions to application modules.
	/// </summary>
	public class ApplicationUtil
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SquareNBox.ApplicationUtil"/> class.
		/// </summary>
		public ApplicationUtil()
		{
		}

		/// <summary>
		/// Function to generate ramdom number between 0 and maxValue.
		/// </summary>
		/// <returns>The random number.</returns>
		/// <param name="maxValue">Max upper limit value.</param>
		public static int RamdomNumberGenerator(int maxValue)
		{
			const int minValue = 1;
			return RamdomNumberGenerator(minValue, maxValue);
		}

		/// <summary>
		/// Function to generate ramdom number between minValue and maxValue.
		/// </summary>
		/// <returns>The random number.</returns>
		/// <param name="minValue">Minimum lower limit value.</param>
		/// <param name="maxValue">Max upper limit value.</param>
		public static int RamdomNumberGenerator(int minValue, int maxValue)
		{
			Random RamdomNumGenerator = new Random();
			return RamdomNumGenerator.Next(minValue, maxValue);
		}

		/// <summary>
		/// Funtion to check network connectivity.
		/// </summary>
		/// <returns><c>true</c>, if network connectivity was checked, <c>false</c> otherwise.</returns>
		/// <param name="context">Context.</param>
		public static bool CheckNetworkConnectivity(Context context)
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(
																					Context.ConnectivityService);
			NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
			return networkInfo.IsConnected;
		}
	}
}