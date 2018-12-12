using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Ecart_Shopping
{
    [Activity(Label = "Ecart_Shopping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        DBHelper myDatabase;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button1 = FindViewById<Button>(Resource.Id.login);
            Button button2 = FindViewById<Button>(Resource.Id.register);
            EditText username = FindViewById<EditText>(Resource.Id.editText1);
            EditText password = FindViewById<EditText>(Resource.Id.editText2);
            ImageView image = FindViewById<ImageView>(Resource.Id.imageView1);
            myDatabase = new DBHelper(this);
            myDatabase.Open();

            button1.Click += delegate {

                var listOfUsers = myDatabase.getUsers();
                while (listOfUsers.MoveToNext())
                {

                    var lName = listOfUsers.GetString(listOfUsers.GetColumnIndexOrThrow(DBHelper.lNameField));
                    var fName = listOfUsers.GetString(listOfUsers.GetColumnIndexOrThrow(DBHelper.fNameField));
                    var email = listOfUsers.GetString(listOfUsers.GetColumnIndexOrThrow(DBHelper.emailField));
                    var Admuser = "Admin";
                    var Admpass = "pass";
                    var Admlname = "Admin";
                    var Admfname = "Admin";
                    var Admemail = "Admin@ecart.com";


                    TextView t = FindViewById<TextView>(Resource.Id.Errlog);
                    if ((username.Text).Equals(Admuser) && (password.Text).Equals(Admpass))
                    {
                        t.Text = "";
                        var welcomeActivity = new Intent(this, typeof(categeory));
                        welcomeActivity.PutExtra("myUser", Admuser);
                        welcomeActivity.PutExtra("myfname", Admfname);
                        welcomeActivity.PutExtra("mylname", Admlname);
                        welcomeActivity.PutExtra("mymail", Admemail);

                        StartActivity(welcomeActivity);
                    }
                    else if ((username.Text).Equals(fName) && (password.Text).Equals(lName))
                    {
                        t.Text = "";
                       
                            var welcomeActivity = new Intent(this, typeof(IcActivity));
                            welcomeActivity.PutExtra("myUser", fName);
                            welcomeActivity.PutExtra("myfname", fName);
                            welcomeActivity.PutExtra("mylname", lName);
                            welcomeActivity.PutExtra("mymail", email);

                            StartActivity(welcomeActivity);
                        
                    }
                    else
                    {
                        if ((username.Text == "") && (password.Text == ""))
                        {
                            var text = "Enter Username & password Details";
                            Android.Widget.Toast.MakeText(this, text, Android.Widget.ToastLength.Short).Show();
                        }
                        else
                        {

                            t.Text = "INVALID CREDENTIALS";
                            
                        }

                    }
                }





            };




            button2.Click += delegate {
                StartActivity(typeof(saveData));
            };
        }
    }
}

