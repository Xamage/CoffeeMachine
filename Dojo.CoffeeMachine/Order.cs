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

    public class Order<T> : Order
        where T : Drink, new()
    {
        public Order(double amount = 0) 
            : base(new T(), amount)
        {
        }

        public new T Drink
        {
            get { return base.Drink as T; }
            set { base.Drink  = value; }
        }
    }
}