namespace HorseTrackAtm
{
    public class Horse
    {
        public int Number { get; private set; }
        public string Name { get; private set; }
        public int Odds { get; private set; }

        public Horse(int number, string name, int odds)
        {
            Number = number;
            Name = name;
            Odds = odds;
        }
    }
}