using System;
using System.Collections.Generic;

namespace Dojo.CoffeeMachine
{
    public class OrderTranslator
    {
        private const string CommandTemplate = "{DrinkCode}:{Sugars}:{Stick}";

        //private readonly Dictionary<Type, string> _drinkCommandTemplates = new Dictionary<Type, string>
        //{
        //    { typeof(Chocolate), "H:{0}:{1}" },
        //    { typeof(Coffee), "C:{0}:{1}" },
        //    { typeof(Tea), "T:{0}:{1}" },
        //    { typeof(ExtraHotChocolate), "Hh:{0}:{1}"},
        //    { typeof(ExtraHotCoffee), "Ch:{0}:{1}" },
        //    { typeof(ExtraHotTea), "Th:{0}:{1}" },
        //    { typeof(OrangeJuice), "O:{0}:{1}" }
        //};

        public string Translate(Order order)
        {
            return CommandTemplate
                .Replace("{DrinkCode}", order.Drink.Code)
                .Replace("{Sugars}", order.Sugars > 0 ? order.Sugars.ToString() : "")
                .Replace("{Stick}", order.Sugars > 0 ? "0" : "");

            //string command;

            //if (_drinkCommandTemplates.TryGetValue(order.Drink.GetType(), out command))
            //{
            //    return string.Format(command, order.Sugars > 0 ? order.Sugars.ToString() : "", order.Sugars > 0 ? "0" : "");
            //}

            //throw new Exception("Boisson inconnue.");
        }
    }
}