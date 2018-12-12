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
using Android.Database;

namespace Ecart_Shopping
{
    [Activity(Label = "DBAdapter")]
    class DBAdapter
    {
        private Context c;
        private SQLiteDatabase db;
        private DB1Helper helper;

        public DBAdapter(Context c)
        {
            this.c = c;
            helper = new DB1Helper(c);
        }
        //open Connection
        public void OpenDB()
        {
            try
            {
                db = helper.WritableDatabase;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //close db
        public void CloseDB()
        {
            //try { helperClose(); }
            try
            {
                helper.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public bool Add(String name)
        {
            try
            {
                ContentValues cv = new ContentValues();
                cv.Put(Constants.NAME, name);
                db.Insert(Constants.TB_NAME, Constants.Row_id, cv);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        //select/ retive
        public ICursor Retrieve()
        {
            string[] columns = { Constants.Row_id, Constants.NAME };
            return db.Query(Constants.TB_NAME, columns, null, null, null, null, null);
        }


    }
}