using SequenceGenerator.Constant;
using SequenceGenerator.Services.Interfaces;
using System;
using System.Linq;

namespace SequenceGenerator.Services
{
    internal class GenerateRandomString : IGenerateRandomString
    {
        /// <summary>
        /// By default return a random string with 30 chars
        /// Could be customized to suit the number
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GenerateRandomAplhaNumericString(int length = 30)
        {
            const string chars = Constants.RandomStringChars;

            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
