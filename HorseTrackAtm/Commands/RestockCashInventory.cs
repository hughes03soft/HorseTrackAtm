﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm.Commands
{
    public class RestockCashInventory : HorseTrackAtmCommand
    {
        public RestockCashInventory(HorseTrackAtm atm, string command) : base(atm, command) { }

        public override string Execute()
        {
            throw new NotImplementedException();
        }
    }
}
