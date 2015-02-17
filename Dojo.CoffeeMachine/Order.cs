namespace Dojo.CoffeeMachine
{
    public class Order
    {
        public Order(Drink drink, double amount = 0)
        {
            Drink = drink;
            GivenAmount = amount;
        }

        public Drink Drink { get; protected set; }

        public int Sugars { get; set; }

        public double GivenAmount { get; set; }
    }

}