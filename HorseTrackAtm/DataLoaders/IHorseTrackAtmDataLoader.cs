using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm
{
    public interface IHorseTrackAtmDataLoader
    {
        SortedDictionary<int, int> GetDenominations();
        List<Horse> GetHorses();
    }
}
