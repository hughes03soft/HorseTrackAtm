using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm.Commands
{
    public class RestockCashInventory : HorseTrackAtmCommand
    {
        public const char FirstChar = 'R';

        public RestockCashInventory(HorseTrackAtm atm, string command) : base(atm, command) { }

        public override string Execute()
        {
            string ret;

            if(IsValid(_command))
            {
                _atm.LoadDenominations();
                ret = _atm.GetStatusMessage();
            }
            else
            {
                ret = GetInvalidCommandMessage();
            }

            return ret;
        }

        private bool IsValid(string command)
        {
            return command.Length == 1 && char.ToUpper(command[0]) == FirstChar;
        }
    }
}
