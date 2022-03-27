using System.Collections.Generic;

namespace SequenceGenerator.Services.Interfaces
{
    public interface ISequenceFinder
    {
        Dictionary<int,int> FindSequence(string randomlyGeneratedString);
    }
}
