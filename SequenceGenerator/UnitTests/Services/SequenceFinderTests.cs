using NUnit.Framework;
using SequenceGenerator.Services;
using SequenceGenerator.Services.Interfaces;

namespace SequenceGenerator.UnitTests.Services
{
    public class SequenceFinderTests
    {
        private readonly ISequenceFinder _sequenceFinder;

        public SequenceFinderTests()
        {
            _sequenceFinder = new SequenceFinder();
        }

        [Test]
        public void FindSequence_Returns_Null_If_The_String_IsNull()
        {
            //Arrange
            string randomString = null;

            //Act
            var result = _sequenceFinder.FindSequence(randomString);

            //Assert
            Assert.Null(result);

        }

        [Test]
        public void FindSequence_Returns_Null_If_The_String_IsEmpty()
        {
            //Arrange
            var randomString = string.Empty;

            //Act
            var result = _sequenceFinder.FindSequence(randomString);

            //Assert
            Assert.Null(result);

        }

        [Test]
        public void FindSequence_Returns_Sequence_If_The_String_IsNotNullOrEmpty()
        {
            //Arrange
            var randomString = "asd123689qwe457rty89234\n567zx01245cvbnm";

            //Act
            var result = _sequenceFinder.FindSequence(randomString);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Count, 6);

        }
    }
}
