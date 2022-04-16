namespace HorseTrackAtm.Commands
{
    public class UnknownCommand : HorseTrackAtmCommand
    {
        public UnknownCommand(HorseTrackAtm atm, string command) : base(atm, command) { }

        public override string Execute()
        {
            return GetInvalidCommandMessage();
        }
    }
}
