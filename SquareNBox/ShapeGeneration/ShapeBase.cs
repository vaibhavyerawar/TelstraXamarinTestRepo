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
		//Default value for border width.
		protected readonly float DEFAULT_BORDER_WIDTH = 2.0f;

		//Default value for padding to shape content area.
		protected readonly float DEFAULT_INSET_PADDING = 2.0f;

		//Defautl border color.
		private readonly Color DEFAULT_BORDER_COLOR = Color.Black;

		//Default shape background color.
		private readonly Color DEFAULT_SHAPE_BACKGROUND_COLOR = Color.White;

		//Graphics rect object for creating border bounds on canavas.
		protected RectF BorderRect;

		//Graphics rect object for creating bounds on canavas.
		protected readonly RectF DrawableRect = new RectF();

		//Graphics paint object for shape with bitmap.
		protected readonly Paint BitmapPaint = new Paint();

		//Graphics paint object for shape with color.
		protected readonly Paint ShapeBackgroundPaint = new Paint();

		//Graphics paint object for shape border color.
		protected readonly Paint BorderPaint = new Paint();

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
			ShapeBackgroundPaint.Color = DEFAULT_SHAPE_BACKGROUND_COLOR;
			ShapeBackgroundPaint.SetStyle(Paint.Style.Fill);
			ShapeBackgroundPaint.AntiAlias = true;

			BorderPaint.Color = DEFAULT_BORDER_COLOR;
			BorderPaint.SetStyle(Paint.Style.Stroke);
			BorderPaint.AntiAlias = true;
			BorderPaint.StrokeWidth = DEFAULT_BORDER_WIDTH;

            this.SetBackgroundColor(DEFAULT_SHAPE_BACKGROUND_COLOR);
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
		protected RectF CalculateBounds()
		{
			int availableWidth =  mWidth - PaddingLeft - PaddingRight;
			int availableHeight = mHeight - PaddingTop - PaddingBottom;

			int sideLength = Math.Min(availableWidth, availableHeight);

			float left = PaddingLeft + (availableWidth - sideLength) / DEFAULT_INSET_PADDING;
			float top = PaddingTop + (availableHeight - sideLength) / DEFAULT_INSET_PADDING;

			return new RectF(left, top, left + sideLength, top + sideLength);
		}
	}
}