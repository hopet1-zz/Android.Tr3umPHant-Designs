using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Json;
using System.IO;
using System.Text.RegularExpressions;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;


namespace Tr3umPHantDesigns
{
	[Activity (Label = "Request", Icon = "@drawable/icon")]			
	public class Request : Activity{

		EditText firstName, lastName, phoneNumber, email, information;
		Button submit;
		Spinner designType;
		DateTime todays_date = DateTime.Today;
		TextView designDate;

		protected override void OnCreate (Bundle bundle){

			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Request);

			//First Name
			firstName = FindViewById<EditText> (Resource.Id.firstName);

			//Last Name
			lastName = FindViewById<EditText> (Resource.Id.lastName);

			//Phone Number
			phoneNumber = FindViewById<EditText> (Resource.Id.phoneNumber);

			//E-Mail
			email = FindViewById<EditText> (Resource.Id.email);

			//Type of Design
			designType = FindViewById<Spinner> (Resource.Id.spinner);
			List<String> choices = new List<String>();
			choices.Add ("Illustration - $35"); choices.Add ("Flyer - $30"); choices.Add ("Logo - $25");
			CustomAdapter adapter = new CustomAdapter (this, choices);
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			designType.Adapter = adapter;

			//Design Date
			designDate = FindViewById<TextView> (Resource.Id.designDate);
			designDate.Click += delegate {
				#pragma warning disable
				ShowDialog(0);
				#pragma warning restore
			};

			//Information
			information = FindViewById<EditText> (Resource.Id.information);

			//Submit Button
			submit = FindViewById<Button> (Resource.Id.submit_button);
			submit.Click += delegate {
				if(validSubmission(firstName.Text, lastName.Text, phoneNumber.Text, email.Text, designDate.Text, information.Text)){
					if(isValidEmail(email.Text) && isValidPhone(phoneNumber.Text)){
						SendToPHP();
					}
				}
				else{
					showErrorAlert();
				}
			};
		}

		//METHOD: Create dialog for Date Picker.
		#pragma warning disable
		protected override Dialog OnCreateDialog (int id){
		#pragma warning restore
			DatePickerDialog dpd = new DatePickerDialog (this, HandleDateSet, todays_date.Year,
				todays_date.Month - 1, todays_date.Day);
			dpd.SetMessage ("Requests issued within 2 days will add a $15.00 fee.");
			dpd.SetIcon (Resource.Drawable.ic_action_time);
			dpd.SetCanceledOnTouchOutside (false);
			dpd.SetCancelable (false);
			#pragma warning disable
			dpd.SetButton ("Cancel", dpd);
			dpd.SetButton2("Confirm", dpd);
			#pragma warning restore
			return dpd;
		}

		//METHOD: Handles date updates.
		void HandleDateSet (object sender, DatePickerDialog.DateSetEventArgs e){
			todays_date = e.Date;
			designDate.Text = todays_date.ToString ("D");
		}
			
		//METHOD: Determine if any fields have been left blank.
		protected bool validSubmission(String firstName, String lastName, String phoneNumber, String email, String designDate, String information){
			if (firstName == "" || lastName == "" || phoneNumber == "" || email == "" || designDate == "" || information == "")
				return false;
			return true;
		}

		//METHOD: Show successful sent contact form alert. 
		protected void showSuccessAlert(){
			new AlertDialog.Builder(this).SetTitle ("Sent :)").SetMessage ("I will respond shortly.").Show ();
		}

		//METHOD: Show unsuccessful sent contact form alert due to empty input fields. 
		protected void showErrorAlert(){
			new AlertDialog.Builder(this).SetTitle ("Sorry :(").SetMessage ("Some fields were left blank.").Show ();
		}

		//METHOD: Show unsuccessful sent contact form alert due to inconnectivity.
		protected void showWebErrorAlert(){
			new AlertDialog.Builder(this).SetTitle ("Error :(").SetMessage ("Could not connect to the server; try connecting to the wifi.").Show ();
		}

		//METHOD: Clear input fields on form. 
		protected void clearFields(){
			firstName.Text = lastName.Text = phoneNumber.Text = email.Text = information.Text = "";
		}

		//METHOD: Determines if email input given by user is valid or not. 
		public bool isValidEmail(String email) {
			if (!Android.Util.Patterns.EmailAddress.Matcher (email).Matches ()) {
				new AlertDialog.Builder(this).SetTitle("Error :(").SetMessage("Invalid E-Mail (format should be johndoe@xyz.com)").Show();
				return false;
			}
			return true;
		}

		//METHOD: Determines if phone number input given byuser is valid or not.
		public bool isValidPhone(String phone){
			if (!new Regex (@"^\d{10}$").Match (phone).Success) {
				new AlertDialog.Builder (this).SetTitle ("Error :(").SetMessage ("Invalid Phone-Number (format should be 8590034564)").Show ();
				return false;
			}
			return true;
		}

		//CLASS: JSON Data Class
		public class data{
			public String firstName{ get; set; }
			public String lastName{get; set;}
			public String phoneNumber{ get; set; }
			public String email{ get; set; }
			public String designType{ get; set; }
			public String designDate{ get; set; }
			public String information{ get; set; }
		}

		//METHOD: Sends user's input via JSON to web server, which is then emailed. 
		protected void SendToPHP(){
			ProgressDialog progress = new ProgressDialog (this);
			progress.SetMessage ("Sending, please wait.");
			progress.SetCancelable(false);
			progress.Indeterminate = true;
			progress.SetProgressStyle(ProgressDialogStyle.Spinner);
			progress.Show ();
			try{
				data DataObj = new data();
				DataObj.firstName = firstName.Text;
				DataObj.lastName = lastName.Text;
				DataObj.phoneNumber = phoneNumber.Text;
				DataObj.email = email.Text;
				DataObj.designType = designType.SelectedItem.ToString();
				DataObj.designDate = designDate.Text;
				DataObj.information = information.Text;

				String JSONString = JsonClass.JSONSerialize<data>(DataObj);

				String url = "http://www.tr3umphant-designs.com/request.php";

				HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
				myRequest.Method = "POST";
				String postData = JSONString;

				byte[] pdata = Encoding.UTF8.GetBytes(postData);

				myRequest.ContentType = "application/x-www-form-urlencoded";
				myRequest.ContentLength = pdata.Length;

				Stream mystream = myRequest.GetRequestStream();
				mystream.Write(pdata, 0, pdata.Length);

				/*
				WebResponse myResponse = myRequest.GetResponse();
				Stream responseStream = myResponse.GetResponseStream();
				StreamReader streamReader = new StreamReader(responseStream);

				string result = streamReader.ReadToEnd();
				Toast.MakeText(this, result,ToastLength.Long).Show();

				streamReader.Close ();
				responseStream.Close ();
				myResponse.Close ();
				mystream.Close ();
				*/
				progress.Dismiss();
				showSuccessAlert ();
				clearFields();
			}
			catch(WebException ex){
				showWebErrorAlert ();
				string _exception = ex.ToString();
				//Toast.MakeText(this, _exception, ToastLength.Long).Show();
				Console.WriteLine("--->" + _exception);
			}
		}
	}
}

