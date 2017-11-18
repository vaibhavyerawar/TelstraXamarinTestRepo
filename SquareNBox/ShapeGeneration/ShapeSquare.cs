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
		//Graphics rect object for creating bounds on canavas.
		private RectF DrawableRect;

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
			DrawableRect = new RectF(mWidth, mHeight, 0, 0);
			
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