using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dojo.CoffeeMachine.Tests
{
    [TestClass]
    public class OrderTranslatorTests
    {
        private readonly IDrinkMaker _mockDrinkMaker = Mock.Of<IDrinkMaker>();
        private readonly DrinkMaker _realDrinkMaker = new DrinkMaker();

        [TestMethod]
        public void GivenAnOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<Coffee>(1) { Sugars = 1 });

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("C:1:0"), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GivenAnOrderWithUnknownDrinkThenThrowsException()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine();

            // Given
            coffeeMachine.Order(new Order<CocaCola>());
        }
        
        [TestMethod]
        public void GivenAGenericOrderWithCoffeeAndOneSugarThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<Coffee>(1) { Sugars = 1 });

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("C:1:0"), Times.Once);
        }

        [TestMethod]
        public void GivenAGenericOrderWithChocolateAndNoSugarThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<Chocolate>(1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("H::"), Times.Once);
        }

        [TestMethod]
        public void GivenAGenericOrderWithTeaAnd2SugarsThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<Tea>(1) { Sugars = 2 });

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("T:2:0"), Times.Once);
        }

        [TestMethod]
        public void GivenAMessageFromDrinkMakerThenCoffeeMachineDisplaysTheMessage()
        {
            const string expected = "Hello dear customer!";
            
            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            _realDrinkMaker.SendMessage(expected);

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }
        
        [TestMethod]
        public void GivenAnOrderWithInsuffisantAmountOfMoneyThenGetExpectedMessage()
        {
            const string expected = "Missing 0,40 €";

            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<Tea>());

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }

        [TestMethod]
        public void GivenAnOrderWithExtraHotChocolateThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<ExtraHotChocolate>(1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Hh::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithExtraHotCoffeeThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<ExtraHotCoffee>(1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Ch::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithExtraHotTeaThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<ExtraHotTea>(1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("Th::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderWithOrangeJuiceThenGetExpectedCommand()
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine(_mockDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<OrangeJuice>(1));

            // Then
            Mock.Get(_mockDrinkMaker).Verify(f => f.Process("O::"), Times.Once);
        }

        [TestMethod]
        public void GivenAnOrderOfOrangeJuiceWithInsuffisantAmountOfMoneyThenGetExpectedMessage()
        {
            const string expected = "Missing 0,60 €";

            CoffeeMachine coffeeMachine = new CoffeeMachine(_realDrinkMaker);

            // Given
            coffeeMachine.Order(new Order<OrangeJuice>());

            // Then
            Assert.IsTrue(coffeeMachine.Messages.Count > 0, "Aucun message reçu");
            Assert.AreEqual(expected, coffeeMachine.Messages.Dequeue());
        }

        
        #region Inner types

        /// <summary>
        /// Une  boisson non gérée par la machine
        /// </summary>
        private class CocaCola : Drink
        {
            public override string Code
            {
                get { return "COKE"; }
            }

            public override double Price
            {
                get { return 0; }
            }
        }

        #endregion
    }
}