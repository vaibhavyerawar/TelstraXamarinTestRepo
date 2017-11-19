using System;
using Android.Content;
using Android.Graphics;
using Android.Util;

namespace ShapeGenerator
{
	/**
	 * Class to create square shape.
	 **/
	public class ShapeSquare : ShapeBase
	{

		public ShapeSquare(Context context) : base(context)
		{
		}

		public ShapeSquare(Context context, IAttributeSet attrs) : base(context, attrs, 0)
		{
		}

		public ShapeSquare(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
		}

		/**
		 * View function to update view canvas.
		 **/
		protected override void OnDraw(Canvas canvas)
		{
			BorderRect = CalculateBounds();
			DrawableRect.Set(BorderRect);

			DrawableRect.Inset(DEFAULT_BORDER_WIDTH - DEFAULT_INSET_PADDING, 
			                   DEFAULT_BORDER_WIDTH - DEFAULT_INSET_PADDING);

			canvas.DrawRect(BorderRect, BorderPaint);

			if (BitmapImage != null)
			{
				canvas.DrawRect(DrawableRect, BitmapPaint);
			}
			else
			{
				canvas.DrawRect(DrawableRect, ShapeBackgroundPaint);
			}
		}
	}
}