using NUnit.Framework;
using SequenceGenerator.Services;
using SequenceGenerator.Services.Interfaces;
using SequenceGenerator.ViewModels;

namespace SequenceGenerator.UnitTests.ViewModels
{
    public class GenerateSequenceViewModelTests
    {
        private readonly GenerateSequenceViewModel _viewModel;
        private readonly ISequenceFinder _sequenceFinder;
        private readonly IGenerateRandomString _generateRandomString;

        public GenerateSequenceViewModelTests()
        {
            _sequenceFinder = new SequenceFinder();
            _generateRandomString = new GenerateRandomString(); 

            _viewModel = new(_generateRandomString, _sequenceFinder);
        }


        [Test]
        public void ViewModelOnInit_ViewProperties_DefaultState()
        {
            //Arrange,Act
            //Assert
            Assert.False(_viewModel.IsProceedEnabled);
            Assert.False(_viewModel.IsOutputBoxVisible);
            Assert.True(_viewModel.IsBrowseEnabled);
            Assert.True(_viewModel.IsGenerateRandomStringEnabled);
            Assert.Null(_viewModel.FilePath);
            Assert.Null(_viewModel.RandomSequence);
            Assert.NotNull(_viewModel.NoSequenceFoundText);

        }
    }
}
