using SequenceGenerator.Constant;
using SequenceGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SequenceGenerator.Services
{
    internal class SequenceFinder : ISequenceFinder
    {
        private Dictionary<int, int> _output;
        public Dictionary<int,int> FindSequence(string randomlyGeneratedString)
        {
            if(string.IsNullOrEmpty(randomlyGeneratedString))
            {
                return null;
            }

            _output = new();
            var list = GetNumbersFromRandomString(randomlyGeneratedString);
            FindConsecutiveDigits(list);
            return _output;
        }

        private static List<int> GetNumbersFromRandomString(string sequence)
        {
            var regex = new Regex(Constants.RegexPatternToGetNumbers);
            var result = regex.Matches(sequence);
            return result.Select(x => Convert.ToInt32(x.Value)).ToList();
        }

        private void FindConsecutiveDigits(List<int> numbers)
        {
            foreach (var num in numbers)
            {
                var digit = Array.ConvertAll(num.ToString().ToArray(), x => (int)x - 48);
                if (digit.Length <= 1) continue;
                FindLongestSequencePossible(digit.ToList());
            }
        }

        private void FindLongestSequencePossible(List<int> Sequence)
        {
            var currentSequence = new List<int>() { Sequence[0] };

            for (int i = 1; i < Sequence.Count; i++)
            {
                //if 2 numbers are consecutive add to the list
                if (Sequence[i - 1] == Sequence[i] - 1)
                {
                    currentSequence.Add(Sequence[i]);
                }
                else
                {
                    //if they are not consecutive, see if there is sequence already in the list,
                    ////clear the list & add the current number in the list
                    FetchCount(currentSequence);
                    currentSequence.Clear();
                    currentSequence.Add(Sequence[i]);
                }
            }

            FetchCount(currentSequence);
        }

        private void FetchCount(List<int> currentSequence)
        {
            if (currentSequence.Count == 1)
                return;

            var num = Convert.ToInt32(string.Join(string.Empty, currentSequence));

            if (!_output.Keys.Contains(num))
            {
                _output.Add(num, 1);
            }
            else
            {
                _output[num]++;
            }
        }
    }
}
