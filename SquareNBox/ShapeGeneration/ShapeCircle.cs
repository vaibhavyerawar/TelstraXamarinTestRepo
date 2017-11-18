using System;
using Android.Content;
using Android.Graphics;
using Android.Util;

namespace ShapeGenerator
{
	/**
	 * Class to create circle shape.
	 **/
	public class ShapeCircle : ShapeBase
	{
		//Graphics rect object for creating bounds on canavas.
		private readonly RectF DrawableRect = new RectF();

		//Radius to draw circle on canvas. 
		private float DrawableRadius;

		//Constant value to calculate radius from diamention.
		private readonly float DIVIDER = 2.0f;

		public ShapeCircle(Context context) : base(context)
		{
		}

		public ShapeCircle(Context context, IAttributeSet attrs) : base(context, attrs, 0)
		{
		}

		public ShapeCircle(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
		}

		/**
		 * View function to update view canvas.
		 **/
		protected override void OnDraw(Canvas canvas)
		{
			DrawableRect.Set(calculateBounds());
			DrawableRadius = Math.Min(DrawableRect.Height() / DIVIDER, DrawableRect.Width() / DIVIDER);

			if (BitmapImage != null)
			{
				canvas.DrawCircle(DrawableRect.CenterX(), DrawableRect.CenterY(), DrawableRadius, BitmapPaint);
			}
			else
			{
				canvas.DrawCircle(DrawableRect.CenterX(), DrawableRect.CenterY(), DrawableRadius, ShapeBackgroundPaint);
			}
		}
	}
}