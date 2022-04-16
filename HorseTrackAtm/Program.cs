using HorseTrackAtm.Commands;
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
            var loader = new DefaultHorseTrackAtmDataLoader();
            var atm = new HorseTrackAtm(loader);

            atm.LoadDenominations();
            atm.LoadHorses();

            var message = atm.GetStatusMessage();
            Console.Write(message);

            bool quit = false;
            while(quit == false)
            {
                var input = Console.ReadLine();
                var command = HorseTrackAtmCommandFactory.Create(atm, input);

                var result = command.Execute();

                if (result.Length > 0)
                    Console.WriteLine(result);

                quit = atm.IsQuitting;
            }
        }
    }
}
