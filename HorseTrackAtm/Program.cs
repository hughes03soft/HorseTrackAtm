using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm
{
    class Program
    {
        static void Main(string[] args)
        {
            var atm = new HorseTrackAtm();
            atm.Load(new DefaultHorseTrackAtmDataLoader());

            var message = atm.GetStartupMessage();
            Console.Write(message);

            bool quit = false;

            while(quit == false)
            {
                var input = Console.ReadLine();
                atm.ProcessInput(input);
                quit = atm.IsQuitting;
            }
        }
    }
}
