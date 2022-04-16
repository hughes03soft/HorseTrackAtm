using HorseTrackAtm.Commands;
using System;

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
            Console.WriteLine(message);

            bool quit = false;
            while(quit == false)
            {
                var input = Console.ReadLine();
                var command = HorseTrackAtmCommandFactory.Create(atm, input);

                var result = command.Execute();

                if (result.Length > 0)
                    Console.WriteLine(result);

                Console.WriteLine(atm.GetStatusMessage());

                quit = atm.IsQuitting;
            }
        }
    }
}
