using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.CoffeeMachine
{
    public class Order
    {
        public Drink Drink { get; set; }

        public int Sugars { get; set; }
    }

    public class Order<T> : Order
        where T : Drink, new()
    {
        public Order()
        {
            Drink = new T();
        }

        public new T Drink
        {
            get { return base.Drink as T; }
            set { base.Drink  = value; }
        }
    }
}