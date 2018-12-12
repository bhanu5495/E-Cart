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
using Android.Database.Sqlite;

namespace Ecart_Shopping
{
    [Activity(Label = "register")]
    public class saveData : Activity
    {
        DBHelper myDatabase;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);

            var firstName = FindViewById<EditText>(Resource.Id.firstedit);
            var lastName = FindViewById<EditText>(Resource.Id.editLast);
            var email = FindViewById<EditText>(Resource.Id.editEmail);
            myDatabase = new DBHelper(this);
            myDatabase.Open();
            Button save = FindViewById<Button>(Resource.Id.button1);
            save.Click += delegate
            {


                // myDatabase.OnCreate(db);
                myDatabase.insertUsersInfo(lastName.Text, firstName.Text, email.Text);

                // myDatabase.OnUpgrade(database,1,2);
                //  myDatabase.Close();
                StartActivity(typeof(MainActivity));
            };

        }
    }
}