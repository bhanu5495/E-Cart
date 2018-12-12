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
    [Activity(Label = "Welcome")]
    public class Welcome : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Welcome);


            Button btn = FindViewById<Button>(Resource.Id.button1);

            string user = Intent.GetStringExtra("myUser");
            string text = Intent.GetStringExtra("text");

          //  btn.Text = "Welcome " + user.ToString();
        }
    }
}