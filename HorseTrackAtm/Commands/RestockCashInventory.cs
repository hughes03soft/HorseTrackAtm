namespace HorseTrackAtm.Commands
{
    public class RestockCashInventory : HorseTrackAtmCommand
    {
        public const char FirstChar = 'R';

        public RestockCashInventory(HorseTrackAtm atm, string command) : base(atm, command) { }

        public override string Execute()
        {
            string ret = "";

            if(IsValid(_command))
            {
                _atm.LoadDenominations();
            }
            else
            {
                ret = GetInvalidCommandMessage();
            }

            return ret;
        }

        private bool IsValid(string command)
        {
            return command.Length == 1 && char.ToUpper(command[0]) == FirstChar;
        }
    }
}
