using Microsoft.VisualStudio.TestTools.UnitTesting;
using HorseTrackAtm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HorseTrackAtm.Commands;

namespace HorseTrackAtm.Tests
{
    [TestClass()]
    public class HorseTrackAtmTests
    {
        private HorseTrackAtm _atm = new HorseTrackAtm(new DefaultHorseTrackAtmDataLoader());
        private readonly string _defaultStatusMessage =
                "Inventory:" + Environment.NewLine +
                "$1, 10" + Environment.NewLine +
                "$5, 10" + Environment.NewLine +
                "$10, 10" + Environment.NewLine +
                "$20, 10" + Environment.NewLine +
                "$100, 10" + Environment.NewLine +
                "Horses:" + Environment.NewLine +
                "1, That Darn Gray Cat, 5, won" + Environment.NewLine +
                "2, Fort Utopia, 10, lost" + Environment.NewLine +
                "3, Count Sheep, 9, lost" + Environment.NewLine +
                "4, Ms Traitour, 4, lost" + Environment.NewLine +
                "5, Real Princess, 3, lost" + Environment.NewLine +
                "6, Pa Kettle, 5, lost" + Environment.NewLine +
                "7, Gin Stinger, 6, lost";

        public HorseTrackAtmTests()
        {
            _atm.LoadDenominations();
            _atm.LoadHorses();
        }

        [TestMethod()]
        public void GetStartupMessageTest()
        {
            var actual = _atm.GetStatusMessage();
            Assert.AreEqual(_defaultStatusMessage, actual);
        }

        [TestMethod()]
        public void UnknownCommand()
        {
            var command = "asdfsf";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();

            string expected = "Invalid Command: asdfsf";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RestockCashInventory()
        {
            _atm.UpdateInventory(1, 7);

            var command = "r";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            actual += _atm.GetStatusMessage();
            
            Assert.AreEqual(_defaultStatusMessage, actual);
        }

        [TestMethod()]
        public void Quit()
        {
            var command = "q";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();

            Assert.IsTrue(actual.Length == 0);
            Assert.IsTrue(_atm.IsQuitting);
        }

        [DataTestMethod]
        [DataRow("wasdf", "Invalid Command: wasdf", 1)]
        [DataRow("w sdf", "Invalid Command: w sdf", 1)]
        [DataRow("w 99", "Invalid Horse Number: 99", 1)]
        public void SetWinningHorseInvalid(string command, string expectedStatus, int expectedWinningHorseNumber)
        {
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();

            Assert.AreEqual(expectedStatus, actual);
            Assert.AreEqual(expectedWinningHorseNumber, _atm.WinningHorse);
        }

        [TestMethod()]
        public void SetWinningHorse()
        {
            var command = "w 3";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            actual += _atm.GetStatusMessage();

            var expectedStatus = string.Copy(_defaultStatusMessage);
            expectedStatus = expectedStatus.Replace("won", "lost");
            expectedStatus = expectedStatus.Replace("Sheep, 9, lost", "Sheep, 9, won");

            Assert.AreEqual(expectedStatus, actual);

            var expectedWinningHorseNumber = 3;
            Assert.AreEqual(expectedWinningHorseNumber, _atm.WinningHorse);
        }

        [DataTestMethod]
        [DataRow("1asdf", "Invalid Command: 1asdf")]
        [DataRow("1 sdf", "Invalid Command: 1 sdf")]
        [DataRow("1 1sdf", "Invalid Command: 1 1sdf")]
        [DataRow("1 1.58", "Invalid Bet: 1.58")]
        [DataRow("99 15", "Invalid Horse Number: 99")]
        public void BetOnHorseInvalid(string command, string expectedStatus)
        {
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();

            Assert.AreEqual(expectedStatus, actual);
        }

        [TestMethod()]
        public void BetOnHorseNoPayout()
        {
            var command = "3 3";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            var expected = "No Payout: Count Sheep";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BetOnHorseInsufficientFunds()
        {
            _atm.WinningHorse = 2;
            var command = "2 137";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            var expected = "Insufficient Funds: $1370";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BetOnHorseSufficientFunds()
        {
            _atm.WinningHorse = 5;
            var command = "5 137";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            var expected = 
                "Payout: Real Princess, $411" + Environment.NewLine +
                "Dispensing:" + Environment.NewLine +
                "$1, 1" + Environment.NewLine +
                "$10, 1" + Environment.NewLine +
                "$100, 4";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BetOnHorseSufficientFundsOneQuantityOfDenom()
        {
            var command = "1 1";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            var expected =
                "Payout: That Darn Gray Cat, $5" + Environment.NewLine +
                "Dispensing:" + Environment.NewLine +
                "$5, 1";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BetOnHorseSufficientFundsOneQuantityOfTwoDenoms()
        {
            _atm.WinningHorse = 5;
            var command = "5 2";
            var atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            var actual = atmCommand.Execute();
            var expected =
                "Payout: Real Princess, $6" + Environment.NewLine +
                "Dispensing:" + Environment.NewLine +
                "$1, 1" + Environment.NewLine +
                "$5, 1";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void BetOnHorseSufficientFundsToInsufficientThenRestock()
        {
            string command;
            HorseTrackAtmCommand atmCommand;

            StringBuilder actual = new StringBuilder();
            for(int i = 0; i < 18; i++ )
            {
                command = "1 2";
                atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
                actual.AppendLine(atmCommand.Execute());
            }

            command = "r";
            atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            atmCommand.Execute();

            command = "1 2";
            atmCommand = HorseTrackAtmCommandFactory.Create(_atm, command);
            actual.AppendLine(atmCommand.Execute());

            StringBuilder expected = new StringBuilder();

            for(int i = 0; i < 10; i++)
            {
                expected.AppendLine("Payout: That Darn Gray Cat, $10");
                expected.AppendLine("Dispensing:");
                expected.AppendLine("$10, 1");
            }

            for (int i = 0; i < 5; i++)
            {
                expected.AppendLine("Payout: That Darn Gray Cat, $10");
                expected.AppendLine("Dispensing:");
                expected.AppendLine("$5, 2");
            }

            expected.AppendLine("Payout: That Darn Gray Cat, $10");
            expected.AppendLine("Dispensing:");
            expected.AppendLine("$1, 10");

            expected.AppendLine("Insufficient Funds: $10");
            expected.AppendLine("Insufficient Funds: $10");

            expected.AppendLine("Payout: That Darn Gray Cat, $10");
            expected.AppendLine("Dispensing:");
            expected.AppendLine("$10, 1");

            var expectedStr = expected.ToString();
            var actualStr = actual.ToString();

            Assert.AreEqual(expectedStr, actualStr);
        }
    }
}