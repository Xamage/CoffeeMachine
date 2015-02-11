namespace Dojo.CoffeeMachine
{
    public abstract class Drink
    {
        public abstract string Code { get; }
        public abstract double Price { get; }
    }

    public class Coffee : Drink
    {
        public override string Code
        {
            get { return "C"; }
        }

        public override double Price
        {
            get { return 0.6; }
        }
    }

    public class Chocolate : Drink
    {
        public override string Code
        {
            get { return "H"; }
        }

        public override double Price
        {
            get { return 0.5; }
        }
    }

    public class Tea : Drink
    {
        public override string Code
        {
            get { return "T"; }
        }

        public override double Price
        {
            get { return 0.4; }
        }
    }
}