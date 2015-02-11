using System;

namespace Dojo.CoffeeMachine
{
    public class DrinkMaker : IDrinkMaker
    {
        #region IDrinkMaker Membres

        public void Process(string order)
        {
            if (order.StartsWith("M:"))
            {
                SendMessage(order.Substring(2));
            }
            else if (order.StartsWith("Missing "))
            {
                SendMessage(order);
            }

            // On peut faire la boisson
        }

        public event EventHandler<MessageEventArgs> OnSendMessage;

        #endregion

        public void SendMessage(string message)
        {
            if (OnSendMessage != null)
            {
                OnSendMessage(this, new MessageEventArgs(message));
            }
        }
    }
}