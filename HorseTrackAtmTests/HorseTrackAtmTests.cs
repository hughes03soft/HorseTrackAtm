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
                "3, CountSheep, 9, lost" + Environment.NewLine +
                "4, Ms Traitour, 4, lost" + Environment.NewLine +
                "5, Real Princess, 3, lost" + Environment.NewLine +
                "6, Pa Kettle, 5, lost" + Environment.NewLine +
                "7, Gin Stinger, 6, lost" + Environment.NewLine;

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

            var expectedStatus = string.Copy(_defaultStatusMessage);
            expectedStatus = expectedStatus.Replace("won", "lost");
            expectedStatus = expectedStatus.Replace("Sheep, 9, lost", "Sheep, 9, won");

            Assert.AreEqual(expectedStatus, actual);

            var expectedWinningHorseNumber = 3;
            Assert.AreEqual(expectedWinningHorseNumber, _atm.WinningHorse);
        }
    }
}