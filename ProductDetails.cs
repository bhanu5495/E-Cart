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
using Android.Database;

namespace Ecart_Shopping
{
    [Activity(Label = "ProductDetails")]
    public class ProductDetails : Activity
    {
        Button addtocart;
        Button Back;
        ProductDBHelper db;
        DBHelper db1;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProductDetails);
            string title1 = Intent.GetStringExtra("title1");
            string cost1 = Intent.GetStringExtra("cost1");
            string quantity1 = Intent.GetStringExtra("quantity1");
            string fquantity = "0";
            int id1 =int.Parse( Intent.GetStringExtra("id1"));
            // string image= Intent.GetStringExtra("image");
          
            addtocart = FindViewById<Button>(Resource.Id.add_to_cart);
            Back = FindViewById<Button>(Resource.Id.back);
            

            EditText quant = FindViewById<EditText>(Resource.Id.editText2);
            TextView name = FindViewById<TextView>(Resource.Id.textview2);
            name.Text = title1.ToString();
            TextView cost = FindViewById<TextView>(Resource.Id.textview4);
            cost.Text = cost1.ToString();
            TextView available = FindViewById<TextView>(Resource.Id.textview6);
            available.Text = quantity1.ToString();
            //FindViewById<ImageView>(Resource.Id.image).SetImageResource(int.Parse(GetString(4)));
            // Create your application here
            db = new ProductDBHelper(this);
            db.Open();
            db1 = new DBHelper(this);
            db1.Open();
           
            
            addtocart.Click += delegate
            {
                if (quant.Text == "")
                {
                    string text = "Please Enter Quantity Details";
                    Android.Widget.Toast.MakeText(this, text, Android.Widget.ToastLength.Short).Show();
                }
                else
                {
                    db.Insertlistview(title1, cost1, quant.Text);
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
                    var text = "Added to Cart";
                    Android.Widget.Toast.MakeText(this, text, Android.Widget.ToastLength.Short).Show();
                    //ContentValues values = new ContentValues();
                    //values.Put(ProductDBHelper.quantityField, quantity1 + 1);
                    //db.WritableDatabase.Update("cartInfo", values, "_id=" + id1 + "", null);

                    // db.ReadableDatabase.RawQuery("Delete table tablename where order_id=" + order_id + ";", null);
                    //var intent = new Intent(this, typeof(IcActivity));
                    //StartActivity(intent);
                }
            
            };
            Back.Click += delegate
            {
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