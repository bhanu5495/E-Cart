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

namespace Ecart_Shopping
{
	[Activity(Label = "cartdetails")]
	public class cartdetails : Activity
    {
        Button delete;
        Button Back;
        ProductDBHelper db;
        DBHelper db1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.cartdetails);
            string title = Intent.GetStringExtra("title1");
            string cost = Intent.GetStringExtra("cost1");
            string quantity = Intent.GetStringExtra("quantity1");
            string id = Intent.GetStringExtra("id2");
            // string image= Intent.GetStringExtra("image");






            TextView name = FindViewById<TextView>(Resource.Id.textview2);
            name.Text = title.ToString();
           Button delete = FindViewById<Button>(Resource.Id.delete);
           Button Back = FindViewById<Button>(Resource.Id.back);
            TextView cost1 = FindViewById<TextView>(Resource.Id.textview4);
            cost1.Text = cost.ToString();
            TextView quantity1 = FindViewById<TextView>(Resource.Id.textview6);
            quantity1.Text = quantity.ToString();
            
            
            //FindViewById<ImageView>(Resource.Id.image).SetImageResource(int.Parse(GetString(4)));
            // Create your application here
            db = new ProductDBHelper(this);
            db.Open();
            db1 = new DBHelper(this);
            db1.Open();
            delete.Click += delegate 
            {



                ContentValues values = new ContentValues();
                values.Put(ProductDBHelper.quantityField1, quantity + 1);
                db.WritableDatabase.Update("cartInfo", values, "_id=" + id + "", null);


                db.WritableDatabase.Delete("cartInfo", "_id=" + id + "", null);
                var text = "Deleted";
                Android.Widget.Toast.MakeText(this, text, Android.Widget.ToastLength.Short).Show();
                //  db.ReadableDatabase.RawQuery("delete table tablename where order_id=" + order_id + ";", null);
                //var intent = new Intent(this, typeof(IcActivity));
                //StartActivity(intent);

            };
            Back.Click += delegate
            {
                //db.Insertlistview(title, cost, quantity);
                var listOfUsers = db1.getUsers();
                while (listOfUsers.MoveToNext())
                {

                    var lName = listOfUsers.GetString(listOfUsers.GetColumnIndexOrThrow(DBHelper.lNameField));
                    var fName = listOfUsers.GetString(listOfUsers.GetColumnIndexOrThrow(DBHelper.fNameField));
                    var email = listOfUsers.GetString(listOfUsers.GetColumnIndexOrThrow(DBHelper.emailField));

                    var welcomeActivity = new Intent(this, typeof(IcActivity));
                    welcomeActivity.PutExtra("myUser", fName);
                    welcomeActivity.PutExtra("myfname", fName);
                    welcomeActivity.PutExtra("mylname", lName);
                    welcomeActivity.PutExtra("mymail", email);

                    StartActivity(welcomeActivity);

                }
            };


        }



    }
}