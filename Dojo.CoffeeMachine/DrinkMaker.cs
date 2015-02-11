using System;

namespace Dojo.CoffeeMachine
{
    public class DrinkMaker : IDrinkMaker
    {
        #region IDrinkMaker Membres

        public void Process(string order)
        {
            throw new NotImplementedException("Pas encore implémenté");
        }

        public event EventHandler<MessageEventArgs> OnSendMessage;

        #endregion
    }
}