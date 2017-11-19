using System;
using Android.Content;
using Android.Net;

namespace SquareNBox
{
	/**
	 * Class for providing utility functions to application modules.
	 **/
	public class ApplicationUtil
	{
		public ApplicationUtil()
		{
		}

		//Function to generate ramdom number between 0 and maxValue.
		public static int RamdomNumberGenerator(int maxValue)
		{
			int minValue = 1;
			return RamdomNumberGenerator(minValue, maxValue);
		}

		//Function to generate ramdom number between minValue and maxValue.
		public static int RamdomNumberGenerator(int minValue, int maxValue)
		{
			Random RamdomNumGenerator = new Random();
			return RamdomNumGenerator.Next(minValue, maxValue);
		}

		//Funtion to check network connectivity.
		public static bool CheckNetworkConnectivity(Context context)
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(
																					Context.ConnectivityService);
			NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
			return networkInfo.IsConnected;
		}
	}
}