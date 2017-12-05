using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using ShapeGenerator;

namespace SquareNBox
{
	/// <summary>
	/// Abstract base class for ViewModel classes.
	/// </summary>
	public abstract class BaseViewModel
	{
		/// <summary>
		/// The NetworkManager class instance.
		/// </summary>
		private NetworkManager networkManager;

		/// <summary>
		/// The xml Parser class instance.
		/// </summary>
		private Parser XmlParser;

		/// <summary>
		/// The ShapeFactory class instance.
		/// </summary>
		private ShapeFactory shapeFactory;

		/// <summary>
		/// Activity context reference.
		/// </summary>
		protected Context context;

		/// <summary>
		/// Entity model class ShapeData reference.
		/// </summary>
		/// <value>The shape model.</value>
		private ShapeData ShapeModel { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SquareNBox.BaseViewModel"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public BaseViewModel(Context context)
		{
			this.context = context;
			init();
		}

		/// <summary>
		/// Function to initialisation of objects and class memebers.
		/// </summary>
		private void init()
		{
			networkManager = new NetworkManager();
			XmlParser = new Parser();
			shapeFactory = new ShapeFactory(context);
		}

		/// <summary>
		/// Function to make a network call.
		/// </summary>
		/// <returns>True if no error else return false on faliure.</returns>
		/// <param name="requestURL">URL for network call.</param>
		private async Task<HttpResponseMessage> MakeNetworkCallAsync(String requestURL)
		{
			var serverResponse = await networkManager.GetDataAsync(requestURL);

			if (!serverResponse.IsSuccessStatusCode)
			{
				VerifyError(serverResponse);
			}

			return serverResponse;
		}

		/// <summary>
		/// Parses the response.
		/// </summary>
		/// <param name="responseContent">Response content.</param>
		private void ParseResponse(String responseContent)
		{
			ShapeModel = XmlParser.ParseResponse<ShapeData>(responseContent);
		}

		/// <summary>
		/// Function to read string data from response.
		/// </summary>
		/// <returns>The response content as string.</returns>
		/// <param name="serverResponse">Server response.</param>
		private async Task<String> ReadResponseContentAsString(HttpResponseMessage serverResponse)
		{
			var responseContentAsString = await serverResponse.Content.ReadAsStringAsync();
			return responseContentAsString;
		}

		/// <summary>
		/// Reads the response content as bytes.
		/// </summary>
		/// <returns>The response content as bytes.</returns>
		/// <param name="serverResponse">Server response.</param>
		private async Task<byte[]> ReadResponseContentAsBytes(HttpResponseMessage serverResponse)
		{
			var responseContentAsBytes = await serverResponse.Content.ReadAsByteArrayAsync();
			return responseContentAsBytes;
		}

		/// <summary>
		/// Function to create and return new required shape.
		/// </summary>
		/// <returns>The new shape.</returns>
		/// <param name="shapeEnum">ShapeEnum for getting required shape.</param>
		protected ShapeBase CreateNewShape(ShapeEnum shapeEnum)
		{
			ShapeBase newShape = shapeFactory.GetNewShape(shapeEnum);
			return newShape;
		}

		/// <summary>
		/// Function to set color to the shape.
		/// </summary>
		/// <param name="shapeInstance">Shape instance.</param>
		/// <param name="shapeFaceColor">Shape face color.</param>
		protected void SetColorToShape(ShapeBase shapeInstance, Color shapeFaceColor)
		{
			shapeInstance.setShapeBackgroundColor(shapeFaceColor);
		}

		/// <summary>
		/// Sets the image to shape.
		/// </summary>
		/// <returns>The image to shape.</returns>
		/// <param name="shapeInstance">Instance of shape.</param>
		/// <param name="requestUrl">Request URL.</param>
		protected async Task SetImageToShape(ShapeBase shapeInstance, String requestUrl)
		{
			HttpResponseMessage responseMessage = await MakeNetworkCallAsync(requestUrl);

			if (responseMessage != null)
			{
				String responseContent = await ReadResponseContentAsString(responseMessage);
				ParseResponse(responseContent);
				responseMessage = await MakeNetworkCallAsync(ShapeModel.ImageUrl);

				if (responseMessage != null)
				{
					byte[] imageByteData = await ReadResponseContentAsBytes(responseMessage);
					Bitmap shapeBitmap = ConvertBytesToBitmap(imageByteData, shapeInstance.Width, shapeInstance.Height);
					shapeInstance.SetImageBitmap(shapeBitmap);
				}
			}
		}

		/// <summary>
		/// Converts the bytes to bitmap.
		/// </summary>
		/// <param name="imageByteData">Image byte data.</param>
		private Bitmap ConvertBytesToBitmap(byte[] imageByteData, int width, int height)
		{
			int OFFSET = 0;
			Bitmap bitmapForShape = BitmapFactory.DecodeByteArray(imageByteData, OFFSET, imageByteData.Length);
			bitmapForShape = Bitmap.CreateScaledBitmap(bitmapForShape, width, height, false);
			return bitmapForShape;
		}

		/// <summary>
		/// Function to verify cause of network error and specify message.
		/// </summary>
		/// <param name="httpResponseMsg">Response message of network call.</param>
		private void VerifyError(HttpResponseMessage httpResponseMsg)
		{
			String ErrorMsg;

			switch (httpResponseMsg.StatusCode)
			{
				case HttpStatusCode.BadRequest:
					ErrorMsg = Constants.BAD_REQUEST_MSG;
					break;

				case HttpStatusCode.ServiceUnavailable:
					ErrorMsg = Constants.SERVICE_UNAVAILABLE_ERROR_MSG;
					break;

				case HttpStatusCode.Forbidden:
					ErrorMsg = Constants.INTERNAL_SERVER_ERROR_MSG;
					break;

				default:
					ErrorMsg = Constants.NETWORK_CONNECTION_ERROR_MSG;
					break;
			}

			throw new ArgumentException(ErrorMsg);
		}
	}
}