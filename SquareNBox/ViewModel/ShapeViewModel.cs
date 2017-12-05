using System;
using System.Threading.Tasks;
using System.Xml;
using Android.Content;
using Android.Graphics;
using ShapeGenerator;

namespace SquareNBox
{
	public class ShapeViewModel : BaseViewModel
	{
		/// <summary>
		/// The shape dimension constant.
		/// </summary>
		private readonly int SHAPE_DIMENSION_CONSTANT = 100;

		/// <summary>
		/// The height of the width.
		/// </summary>
		private int WidthHeight;

		/// <summary>
		/// The type of the next shape.
		/// </summary>
		private ShapeEnum nextShapeType;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SquareNBox.ShapeViewModel"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public ShapeViewModel(Context context) : base(context)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The new shape.</returns>
		public ShapeBase GenerateNewShape()
		{
			int nextShapeIndex = ApplicationUtil.RamdomNumberGenerator(Constants.MIN_LIMIT_FOR_SHAPE, 
			                                                           Constants.MAX_LIMIT_FOR_SHAPE);
			nextShapeType = (ShapeEnum)Enum.Parse(typeof(ShapeEnum), nextShapeIndex.ToString());
			ShapeBase nextShape = CreateNewShape(nextShapeType);
			SetDiamentions(nextShape);
			return nextShape;
		}

		/// <summary>
		/// Decorates the shape.
		/// </summary>
		/// <returns>The shape.</returns>
		/// <param name="shapeToDecorate">Shape to decorate.</param>
		public async Task DecorateShape(ShapeBase shapeToDecorate)
		{
			Color shapeColor = GenerateNewColor();
			SetColorToShape(shapeToDecorate, shapeColor);

			if (ApplicationUtil.CheckNetworkConnectivity(context))
			{
				switch (nextShapeType)
				{
					case ShapeEnum.SHAPE_CIRCLE:
						await SetImageToShape(shapeToDecorate, Constants.CIRCLE_DATA_URL);
						break;

					case ShapeEnum.SHAPE_SQUARE:
						await SetImageToShape(shapeToDecorate, Constants.SQUARE_DATA_URL);
						break;
				}
			}
		}
	

		/// <summary>
		/// Sets the diamentions.
		/// </summary>
		/// <param name="newShape">New shape.</param>
		private void SetDiamentions(ShapeBase newShape)
		{
			WidthHeight = SHAPE_DIMENSION_CONSTANT * 
						ApplicationUtil.RamdomNumberGenerator(Constants.SHAPE_DIMENSION_LIMIT);
			newShape.SetShapeDiamentions(WidthHeight, WidthHeight);
		}


		/// <summary>
		/// Generates the new color.
		/// </summary>
		/// <returns>The new color.</returns>
		private Color GenerateNewColor()
		{
			int red = ApplicationUtil.RamdomNumberGenerator(Constants.MAX_LIMIT_COLOR_CODE);
			int green = ApplicationUtil.RamdomNumberGenerator(Constants.MAX_LIMIT_COLOR_CODE);
			int blue = ApplicationUtil.RamdomNumberGenerator(Constants.MAX_LIMIT_COLOR_CODE);
			Color newShapeColor = Color.Rgb(red, green, blue);
			return newShapeColor;
		}
	}
}