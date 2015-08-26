
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
	[Activity (Label = "Graphic", Icon = "@drawable/icon")]	
	#pragma warning disable
	public class Graphic : TabActivity
	#pragma warning restore
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Graphic);

			CreateTab(typeof(Flyers), "Flyers", "Flyers", Resource.Drawable.Icon);
			CreateTab(typeof(Logos), "Logos", "Logos", Resource.Drawable.Icon);
			CreateTab(typeof(Illustrations), "Illustrations", "Illustrations", Resource.Drawable.Icon);

		}
			
		private void CreateTab(Type activityType, string tag, string label, int drawableId )
		{
			var intent = new Intent(this, activityType);
			intent.AddFlags(ActivityFlags.NewTask);

			var spec = TabHost.NewTabSpec(tag);
			var drawableIcon = Resources.GetDrawable(drawableId);
			spec.SetIndicator(label, drawableIcon);
			spec.SetContent(intent);
			TabHost.AddTab(spec);
		}



	}
}

