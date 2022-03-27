using NUnit.Framework;
using SequenceGenerator.Services;
using SequenceGenerator.Services.Interfaces;

namespace SequenceGenerator.UnitTests.Services
{
    public class GenerateRandomStringTests
    {
        private readonly IGenerateRandomString _randomString;

        public GenerateRandomStringTests()
        {
            _randomString = new GenerateRandomString();
        }

        [Test]
        public void Generate_RandomString_Returns_String()
        {
            //Arrange
            int stringLength = 30;

            //Act
            var result = _randomString.GenerateRandomAplhaNumericString(stringLength);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(stringLength, result.Length);
        }

        [Test]
        public void Generate_RandomString_With0Length_returnsEmptyString()
        {
            //Arrange
            int stringLength = 0;

            //Act
            var result = _randomString.GenerateRandomAplhaNumericString(stringLength);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(stringLength, result.Length);
        }
    }
}
