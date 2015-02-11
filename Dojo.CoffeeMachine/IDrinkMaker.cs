using System;

namespace Dojo.CoffeeMachine
{
    public interface IDrinkMaker
    {
        void Process(string order);

        event EventHandler<MessageEventArgs> OnSendMessage;
    }
}