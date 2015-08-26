
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
	[Activity (Label = "Logos")]			
	public class Logos : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.GridViewLayout);

			var gridview = FindViewById<GridView> (Resource.Id.gridview);
			gridview.Adapter = new LogosImageAdapter (this);

			gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
				var intent = new Intent(this, typeof(FullImageLogos));
				intent.PutExtra("id", args.Position.ToString());
				StartActivity(intent); 
			};
		}
	}
}

