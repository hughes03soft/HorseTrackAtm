using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HorseTrackAtm
{
    public class HorseTrackAtm
    {
        private Dictionary<int, int> _denominations = new Dictionary<int, int>();
        private List<Horse> _horses = new List<Horse>();
        private readonly IHorseTrackAtmDataLoader _loader;
        public int WinningHorse { get; set; }
        public bool IsQuitting { get; set; }

        public HorseTrackAtm(IHorseTrackAtmDataLoader loader)
        {
            _loader = loader;
        }

        public void LoadDenominations()
        {
            _denominations = _loader.GetDenominations();
        }

        public void LoadHorses()
        {
            _horses = _loader.GetHorses();
            _horses.Sort();
            WinningHorse = _horses[0].Number;
        }

        public string GetStatusMessage()
        {
            StringBuilder message = new StringBuilder();
            AppendInventoryStatus(message);
            AppendHorseStatus(message);

            message.Remove(message.Length - Environment.NewLine.Length, Environment.NewLine.Length);

            return message.ToString();
        }

        private void AppendInventoryStatus(StringBuilder status)
        {
            status.AppendLine(Constants.InventoryStatusHeader);

            foreach (var denomination in _denominations.Keys)
            {
                var quantity = _denominations[denomination];
                status.AppendFormat(Constants.InventoryStatusFormat,
                    Constants.Currency, denomination, quantity);
                status.AppendLine();
            }
        }

        private void AppendHorseStatus(StringBuilder status)
        {
            status.AppendLine(Constants.HorsesStatusHeader);

            foreach (var horse in _horses)
            {
                status.AppendFormat(Constants.HorseStatusFormat, 
                    horse.Number, horse.Name, horse.Odds, 
                    horse.Number == WinningHorse ?
                    Constants.WinStatus : Constants.LossStatus);
                status.AppendLine();
            }
        }

        public void UpdateInventory(int denomination, int quantity)
        {
            _denominations[denomination] = quantity;
        }

        public Horse GetHorse(int number)
        {
            return _horses.Find(h => h.Number == number);
        }

        public bool IsHorseRegistered(int number)
        {
            return GetHorse(number) != null;
        }

        public Dictionary<int, int> DispenseCash(int payout)
        {
            var ret = new Dictionary<int, int>(_denominations);
            var temp = new Dictionary<int, int>(_denominations);
            var denoms = _denominations.Keys.Reverse();

            foreach(var denom in denoms)
            {
                var quantity = temp[denom];
                var maxQuantity = FindMaxDenomQuantity(payout, denom, quantity);

                payout -= denom * maxQuantity;
                temp[denom] -= maxQuantity;
                ret[denom] = maxQuantity;

                if (payout == 0)
                    break;
            }

            if(payout > 0)
            {
                ret.Clear();
            }
            else
            {
                _denominations = temp;
            }

            return ret;
        }

        private int FindMaxDenomQuantity(int payout, int denom , int quantity)
        {
            int ret;
            int i;
            int offset = 0;

            for (i = 0; i < quantity; i++)
            {
                if (payout - denom * i < 0)
                {
                    offset = 1;
                    break;
                }
            }

            ret = i - offset;

            return ret;
        }
    }
}
