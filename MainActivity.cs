using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Tr3umPHantDesigns
{
	[Activity (Label = "Tr3umPHant-Designs", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			/*Get width and height of screen.
			var metrics = Resources.DisplayMetrics;
			var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
			var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);*/

			/*Display width and heights of screen.
			AlertDialog.Builder alert = new AlertDialog.Builder(this);
			alert.SetMessage ("Width: " + widthInDp + "\nHeight: " + heightInDp);
			alert.Show ();*/


			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//About Button
			Button about_button = FindViewById<Button> (Resource.Id.about_button);
			about_button.Click += delegate {
				var intent = new Intent(this, typeof(About));
				StartActivity(intent);
			};

			//Graphic Work Button
			Button graphic_button = FindViewById<Button> (Resource.Id.graphic_button);
			graphic_button.Click += delegate {
				var intent = new Intent(this, typeof(Graphic));
				StartActivity(intent);
			};

			//Requests Button
			Button request_button = FindViewById<Button> (Resource.Id.request_button);
			request_button.Click += delegate {
				var intent = new Intent(this, typeof(Request));
				StartActivity(intent);
			};


			//Contact Button
			Button contact_button = FindViewById<Button> (Resource.Id.contact_button);
			contact_button.Click += delegate {
				var intent = new Intent(this, typeof(Contact));
				StartActivity(intent);
			};
		}

		private int ConvertPixelsToDp(float pixelValue){
			var dp = (int) ((pixelValue)/Resources.DisplayMetrics.Density);
			return dp;
		}
			
	}
}


