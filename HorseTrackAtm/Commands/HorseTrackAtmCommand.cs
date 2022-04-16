namespace HorseTrackAtm.Commands
{
    public abstract class HorseTrackAtmCommand
    {
        private const string InvalidCommandMessage = "Invalid Command:";

        protected readonly HorseTrackAtm _atm;
        protected readonly string _command;

        public HorseTrackAtmCommand(HorseTrackAtm atm, string command)
        {
            _atm = atm;
            _command = command;
        }
        public abstract string Execute();

        protected string GetInvalidCommandMessage()
        {
            return InvalidCommandMessage + " " + _command;
        }
    }
}
