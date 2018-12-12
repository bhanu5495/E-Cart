using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Database;

namespace Ecart_Shopping
{
    [Activity(Label = "categeory")]
    public class categeory : Activity
    {
        private Spinner sp;
        private EditText nameEditText;
        private Button saveBtn, retrieveBtn;
        JavaList<string> hk = new JavaList<string>();
        private ArrayAdapter adapter;
        string cat;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.categeory);
            this.InitializeUI();
            //adapter
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, hk);
            sp.Adapter = adapter;
            saveBtn.Click += saveBtn_click;
            retrieveBtn.Click += retrieveBtn_click;
            sp.ItemSelected += sp_itemSelected;
        }

        void retrieveBtn_click(object sender, EventArgs e)
        {
            var welcomeActivity = new Intent(this, typeof(Addproduct));
            welcomeActivity.PutExtra("title1", cat);
            StartActivity(welcomeActivity);
        }
        void sp_itemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            
            cat = hk[e.Position];
            //Retrieve();
            Toast.MakeText(this, hk[e.Position], ToastLength.Short).Show();
        }
        void saveBtn_click(object sender, EventArgs e)
        {
            Save(nameEditText.Text);
        }


        private void InitializeUI()
        {
            sp = FindViewById<Spinner>(Resource.Id.sp);
            nameEditText = FindViewById<EditText>(Resource.Id.nmeEditTxt);
            saveBtn = FindViewById<Button>(Resource.Id.saveBtn);
            retrieveBtn = FindViewById<Button>(Resource.Id.retriveBtn);
            Retrieve();

        }
        // save
        private void Save(String name)
        {
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            bool saved = db.Add(name);
            db.CloseDB();
            if (saved)
            {
                nameEditText.Text = "";
            }
            else
            {
                Toast.MakeText(this, "Unable to Save", ToastLength.Short).Show();
            }
            //refresh
            this.Retrieve();

        }
        //retive
        private void Retrieve()
        {
            hk.Clear();
            DBAdapter db = new DBAdapter(this);
            db.OpenDB();
            ICursor c = db.Retrieve();
            if (c != null)
            {
                while (c.MoveToNext())
                {
                    int id = c.GetInt(0);
                    string name = c.GetString(1);
                    hk.Add(name);
                }
            }
            else
            {
                Console.Write("Null Retrieved");
            }
            db.CloseDB();
            sp.Adapter = adapter;

        }


    }
}