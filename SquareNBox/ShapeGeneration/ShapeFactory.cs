using System;
using Android.Content;

namespace ShapeGenerator
{
	public class ShapeFactory
	{
		private Context AppContext;

		public ShapeFactory(Context context)
		{
			AppContext = context;
		}

		public ShapeBase GetNewShape(ShapeEnum shapeEnum)
		{
			switch (shapeEnum)
			{
				case ShapeEnum.SHAPE_CIRCLE:
					return new ShapeCircle(AppContext);

				case ShapeEnum.SHAPE_SQUARE:
					return new ShapeSquare(AppContext);

				default:
					return null;
			}
		}
	}
}