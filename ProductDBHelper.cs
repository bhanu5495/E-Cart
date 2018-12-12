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
    public class ProductDBHelper : SQLiteOpenHelper
    {
        private const string DBName = "productDB";
        private const int DatabaseVersion = 1;
        private const string TableName = "productInfo";
        private const string TableName1 = "CartInfo";


        public const string idField = "_id";
        public const string idField1 = "prod_id";
        public const string titleField = "product_title";
        public const string typeField = "product_type";
        public const string costField = "product_cost";
        public const string quantityField = "product_quantity";
        public const string quantityField1 = "product_quantity";


        private SQLiteDatabase dbObj;
       

        private const string tableCreate = "CREATE TABLE " + TableName + "(_id INTEGER PRIMARY KEY," + "product_title VARCHAR(255), product_type VARCHAR(255), product_cost VARCHAR(255),product_quantity VARCHAR(255));";
        private const string tableCreate1 = "CREATE TABLE " + TableName1 + "(_id INTEGER PRIMARY KEY," + "product_title VARCHAR(255), product_cost VARCHAR(255),product_quantity VARCHAR(255));";
        public ProductDBHelper(Context context) : base(context, DBName, null, DatabaseVersion)
        {

        }


        public void Open()
        {
            this.dbObj = this.WritableDatabase;
            this.dbObj = this.ReadableDatabase;
        


        }

        public void InsertProductInfo(string title, string type, string cost,string quantity)
        {
            var contentData = new ContentValues();
            contentData.Put(titleField, title);
            contentData.Put(typeField, type);
            contentData.Put(costField, cost);
            contentData.Put(quantityField, quantity);

            this.dbObj.Insert(TableName, null, contentData);
        }
        public void Insertlistview(string title, string cost, string quantity)
        {
            var Data = new ContentValues();
            Data.Put(titleField, title);
            Data.Put(costField, cost);
            Data.Put(quantityField, quantity);
            this.dbObj.Insert(TableName1, null, Data);

        }

        public ICursor getUsers()
        {
            var columnNames = new[] { idField, titleField, typeField, costField,quantityField};
            var productList = this.dbObj.Query(TableName, columnNames, null, null, null, null, null);
            return productList;
        }
        public ICursor getcartlist()
        {
            var columnName = new[] { idField, titleField, costField, quantityField };
            var cartList = this.dbObj.Query(TableName1, columnName, null, null, null, null, null);
            return cartList;
        }


        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(tableCreate);
            db.ExecSQL(tableCreate1);
            
        }
       
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXIST" + TableName);
            this.OnCreate(db);
        }
    }
}