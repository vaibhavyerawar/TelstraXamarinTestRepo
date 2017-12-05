using Android.App;
using Android.Widget;
using Android.OS;
using ShapeGenerator;
using System;
using Android.Views;
using System.Threading.Tasks;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Android.Util;

namespace SquareNBox
{
	[Activity(Label = "SquareNBox", MainLauncher = true)]
	public class MainActivity : Activity, View.IOnTouchListener, View.IOnLongClickListener, View.IOnDragListener,
	View.IOnClickListener, ISensorEventListener
	{

		/// <summary>
		/// Threshold value for shake gravity.
		/// </summary>
		private readonly float SHAKE_THRESHOLD = 2.7F;

		/// <summary>
		/// Parent layout instance.
		/// </summary>
		private RelativeLayout parent;

		/// <summary>
		/// ShapeViewModel class instance.
		/// </summary>
		private ShapeViewModel shapeViewModel;

		/// <summary>
		/// Sensor manager instance.
		/// </summary>
		private SensorManager sensorManager;

		/// <summary>
		/// Accelerometer sensor reference.
		/// </summary>
		private Sensor accelerometerSensor;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			init();
		}

		/// <summary>
		/// Registering sensor event listener with sensor manager.
		/// </summary>
		protected override void OnResume()
		{
			base.OnResume();
			sensorManager.RegisterListener(this, accelerometerSensor, SensorDelay.Game);
		}

		/// <summary>
		/// Unregistering sensor event listener with sensor manager.
		/// </summary>
		protected override void OnPause()
		{
			base.OnPause();
			sensorManager.UnregisterListener(this);
		}

		/// <summary>
		/// Init this instance.
		/// </summary>
		private void init()
		{
			sensorManager = (SensorManager)GetSystemService(Context.SensorService);
			accelerometerSensor = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
			shapeViewModel = new ShapeViewModel(this);
			parent = FindViewById<RelativeLayout>(Resource.Id.parentMainLayout);
			parent.SetOnTouchListener(this);
		}

		/// <summary>
		/// Creates the and add new shape.
		/// </summary>
		/// <returns>The and add new shape.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private async Task createAndAddNewShape(float x, float y)
		{
			ShapeBase newShape = shapeViewModel.GenerateNewShape();
			newShape.SetOnClickListener(this);
			newShape.SetOnLongClickListener(this);
			newShape.RootView.SetOnDragListener(this);
			newShape.SetX(x);
			newShape.SetY(y);
			parent.AddView(newShape);

			await ChangeViewStyle(newShape);
		}

		/// <summary>
		/// Function to changes the view style.
		/// </summary>
		/// <returns>The view style.</returns>
		/// <param name="shape">Shape.</param>
		private async Task ChangeViewStyle(ShapeBase shape)
		{
			try
			{
				await shapeViewModel.DecorateShape(shape);
			}
			catch (ArgumentException argExcp)
			{
				Toast.MakeText(this, argExcp.Message, ToastLength.Long).Show();
			}
		}

		/// <summary>
		/// Set prerequisite for dragging to shape.
		/// </summary>
		/// <param name="shapeToDrag">Shape to drag.</param>
		private void SetDragToShape(ShapeBase shapeToDrag)
		{
			var clipData = ClipData.NewPlainText(String.Empty, String.Empty);
			View.DragShadowBuilder viewShadowOnDragging = new View.DragShadowBuilder(shapeToDrag);
			shapeToDrag.StartDrag(clipData, viewShadowOnDragging, shapeToDrag, 0);
		}

		/// <summary>
		/// Funtion to remove all shapes from screen on phone shaking.
		/// </summary>
		/// <param name="sensorEvent">Sensor event.</param>
		private void ClearScreen(SensorEvent sensorEvent)
		{
			if (parent.ChildCount > 0)
			{
				float x = sensorEvent.Values[0];
				float y = sensorEvent.Values[1];
				float z = sensorEvent.Values[2];

				float gX = x / SensorManager.GravityEarth;
				float gY = y / SensorManager.GravityEarth;
				float gZ = z / SensorManager.GravityEarth;

				float gForce = FloatMath.Sqrt(gX * gX + gY * gY + gZ * gZ);

				if (gForce > SHAKE_THRESHOLD)
				{
					parent.RemoveAllViews();
				}
			}
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			if (e.Action == MotionEventActions.Down)
			{
				createAndAddNewShape(e.GetX(), e.GetY());
				return true;
			}

			return false;
		}

		public bool OnLongClick(View v)
		{
			SetDragToShape((ShapeBase)v);
			return true;
		}

		public bool OnDrag(View v, DragEvent e)
		{
			switch (e.Action)
			{
				case DragAction.Drop:
					View view = (View)e.LocalState;
					view.SetX(e.GetX());
					view.SetY(e.GetY());
					break;

				default:
					break;
			}

			return true;
		}

		public void OnClick(View v)
		{
			ChangeViewStyle((ShapeBase)v);
		}

		public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
		{
		}

		public void OnSensorChanged(SensorEvent e)
		{
			ClearScreen(e);
		}
	}
}