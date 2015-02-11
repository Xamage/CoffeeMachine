using System.Collections.Generic;

namespace Dojo.CoffeeMachine
{
    public class CoffeeMachine
    {
        public CoffeeMachine(IDrinkMaker drinkMaker = null)
        {
            DrinkMaker = drinkMaker ?? new DrinkMaker();
            DrinkMaker.OnSendMessage += DrinkMaker_OnSendMessage;
            OrderTranslator = new OrderTranslator();
            Messages = new Queue<string>();
        }

        private IDrinkMaker DrinkMaker { get; set; }

        private OrderTranslator OrderTranslator { get; set; }

        public Queue<string> Messages { get; private set; }

        public void Order(Order order)
        {
            DrinkMaker.Process(OrderTranslator.Translate(order));
        }

        private void DrinkMaker_OnSendMessage(object sender, MessageEventArgs e)
        {
            Messages.Enqueue(e.Message);
        }
    }
}