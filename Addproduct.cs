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
    [Activity(Label = "Addproduct")]
    public class Addproduct : Activity
    {
        ProductDBHelper db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Addproduct);

            var prodName = FindViewById<EditText>(Resource.Id.firstedit);
            var prodcost = FindViewById<EditText>(Resource.Id.thirdedit);
            var prodqunt = FindViewById<EditText>(Resource.Id.fourthedit);
            

            string title1 = Intent.GetStringExtra("title1");

            TextView name = FindViewById<TextView>(Resource.Id.textview6);
            name.Text = title1.ToString();

            db = new ProductDBHelper(this);
            db.Open();
            Button addcat = FindViewById<Button>(Resource.Id.Addcat);
            Button back = FindViewById<Button>(Resource.Id.back);
            Button save = FindViewById<Button>(Resource.Id.Add);
            addcat.Click += delegate
            {
                StartActivity(typeof(categeory));
            };
            save.Click += delegate
            {
                if (prodName.Text == "")
                {
                    string title = "Enter Product Name";
                    Android.Widget.Toast.MakeText(this, title, Android.Widget.ToastLength.Short).Show();
                }
                else if (prodcost.Text == "")
                {
                    string title = "Enter Product cost";
                    Android.Widget.Toast.MakeText(this, title, Android.Widget.ToastLength.Short).Show();
                }
                else if (prodqunt.Text == "")
                {
                    string title = "Enter Product quantity";
                    Android.Widget.Toast.MakeText(this, title, Android.Widget.ToastLength.Short).Show();
                }
                else
                {

                    db.InsertProductInfo(prodName.Text, title1, prodcost.Text, prodqunt.Text);
                    //  db.Close();
                    var text = "success";
                    Android.Widget.Toast.MakeText(this, text, Android.Widget.ToastLength.Short).Show();
                }
            };
            back.Click += delegate
            {
                StartActivity(typeof(MainActivity));
            };
            
        }

        
    }
}