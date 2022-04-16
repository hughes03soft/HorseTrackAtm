using System.Collections.Generic;

namespace HorseTrackAtm
{
    public class DefaultHorseTrackAtmDataLoader : IHorseTrackAtmDataLoader
    {
        public SortedDictionary<int, int> GetDenominations()
        {
            var ret = new SortedDictionary<int, int>()
            {
                { 1,   10 },
                { 5,   10 },
                { 10,  10 },
                { 20,  10 },
                { 100, 10 },
            };

            return ret;
        }

        public List<Horse> GetHorses()
        {
            var ret = new List<Horse>
            {
                new Horse(1, "That Darn Gray Cat", 5),
                new Horse(2, "Fort Utopia", 10),
                new Horse(3, "Count Sheep", 9),
                new Horse(4, "Ms Traitour", 4),
                new Horse(5, "Real Princess", 3),
                new Horse(6, "Pa Kettle", 5),
                new Horse(7, "Gin Stinger", 6),
            };

            return ret;
        }
    }
}
