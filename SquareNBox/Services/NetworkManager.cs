using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SquareNBox
{
	/// <summary>
	/// Class for managing network calls.
	/// </summary>
	public class NetworkManager
	{
		/// <summary>
		/// HttpClient Instance.
		/// </summary>
		private HttpClient client;

		/// <summary>
		/// Buffer size for response content.
		/// </summary>
		private readonly long BUFFER_SIZE_MAX = 256000;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SquareNBox.NetworkManager"/> class.
		/// </summary>
		public NetworkManager()
		{
			init();
		}

		/// <summary>
		/// Initialization function.
		/// </summary>
		private void init()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = BUFFER_SIZE_MAX;
		}

		/// <summary>
		/// Function for executing GET REST call.
		/// </summary>
		/// <returns>Network response in HttpResponseMessage object.</returns>
		/// <param name="Url">Requesting URL.</param>
		public async Task<HttpResponseMessage> GetDataAsync(String Url)
		{
			var requestUrl = new Uri(Url);
			var response = await client.GetAsync(requestUrl);
			return response;
		}
	}
}
