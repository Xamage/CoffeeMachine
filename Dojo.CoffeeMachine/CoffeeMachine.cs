using System.Collections.Generic;
using System.Linq;

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
            OrderedDrinksHistory = new List<Drink>();
        }

        private IDrinkMaker DrinkMaker { get; set; }

        private OrderTranslator OrderTranslator { get; set; }

        public Queue<string> Messages { get; private set; }

        private List<Drink> OrderedDrinksHistory { get; set; }

        /// <summary>
        /// Lance le traitement d'une commande spécifiée
        /// </summary>
        /// <param name="order">La commande à traiter</param>
        public void Order(Order order)
        {
            if (order.GivenAmount < order.Drink.Price)
            {
                DrinkMaker.Process(string.Format("Missing {0:0.00} €", order.Drink.Price - order.GivenAmount));
                return;
            }

            DrinkMaker.Process(OrderTranslator.Translate(order));
            OrderedDrinksHistory.Add(order.Drink);
        }

        /// <summary>
        /// Fournit un rapport des commandes passées sur la machine
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetReport()
        {
            return OrderedDrinksHistory
                .GroupBy(d => d.Code)
                .Select(g => string.Format("{0}:{1}:{2:0.00}", g.Key, g.Count(), g.Sum(d => d.Price)));
        }

        private void DrinkMaker_OnSendMessage(object sender, MessageEventArgs e)
        {
            Messages.Enqueue(e.Message);
        }
    }
}