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
    public class CustomLayoutAdaptor : CursorAdapter,IFilterable
    {
        Activity context;
        ICursor cursor;
        ProductDBHelper DB;

        public CustomLayoutAdaptor(Activity context, ICursor cursor) : base(context, cursor)
        {
            this.context = context;
            this.cursor = cursor;
            this.DB = new ProductDBHelper(context);
            Filter = new ProductFilter(this, DB);

        }
        public override void BindView(View view, Context context, ICursor cursor)
        {
            view.FindViewById<TextView>(Resource.Id.name).Text = cursor.GetString(1);
            view.FindViewById<TextView>(Resource.Id.cost).Text = cursor.GetString(3);
            view.FindViewById<TextView>(Resource.Id.quant1).Text = cursor.GetString(2);

            //view.FindViewById<ImageView>(Resource.Id.image).SetImageResource(int.Parse(cursor.GetString(4)));

           
        }
        public override View NewView(Context context, ICursor cursor, ViewGroup
        parent)
        {
            return this.context.LayoutInflater.Inflate(Resource.Layout.listLayout, parent, false);
            
        }
        public Filter Filter { get; private set; }

        public override void NotifyDataSetChanged()
        {
            base.NotifyDataSetChanged();
        }

        private class ProductFilter : Filter
        {
            private readonly CustomLayoutAdaptor adapter;
            private ProductDBHelper DB;

            public ProductFilter(CustomLayoutAdaptor adapter, ProductDBHelper DB)
            {
                this.adapter = adapter;
                this.DB = DB;
            }

            protected override FilterResults PerformFiltering
            (Java.Lang.ICharSequence constraint)
            {
                DB.Open();
                var returnObj = new FilterResults();
                ICursor newCursor =
                  DB.ReadableDatabase.RawQuery
                    ("select * from productInfo where product_title like :constrStr ", new string[] { "%" + constraint.ToString() + "%" });
                returnObj.Values = (Java.Lang.Object)newCursor;
                returnObj.Count = newCursor.Count;
                return returnObj;
            }

            protected override void PublishResults(Java.Lang.ICharSequence constraint, FilterResults results)
            {
                adapter.cursor = (ICursor)results.Values;
                adapter.ChangeCursor((ICursor)results.Values);
                //adapter.NotifyDataSetChanged ();

                constraint.Dispose();
                results.Dispose();
            }
        }
    }
}





