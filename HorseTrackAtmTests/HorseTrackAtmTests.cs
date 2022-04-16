﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HorseTrackAtm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm.Tests
{
    [TestClass()]
    public class HorseTrackAtmTests
    {
        private HorseTrackAtm _atm = new HorseTrackAtm();

        [TestMethod()]
        public void GetStartupMessageTest()
        {
            _atm.Load(new DefaultHorseTrackAtmDataLoader());

            var actual = _atm.GetStatusMessage();

            string expected =
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

            Assert.AreEqual(expected, actual);
        }
    }
}