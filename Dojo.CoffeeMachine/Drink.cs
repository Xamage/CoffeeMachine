using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dojo.CoffeeMachine
{
    public abstract class Drink
    {
        public abstract string Code { get; }
    }

    public class Coffee : Drink
    {
        public override string Code
        {
            get { return "C"; }
        }
    }

    public class Chocolate : Drink
    {
        public override string Code
        {
            get { return "H"; }
        }
    }

    public class Tea : Drink
    {
        public override string Code
        {
            get { return "T"; }
        }
    }
}
