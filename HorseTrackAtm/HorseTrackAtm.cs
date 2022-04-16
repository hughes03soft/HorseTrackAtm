using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm
{
    public class HorseTrackAtm
    {
        private readonly string Currency = "$";

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
            status.AppendLine("Inventory:");

            foreach (var denomination in _denominations.Keys)
            {
                var quantity = _denominations[denomination];
                status.AppendFormat("{0}{1}, {2}", 
                    Currency, denomination, quantity);
                status.AppendLine();
            }
        }

        private void AppendHorseStatus(StringBuilder status)
        {
            status.AppendLine("Horses:");

            foreach (var horse in _horses)
            {
                status.AppendFormat("{0}, {1}, {2}, {3}", 
                    horse.Number, horse.Name, horse.Odds, 
                    horse.Number == WinningHorse ? "won" : "lost");
                status.AppendLine();
            }
        }

        public void ProcessInput(string input)
        {
            Console.WriteLine("Processing input: '{0}'", input);
        }
    }
}
