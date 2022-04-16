﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm
{
    public class HorseTrackAtm
    {
        private Dictionary<int, int> _denominations = new Dictionary<int, int>();
        private List<Horse> _horses = new List<Horse>();
        private readonly IHorseTrackAtmDataLoader _loader;
        public int WinningHorse { get; set; }
        public bool IsQuitting { get; }

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
            WinningHorse = _horses[0].Number;
        }

        public string GetStatusMessage()
        {
            StringBuilder message = new StringBuilder();
            AppendInventoryStatus(message);
            AppendHorseStatus(message);

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
    }
}
