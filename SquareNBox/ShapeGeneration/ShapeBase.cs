using System;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;

namespace ShapeGenerator
{
	/**
	 *  Base class for generating shapes and initializing basic properties. 
	 **/
	public abstract class ShapeBase : ImageView
	{
		//Graphics paint object for shape with bitmap.
		protected readonly Paint BitmapPaint = new Paint();

		//Graphics paint object for shape with color.
		protected readonly Paint ShapeBackgroundPaint = new Paint();

		//Bitmap image for shape.
		protected Bitmap BitmapImage;

		//Shape width.
		protected int mWidth;

		//Shape height.
		protected int mHeight;

		public ShapeBase(Context context) : base(context)
		{
			init();
		}

		public ShapeBase(Context context, IAttributeSet attrs) : base(context, attrs, 0)
		{
			init();
		}

		public ShapeBase(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
		{
			init();
		}

		/**
		 * Function to initialized shape properties.
		 **/
		private void init()
		{
			ShapeBackgroundPaint.Color = Color.Red;
			ShapeBackgroundPaint.SetStyle(Paint.Style.Fill);
			ShapeBackgroundPaint.AntiAlias = true;

			this.SetBackgroundColor(Color.Transparent);
		}


		/**
		 *  Function to set shape background color.
		 **/
		public void setShapeBackgroundColor(Color shapeBackgroundColor)
		{
			ShapeBackgroundPaint.Color = shapeBackgroundColor;
			Invalidate();
		}

		/**
		 * Function to set shape diamentions.
		 **/
		public void SetShapeDiamentions(int width, int height)
		{
			mWidth = width;
			mHeight = height;
            this.LayoutParameters = new LinearLayout.LayoutParams(width, height);
		}

		/**
		 * Function to set bitmap image to shape.
		 **/
		public override void SetImageBitmap(Bitmap bm)
		{
			BitmapImage = bm;
            InitBitmapPaint();
		}

		/**
		 *  Function to initialized grphics paint to show bitmap on shape.
		 **/
		private void InitBitmapPaint()
		{
			if (BitmapImage == null)
			{
				Invalidate();
				return;
			}

			BitmapShader BitmapImageShader = new BitmapShader(
							BitmapImage, Shader.TileMode.Clamp, Shader.TileMode.Clamp);

			BitmapPaint.AntiAlias = true;
			BitmapPaint.SetShader(BitmapImageShader);
			BitmapPaint.Color = Color.Red;

			mWidth = BitmapImage.Height;
			mHeight = BitmapImage.Width;

			Invalidate();
		}

		/**
		 * Function to calculate bounds for shape rect.
		 **/
		protected RectF calculateBounds()
		{
			int availableWidth =  mWidth - PaddingLeft - PaddingRight;
			int availableHeight = mHeight - PaddingTop - PaddingBottom;

			int sideLength = Math.Min(availableWidth, availableHeight);

			float left = PaddingLeft + (availableWidth - sideLength) / 2f;
			float top = PaddingTop + (availableHeight - sideLength) / 2f;

			return new RectF(left, top, left + sideLength, top + sideLength);
		}
	}
}