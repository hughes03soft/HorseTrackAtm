using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm.Commands
{
    public class QuitCommand : HorseTrackAtmCommand
    {
        public const char FirstChar = 'Q';

        public QuitCommand(HorseTrackAtm atm, string command) : base(atm, command) { }

        public override string Execute()
        {
            string ret = "";

            if(IsValid(_command))
            {
                _atm.IsQuitting = true;
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
