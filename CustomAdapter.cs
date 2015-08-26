
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
	[Activity (Label = "CustomAdapter")]			
	public class CustomAdapter : ArrayAdapter<String>{
		private List<String> items;
		private Activity activity;

		public CustomAdapter(Activity activity, List<String> items) : base(activity, Android.Resource.Layout.SimpleSpinnerItem, items){
			this.activity = activity;
			this.items = items;
		}
			
		public override View GetDropDownView (int position, View convertView, ViewGroup parent){
			return base.GetDropDownView (position, convertView, parent);
		}

		public override View GetView (int position, View convertView, ViewGroup parent){
			View v = convertView;
			if (v == null) {
				LayoutInflater inflater = activity.LayoutInflater;
				v = inflater.Inflate (Resource.Drawable.Item, parent, false);
				v.SetTag(Resource.Id.text1, v.FindViewById(Resource.Id.text1));
			}
			TextView tv = (TextView) v.GetTag(Resource.Id.text1);
			tv.Text = items [position];
			return v;
		}


		public override int Count {
			get {
				return base.Count;
			}
		}




	}
}

