using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dojo.CoffeeMachine.Tests
{
    [TestClass]
    public class OrderTranslatorTests
    {
        private CoffeeMachine _coffeeMachine;
        private readonly IDrinkMaker _drinkMaker = Mock.Of<IDrinkMaker>();

        [TestInitialize]
        public void BeforeEachTest()
        {
            _coffeeMachine = new CoffeeMachine(_drinkMaker);
        }

        [TestMethod]
        public void GivenAnOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            // Given
            _coffeeMachine.Order(new Order(new Coffee()) { Sugars = 1 });

            // Then
            Mock.Get(_drinkMaker).Verify(f => f.Process("C:1:0"), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenAnOrderWithUnknownDrinkThenThrowsException()
        {
            // Given
            _coffeeMachine.Order(new Order<CocaCola>());
        }
        
        [TestMethod]
        public void GivenAGenericOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            // Given
            _coffeeMachine.Order(new Order<Coffee> { Sugars = 1 });

            // Then
            Mock.Get(_drinkMaker).Verify(f => f.Process("C:1:0"), Times.Once);
        }

        [TestMethod]
        public void GivenAGenericOrderWithChocolateAndNoSugarThenGetExpectedCommand()
        {
            // Given
            _coffeeMachine.Order(new Order<Chocolate>());

            // Then
            Mock.Get(_drinkMaker).Verify(f => f.Process("H::"), Times.Once);
        }

        [TestMethod]
        public void GivenAGenericOrderWithTeaAnd2SugarsThenGetExpectedCommand()
        {
            // Given
            _coffeeMachine.Order(new Order<Tea> { Sugars = 2 });

            // Then
            Mock.Get(_drinkMaker).Verify(f => f.Process("T:2:0"), Times.Once);
        }

        [TestMethod]
        public void GivenAMessageFromDrinkMakerThenCoffeeMachineDisplaysTheMessage()
        {
            // Given
            _coffeeMachine.Order(new Order<Tea> { Sugars = 2 });

            Mock.Get(_drinkMaker).Raise(d => d.OnSendMessage += null , new MessageEventArgs("Hello"));

            // Then
            Assert.IsTrue(_coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual("Hello", _coffeeMachine.Messages.Dequeue());
        }
        
        #region Inner types

        private class CocaCola : Drink
        {
            public override string Code
            {
                get { return "COKE"; }
            }
        }

        #endregion
    }
}