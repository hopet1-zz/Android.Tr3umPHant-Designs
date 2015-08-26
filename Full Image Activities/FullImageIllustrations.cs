
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
	[Activity (Label = "FullImageIllustrations", Icon = "@drawable/icon")]			
	public class FullImageIllustrations : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.FullImage);

			String intent = Intent.GetStringExtra ("id");
			int position = Int32.Parse (intent);

			IllustrationsImageAdapter iia = new IllustrationsImageAdapter (this);
			ImageView imageView = (ImageView)FindViewById (Resource.Id.full_image_view);
			imageView.SetImageResource (iia.thumbIds[position]);

			String message = null;
			switch (position) {
			case 0:
				Title = "AriZona Designs";
				message = "Created 04/21/15";
				break;
			default:
				break;
			}

			Toast.MakeText (this, message, ToastLength.Long).Show ();

		}
	}
}

