namespace Dojo.CoffeeMachine
{
    /// <summary>
    /// Classe de base des options
    /// </summary>
    public abstract class Option : Drink
    {
        protected Drink Drink { get; private set; }

        protected Option(Drink drink)
        {
            Drink = drink;
        }

        /// <summary>
        /// Obtient le prix de l'option
        /// </summary>
        protected abstract double OptionPrice { get; }

        /// <summary>
        /// Obtient le prix de la boisson = prix de l'option courante + prix de la boisson (ou de l'option) associée
        /// </summary>
        public override double Price
        {
            get { return OptionPrice + Drink.Price; }
        }
    }

    /// <summary>
    /// Sert de classe de base aux options spécifiques au café
    /// </summary>
    public abstract class CoffeeOption : Option
    {
        protected CoffeeOption(Coffee drink) : base(drink)
        { }
    }

    /// <summary>
    /// Sert de classe de base aux options spécifiques au chocolat
    /// </summary>
    public abstract class ChocolateOption : Option
    {
        protected ChocolateOption(Chocolate drink) : base(drink)
        { }
    }

    /// <summary>
    /// Sert de classe de base aux options spécifiques au thé
    /// </summary>
    public abstract class TeaOption : Option
    {
        protected TeaOption(Tea drink) : base(drink)
        { }
    }

    /// <summary>
    /// Option lait
    /// </summary>
    public class Milk : Option
    {
        public Milk(Drink drink) : base(drink)
        { }

        public override string Code
        {
            get { return base.Drink.Code + "l"; }
        }

        protected override double OptionPrice
        {
            get { return 0.10; }
        }
    }

    /// <summary>
    /// Option boisson extra chaude
    /// </summary>
    public class ExtraHot : Option
    {
        public ExtraHot(Drink drink) : base(drink)
        { }

        public override string Code
        {
            get { return base.Drink.Code + "h"; }
        }

        protected override double OptionPrice
        {
            get { return 0; }
        }
    }

    /// <summary>
    /// Option Macchiato. Disponible uniquement pour le café.
    /// </summary>
    public class Macchiato : CoffeeOption
    {
        public Macchiato(Coffee coffee) : base(coffee)
        { }

        public override string Code
        {
            get { return base.Drink.Code + "m"; }
        }

        protected override double OptionPrice
        {
            get { return 0.3; }
        }
    }
}