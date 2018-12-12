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
    public class DBHelper : SQLiteOpenHelper
    {
        private const string dbName = "UserDB";
        private const int dbVersion = 1;
        private const string tbName = "DataInfo";
        private SQLiteDatabase obj;
        public const string idField = "_id";
        public const string fNameField = "FirstName";
        public const string lNameField = "LastName";
        public const string emailField = "Email";
        public const string tbCreate = "CREATE TABLE DataInfo" + "(_id integer primary key autoincrement,LastName varchar(100),FirstName varchar(100),Email varchar(100))";
        public DBHelper(Context context) : base(context, dbName, null, dbVersion)
        {



        }
        public void Open()
        {
            this.obj = this.WritableDatabase;
        }

        public void insertUsersInfo(string lname, string fname, string email)
        {

            var contentData = new ContentValues();
            contentData.Put(lNameField, lname);
            contentData.Put(fNameField, fname);
            contentData.Put(emailField, email);
            this.obj.Insert(tbName, null, contentData);

        }
        public ICursor getUsers()
        {
            this.obj = this.ReadableDatabase;
            var columnNames = new[] { idField, lNameField, fNameField, emailField };
            var userList = this.obj.Query(tbName, columnNames, null, null, null, null, null);

            return userList;

        }

        public override void OnCreate(SQLiteDatabase db)
        {


            db.ExecSQL(tbCreate);


        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS DataInfo");
            this.OnCreate(db);
        }




    }
}