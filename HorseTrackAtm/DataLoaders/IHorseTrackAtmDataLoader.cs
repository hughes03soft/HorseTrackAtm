using System.Collections.Generic;

namespace HorseTrackAtm
{
    public interface IHorseTrackAtmDataLoader
    {
        SortedDictionary<int, int> GetDenominations();
        List<Horse> GetHorses();
    }
}
