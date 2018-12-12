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

namespace Ecart_Shopping
{
    [Activity(Label = "Constants")]
    class Constants
    {
        // Colums
        public static string Row_id = "id";
        public static string NAME = "name";
        //DB properties
        public static string DB_NAME = "b_DB";
        public static string TB_NAME = "b_TB";
        public static int DB_VERSION = 1;
        //Create table
        public static string Create_TB = "CREATE TABLE b_TB(id INTEGER PRIMARY KEY AUTOINCREMENT," + "NAME TEXT NOT NULL);";
        // Drop table
        public static string DROP_TB = "DROP TABLE IF EXISTS" + TB_NAME;
    }
}