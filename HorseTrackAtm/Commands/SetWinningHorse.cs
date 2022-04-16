namespace HorseTrackAtm.Commands
{
    public class SetWinningHorse : HorseTrackAtmCommand
    {
        public const char FirstChar = 'W';
        private int _horseNumber = 0;
        
        public SetWinningHorse(HorseTrackAtm atm, string command) : base(atm, command) { }
        public override string Execute()
        {
            if (IsValid(_command) == false)
                return GetInvalidCommandMessage();

            if (!_atm.IsHorseRegistered(_horseNumber))
                return GetInvalidHorseNumberMessage();

            _atm.WinningHorse = _horseNumber;
            return "";
        }

        private bool IsValid(string command)
        {
            char oneSpace = ' ';
            if (command[1] != oneSpace)
                return false;

            string horseNumber = command.Substring(2, command.Length - 2);
            return int.TryParse(horseNumber, out _horseNumber);
        }

        private string GetInvalidHorseNumberMessage()
        {
            return "Invalid Horse Number: " + _horseNumber;
        }
    }
}
