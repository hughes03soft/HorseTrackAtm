using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm
{
    class HorseTrackAtm
    {
        public bool IsQuitting { get; }

        public void ShowStartupMessage()
        {
            Console.WriteLine("Starting up...");
        }

        public void ProcessInput(string input)
        {
            Console.WriteLine("Processing input: '{0}'", input);
        }
    }
}
