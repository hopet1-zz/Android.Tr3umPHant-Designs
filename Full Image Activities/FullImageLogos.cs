
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Tr3umPHantDesigns
{
	[Activity (Label = "FullImageLogos", Icon = "@drawable/icon")]			
	public class FullImageLogos : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.FullImage);

			String intent = Intent.GetStringExtra ("id");
			int position = Int32.Parse (intent);

			LogosImageAdapter lia = new LogosImageAdapter (this);
			ImageView imageView = (ImageView)FindViewById (Resource.Id.full_image_view);
			imageView.SetImageResource (lia.thumbIds[position]);

			String message = null;
			switch (position) {
			case 0:
				Title = "Bionic Innovations I";
				message = "Created 04/19/15";
				break;
			default:
				break;
			}

			Toast.MakeText (this, message, ToastLength.Long).Show ();

		}
	}
}

