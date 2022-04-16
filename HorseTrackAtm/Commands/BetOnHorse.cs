using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm.Commands
{
    public class BetOnHorse : HorseTrackAtmCommand
    {
        private int _horseNumber = 0;
        private Horse _horse;
        private decimal _decimalBet;
        private string _possibleBet;

        public BetOnHorse(HorseTrackAtm atm, string command) : base(atm, command) { }

        public override string Execute()
        {
            if (IsValid(_command) == false)
                return GetInvalidCommandMessage();

            if (_possibleBet.Contains("."))
                return GetInvalidBetMessage();

            _horse = _atm.GetHorse(_horseNumber);
            if (_horse == null)
                return GetInvalidHorseNumberMessage();

            if (_atm.WinningHorse != _horseNumber)
                return GetNoPayoutMessage();

            return "";
        }

        private bool IsValid(string command)
        {
            var oneSpace = ' ';
            var firstSpaceIndex = command.IndexOf(oneSpace);
            const int notFound = -1;
            if (firstSpaceIndex == notFound)
                return false;

            var possibleHorseNumber = command.Substring(0, firstSpaceIndex);
            var ret = int.TryParse(possibleHorseNumber, out _horseNumber);
            if (ret == false)
                return false;

            _possibleBet = command.Substring(firstSpaceIndex + 1, command.Length - firstSpaceIndex - 1);
            ret = decimal.TryParse(_possibleBet, out _decimalBet);
            if (ret == false)
                return false;

            return true;
        }

        private string GetInvalidHorseNumberMessage()
        {
            return "Invalid Horse Number: " + _horseNumber;
        }

        private string GetNoPayoutMessage()
        {
            return "No Payout: " + _horse.Name;
        }

        private string GetInvalidBetMessage()
        {
            return "Invalid Bet: " + _possibleBet;
        }
    }
}
