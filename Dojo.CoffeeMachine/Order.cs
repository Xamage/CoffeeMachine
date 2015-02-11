namespace Dojo.CoffeeMachine
{
    public class Order
    {
        public Order(Drink drink)
        {
            Drink = drink;
        }

        public Drink Drink { get; protected set; }

        public int Sugars { get; set; }
    }

    public class Order<T> : Order
        where T : Drink, new()
    {
        public Order() 
            : base(new T())
        {
        }

        public new T Drink
        {
            get { return base.Drink as T; }
            set { base.Drink  = value; }
        }
    }
}