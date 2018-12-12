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
    
    [Activity(Label = "customcartview")]
    public class Customcartview : CursorAdapter
    {
        Activity context;
        ProductDBHelper db;
       

        public Customcartview(Activity context, ICursor c) : base(context, c)
        { 
            db = new ProductDBHelper(context);
            db.Open();
            this.context = context;
        }
        public override void BindView(View view, Context context, ICursor cursor)
        {
            //var id = cursor.GetString(0);
             view.FindViewById<TextView>(Resource.Id.productName).Text = cursor.GetString(1);
             view.FindViewById<TextView>(Resource.Id.productcost).Text = cursor.GetString(2);
            view.FindViewById<TextView>(Resource.Id.productquantity1).Text = cursor.GetString(3);
            //Button b= view.FindViewById<Button>(Resource.Id.delete);

            // b.Click += delegate
            // {
            //     //var activity2 = new Intent(context, typeof(MainActivity));
            //    //StartActivity(activity2);
            //     //db.ReadableDatabase.RawQuery("Delete from CartInfo where _id= "+,null);

            // };


        } 
       //public override view getview()
      
        public override View NewView(Context context, ICursor cursor, ViewGroup
        parent)
        {
            return this.context.LayoutInflater.Inflate(Resource.Layout.listLayout1, parent, false);
        }
    }
}