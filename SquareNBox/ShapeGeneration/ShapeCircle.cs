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
		//Radius to draw circle on canvas. 
		private float DrawableRadius;

		//Radius to draw border to circle on canvas. 
		private float BorderRadius;

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
			BorderRect = CalculateBounds();
			DrawableRect.Set(BorderRect);

			float insetAreaWidth = BorderRect.Height() - DEFAULT_BORDER_WIDTH;
			float insetAreaHeight = BorderRect.Width() - DEFAULT_BORDER_WIDTH;

			BorderRadius = Math.Min(insetAreaWidth/ DIVIDER,  insetAreaHeight/ DIVIDER);

			DrawableRect.Inset(DEFAULT_BORDER_WIDTH - DEFAULT_INSET_PADDING, 
			                   DEFAULT_BORDER_WIDTH - DEFAULT_INSET_PADDING);
			
            DrawableRadius = Math.Min(DrawableRect.Height() / DIVIDER, DrawableRect.Width() / DIVIDER);

			canvas.DrawCircle(BorderRect.CenterX(), BorderRect.CenterY(), BorderRadius, BorderPaint);

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