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

    public class ExtraHotCoffee : Coffee
    {
        public override string Code
        {
            get { return "Ch"; }
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

    public class ExtraHotChocolate : Chocolate
    {
        public override string Code
        {
            get { return "Hh"; }
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

    public class ExtraHotTea : Tea
    {
        public override string Code
        {
            get { return "Th"; }
        }
    }

    public class OrangeJuice : Drink
    {
        public override string Code
        {
            get { return "O"; }
        }

        public override double Price
        {
            get { return 0.6; }
        }
    }

}