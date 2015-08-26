
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
	[Activity (Label = "About Me", Icon = "@drawable/icon")]			
	public class About : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.About);
			// Create your application here
		}
	}
}

