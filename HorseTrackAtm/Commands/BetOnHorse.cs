using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseTrackAtm.Commands
{
    public class BetOnHorse : HorseTrackAtmCommand
    {
        private int _betOnHorseNumber = 0;
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

            _horse = _atm.GetHorse(_betOnHorseNumber);
            if (_horse == null)
                return GetInvalidHorseNumberMessage();

            if (_atm.WinningHorse != _betOnHorseNumber)
                return GetNoPayoutMessage();

            var payOut = (int)_decimalBet * _horse.Odds;
            var payOutBreakDown = _atm.DispenseCash(payOut);

            if (payOutBreakDown.Count == 0)
                return GetInsufficientFundsMessge(payOut);

            return GetPayoutMessage(payOut, payOutBreakDown);
        }

        private bool IsValid(string command)
        {
            const char oneSpace = ' ';
            var firstSpaceIndex = command.IndexOf(oneSpace);
            const int notFound = -1;
            if (firstSpaceIndex == notFound)
                return false;

            var possibleHorseNumber = command.Substring(0, firstSpaceIndex);
            var ret = int.TryParse(possibleHorseNumber, out _betOnHorseNumber);
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
            return "Invalid Horse Number: " + _betOnHorseNumber;
        }

        private string GetNoPayoutMessage()
        {
            return "No Payout: " + _horse.Name;
        }

        private string GetInvalidBetMessage()
        {
            return "Invalid Bet: " + _possibleBet;
        }

        private string GetInsufficientFundsMessge(int payOut)
        {
            return "Insufficient Funds: " + HorseTrackAtm.Currency + payOut;
        }

        private string GetPayoutMessage(int payOut, Dictionary<int, int> payOutBreakDown)
        {
            var message = new StringBuilder();

            message.AppendFormat("Payout: {0}, {1}{2}", 
                _horse.Name, HorseTrackAtm.Currency, payOut);
            message.AppendLine();

            message.AppendLine("Dispensing:");

            foreach (var denom in payOutBreakDown.OrderBy(x => x.Key))
            {
                message.AppendFormat("{0}{1}, {2}", 
                    HorseTrackAtm.Currency, denom.Key, denom.Value);
                message.AppendLine();
            }

            message.Remove(message.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return message.ToString();
        }
    }
}
