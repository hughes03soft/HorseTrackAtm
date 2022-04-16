using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm.Commands
{
    public static class HorseTrackAtmCommandFactory
    {
        public static HorseTrackAtmCommand Create(HorseTrackAtm atm, string command)
        {
            var firstChar = char.ToUpper(command[0]);

            HorseTrackAtmCommand ret;

            switch(firstChar)
            {
                case RestockCashInventory.FirstChar:
                    ret = new RestockCashInventory(atm, command);
                    break;
                case QuitCommand.FirstChar:
                    ret = new QuitCommand(atm, command);
                    break;
                case SetWinningHorse.FirstChar:
                    ret = new SetWinningHorse(atm, command);
                    break;
                default:
                    if(char.IsNumber(firstChar))
                    {
                        ret = new BetOnHorse(atm, command);
                    }
                    else
                    {
                        ret = new UnknownCommand(atm, command);
                    }
                    break;
            }

            return ret;
        }
    }
}
