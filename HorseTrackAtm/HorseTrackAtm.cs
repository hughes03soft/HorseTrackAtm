using System;
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
        
        public int WinningHorse { get; set; }
        public bool IsQuitting { get; }


        public void Load(IHorseTrackAtmDataLoader loader)
        {
            _denominations = loader.GetDenominations();
            _horses = loader.GetHorses();

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

        public void ProcessInput(string input)
        {
            Console.WriteLine("Processing input: '{0}'", input);
        }
    }
}
