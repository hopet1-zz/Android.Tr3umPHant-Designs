
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
	[Activity (Label = "FullImageFlyers", Icon = "@drawable/icon")]			
	public class FullImageFlyers : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.FullImage);

			String intent = Intent.GetStringExtra ("id");
			int position = Int32.Parse (intent);

			FlyersImageAdapter fia = new FlyersImageAdapter (this);
			ImageView imageView = (ImageView)FindViewById (Resource.Id.full_image_view);
			imageView.SetImageResource (fia.thumbIds[position]);

			String message = null;
			switch (position) {
				case 0:
					Title = "G.I.F.T.S.";
					message = "Created 02/18/15";
					break;
				case 1:
					Title = "March of Dimes Charity Basketball Game";
					message = "Created 01/12/15";
					break;
				default:
					break;
			}

			Toast.MakeText (this, message, ToastLength.Long).Show ();

		}
	}
}

