using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.CoffeeMachine
{
    public class CoffeeMachine
    {
        public CoffeeMachine(IDrinkMaker drinkMaker = null)
        {
            DrinkMaker = drinkMaker ?? new DrinkMaker();
            OrderTranslator = new OrderTranslator();
        }

        private IDrinkMaker DrinkMaker { get; set; }

        private OrderTranslator OrderTranslator { get; set; }

        public void Order(Order order)
        {
            DrinkMaker.Process(OrderTranslator.Translate(order));
        }
    }
}
