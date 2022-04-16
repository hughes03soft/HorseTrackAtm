using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseTrackAtm
{
    public interface IHorseTrackAtmDataLoader
    {
        Dictionary<int, int> GetDenominations();
        List<Horse> GetHorses();
    }
}
