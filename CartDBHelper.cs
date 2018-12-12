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
    [Activity(Label = "CartDBHelper")]
    public class CartDBHelper : SQLiteOpenHelper
    {
        private const string DBName = "CartDB";
        private const int DatabaseVersion = 1;
        private const string TableName = "CartInfo";

        public const string idField = "_id";
        public const string titleField = "product_title";
        public const string typeField = "product_type";
       


        private SQLiteDatabase dbObj;

        private const string tableCreate = "CREATE TABLE " + TableName + "(_id INTEGER PRIMARY KEY," + "product_title VARCHAR(255), product_type VARCHAR(255));";
        public CartDBHelper(Context context) : base(context, DBName, null, DatabaseVersion)
        {

        }


        public void Open()
        {
            this.dbObj = this.WritableDatabase;



        }

        public void Insertlistview(string title, string type)
        {
            var Data = new ContentValues();
            Data.Put(titleField, title);
            Data.Put(typeField, type);

            this.dbObj.Insert(TableName, null, Data);

        }


        public ICursor getcartlist()
        {
            var columnName = new[] { idField, titleField, typeField };
            var cartList = this.dbObj.Query(TableName, columnName, null, null, null, null, null);
            return cartList;
        }


        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(tableCreate);

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXIST" + TableName);
            this.OnCreate(db);
        }
    }
}