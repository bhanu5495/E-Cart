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
    [Activity(Label = "IcActivity")]
    public class IcActivity : Activity
    {
        ICursor cursor=null;
        ProductDBHelper db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Ictab);

            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            CustomLayoutAdaptor adapter = new CustomLayoutAdaptor(this, cursor);
            //ProductDBHelper db = new ProductDBHelper(this);
            //db.Open();
            AddTab("Profile", Resource.Drawable.ic_tab_white, new SampleTabFragment(this));
            AddTab("Product", Resource.Drawable.ic_tab_white, new SampleTabFragment2(this,adapter,this));
            AddTab("Cart List", Resource.Drawable.ic_tab_white, new SampleTabFragment3(this));

            if (bundle != null)

                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));


        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("tab", this.ActionBar.SelectedNavigationIndex);

            base.OnSaveInstanceState(outState);
        }

        void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();
            tab.SetText(tabText);
            tab.SetIcon(Resource.Drawable.ic_tab_white);

            // must set event handler before adding tab
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.frameLayout1);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.frameLayout1, view);
            };
            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e) {
               e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }
        protected override void OnDestroy()
        {
            if (cursor != null)
            {
                //StopManagingCursor(cursor);
                cursor.Close();
            }
            base.OnDestroy();
        }

      public  class SampleTabFragment : Fragment
        {
            Activity context;
           
            public SampleTabFragment(Activity context)
            {
                this.context = context;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.Ictab2, container, false);
                Button btn = view.FindViewById<Button>(Resource.Id.button1);
                string user = Activity.Intent.GetStringExtra("myUser");
               // btn.Text = "Welcome " + user.ToString();

                var img = view.FindViewById<ImageView>(Resource.Id.imageView1);
                img.SetImageResource(Resource.Drawable.profile);

                string firstname = Activity.Intent.GetStringExtra("myfname");
                string lastname = Activity.Intent.GetStringExtra("mylname");
                string email = Activity.Intent.GetStringExtra("mymail");
                view.FindViewById<TextView>(Resource.Id.textview2).Text = firstname;
                view.FindViewById<TextView>(Resource.Id.textview4).Text = lastname;
                view.FindViewById<TextView>(Resource.Id.textview6).Text = email;

                //var welcomeActivity = new Intent(context, typeof(ProductDetails));
                //welcomeActivity.PutExtra("IcUser", firstname);
                //welcomeActivity.PutExtra("Icfname", firstname);
                //welcomeActivity.PutExtra("Iclname", lastname);
                //welcomeActivity.PutExtra("Icmail", email);

                //StartActivity(welcomeActivity);

                Button button = view.FindViewById<Button>(Resource.Id.Signout);
                button.Click += delegate
                {
                    var activity2 = new Intent(context, typeof(MainActivity));
                    StartActivity(activity2);
                
                };

                return view;
            }

        }

       public class SampleTabFragment2 : Fragment
        {
            ListView listView;
            ProductDBHelper db;
            DBHelper db1;
            ICursor cursor;
            Activity context;
            SearchView searchView;
            CustomLayoutAdaptor adapter;
            private IcActivity icActivity;
            //public SampleTabFragment2(IcActivity icActivity,CustomLayoutAdaptor adapter,ProductDBHelper db)
            public SampleTabFragment2(IcActivity icActivity, CustomLayoutAdaptor adapter, Activity context)
            {
                this.icActivity = icActivity;
                this.adapter = adapter;
                //this.db = db;
                this.context = context;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.CustomView, container, false);

                searchView = view.FindViewById<SearchView>(Resource.Id.searchView1);
                

                db = new ProductDBHelper(context);
                db.Open();

                db1 = new DBHelper(context);
                db1.Open();
                //db.InsertMovieInfo("Alladin", "Disney", "PG", Resource.Drawable.Alladin);
               // db.InsertMovieInfo("Boxtroll", "Disney", "PG", Resource.Drawable.Boxtroll);
               // db.InsertMovieInfo("goodDianosour", "Disney", "PG", Resource.Drawable.gooddianosour);
               // db.InsertMovieInfo("Zutopia", "Disney", "PG", Resource.Drawable.zutopia);
                listView = view.FindViewById<ListView>(Resource.Id.list);

                cursor = db.WritableDatabase.RawQuery("SELECT * FROM productInfo", null);
                context.StartManagingCursor(cursor);
                adapter= new CustomLayoutAdaptor(context, cursor);
                listView.Adapter = adapter;
                searchView.QueryTextChange += (sender, e) => adapter.Filter.InvokeFilter(e.NewText);
                searchView.QueryTextSubmit += (sender, e) =>
                {
                    Toast.MakeText(icActivity, "Searched for : " + e.Query, ToastLength.Short).Show();
                    e.Handled = true;
                };
                listView.ItemClick += OnListItemClick;
                db.Close();

                return view;
            }
            
            protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
            {
                    var obj = listView.Adapter.GetItem(e.Position);
                var curs = (ICursor)obj;
                var id = curs.GetString(0);
                var title = curs.GetString(1);
                var cost = curs.GetString(3);
                var quantity = curs.GetString(4);
               // var image = curs.GetString(4);
                var welcomeActivity = new Intent(context, typeof(ProductDetails));
               welcomeActivity.PutExtra("title1", title);
               welcomeActivity.PutExtra("cost1", cost);
               welcomeActivity.PutExtra("quantity1", quantity);
                welcomeActivity.PutExtra("id1", id);
                

                // welcomeActivity.PutExtra("image", image);
                StartActivity(welcomeActivity);
               // Android.Widget.Toast.MakeText(context, text, Android.Widget.ToastLength.Short).Show();
               // System.Console.WriteLine("Clicked on" + text);
            }

        }
       public class SampleTabFragment3 : Fragment
        {
            ListView listView;
            ProductDBHelper db;
            ICursor cursor;
            Activity context;
            public SampleTabFragment3(Activity context)
            {
                this.context = context;
            }
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                base.OnCreateView(inflater, container, savedInstanceState);

                var view = inflater.Inflate(Resource.Layout.cart_list, container, false);
                db = new ProductDBHelper(context);
                db.Open();
                listView = view.FindViewById<ListView>(Resource.Id.product_list);


                cursor = db.WritableDatabase.RawQuery("SELECT * FROM CartInfo", null);
                context.StartManagingCursor(cursor);

                listView.Adapter = new Customcartview(context, cursor);
                listView.ItemClick += OnListItemClick;
                db.Close();

                return view;
            }
            protected void OnListItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
            {
                var obj = listView.Adapter.GetItem(e.Position);
                var curs = (ICursor)obj;
                var id = curs.GetString(0);
                var title = curs.GetString(1);
                var cost = curs.GetString(2);
                var quantity = curs.GetString(3);
                // var image = curs.GetString(4);
                var welcomeActivity = new Intent(context, typeof(cartdetails));
                welcomeActivity.PutExtra("title1", title);
                welcomeActivity.PutExtra("cost1", cost);
                welcomeActivity.PutExtra("quantity1", quantity);
                welcomeActivity.PutExtra("id2", id);


                // welcomeActivity.PutExtra("image", image);
                StartActivity(welcomeActivity);
                // Android.Widget.Toast.MakeText(context, text, Android.Widget.ToastLength.Short).Show();
                // System.Console.WriteLine("Clicked on" + text);
            }
            public void StartNewActivity(object sender, EventArgs e)
            {
                Intent intent = new Intent(this.Activity, typeof(IcActivity));
                StartActivity(intent);
            }
        }
    }
}