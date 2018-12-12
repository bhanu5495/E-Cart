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
    class Product
    {
        string name;
        string type;
        string cost;
        int image;
        int quantity;

        public Product(string name, string type, string cost, int image,int quantity)
        {
            this.name = name;
            this.type = type;
            this.cost = cost;
            this.image = image;
            this.quantity = quantity;
        }

        public string getName()
        {
            return name;
        }

        public string getType()
        {
            return type;
        }


        public string getcost()
        {
            return cost;
        }

        public int getImage()
        {
            return image;
        }

        public int getquantity()
        {
            return quantity;
        }
    }
}