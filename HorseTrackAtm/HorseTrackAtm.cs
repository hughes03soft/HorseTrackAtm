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

        public void Load(IHorseTrackAtmDataLoader loader)
        {
            _denominations = loader.GetDenominations();
            _horses = loader.GetHorses();
        }
        public bool IsQuitting { get; }

        public string GetStartupMessage()
        {
            StringBuilder message = new StringBuilder();
            AppendInventoryStartupMessage(message);
            AppendHorsesStartupMessage(message);

            return message.ToString();
        }

        private void AppendInventoryStartupMessage(StringBuilder message)
        {
            message.AppendLine("Inventory:");

            foreach (var denomination in _denominations.Keys)
            {
                var quantity = _denominations[denomination];
                message.AppendFormat("{0}{1}, {2}", 
                    Currency, denomination, quantity);
                message.AppendLine();
            }
        }

        private void AppendHorsesStartupMessage(StringBuilder message)
        {
            message.AppendLine("Horses:");

            foreach (var horse in _horses)
            {
                message.AppendFormat("{0}, {1}, {2}", 
                    horse.Number, horse.Name, horse.Odds);
                message.AppendLine();
            }
        }

        public void ProcessInput(string input)
        {
            Console.WriteLine("Processing input: '{0}'", input);
        }
    }
}
