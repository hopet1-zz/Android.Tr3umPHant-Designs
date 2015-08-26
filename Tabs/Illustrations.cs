
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
	[Activity (Label = "Illustrations")]			
	public class Illustrations : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.GridViewLayout);

			var gridview = FindViewById<GridView> (Resource.Id.gridview);
			gridview.Adapter = new IllustrationsImageAdapter (this);

			gridview.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
				var intent = new Intent(this, typeof(FullImageIllustrations));
				intent.PutExtra("id", args.Position.ToString());
				StartActivity(intent);
			};
		}
	}
}

