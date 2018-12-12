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
using Android.Database.Sqlite;

namespace Ecart_Shopping
{
    [Activity(Label = "DB1Helper")]
    public class DB1Helper : SQLiteOpenHelper
    {
        public DB1Helper(Context context) : base(context, Constants.DB_NAME, null, Constants.DB_VERSION)
        {
        }
        public override void OnCreate(SQLiteDatabase db)
        {
            try { db.ExecSQL(Constants.Create_TB); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(Constants.DROP_TB);
            OnCreate(db);
        }
    }
}