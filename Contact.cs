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
	[Activity (Label = "Contact", Icon = "@drawable/icon")]			
	public class Contact : Activity{

		EditText firstName, lastName, phoneNumber, email, information;
		Button submit;

		protected override void OnCreate (Bundle bundle){
			
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Contact);

			//First Name
			firstName = FindViewById<EditText> (Resource.Id.firstName);

			//Last Name
			lastName = FindViewById<EditText> (Resource.Id.lastName);

			//Phone Number
			phoneNumber = FindViewById<EditText> (Resource.Id.phoneNumber);

			//E-Mail
			email = FindViewById<EditText> (Resource.Id.email);

			//Information
			information = FindViewById<EditText> (Resource.Id.information);

			//Submit Button
			submit = FindViewById<Button> (Resource.Id.submit_button);
			submit.Click += delegate {
				if (validSubmission (firstName.Text, lastName.Text, phoneNumber.Text, email.Text, information.Text)) {
					bool valid_email = isValidEmail(email.Text);
					bool valid_phone = isValidPhone(phoneNumber.Text);
					if(valid_email && valid_phone){
						//SendToPHP();
					}
				} else {
					showErrorAlert ();
				}
			};
		}

		//METHOD: Determine if any fields have been left blank.
		protected bool validSubmission(String firstName, String lastName, String phoneNumber, String email, String information){
			if (firstName == "" || lastName == "" || phoneNumber == "" || email == "" || information == "")
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
				DataObj.information = information.Text;

				String JSONString = JsonClass.JSONSerialize<data>(DataObj);

				String url = "http://www.tr3umphant-designs.com/contact.php";

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
				Console.WriteLine("--->" + _exception);
			}
		}


	}
}


