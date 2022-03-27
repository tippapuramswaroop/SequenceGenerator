using Microsoft.Win32;
using NLog;
using Prism.Commands;
using SequenceGenerator.Constant;
using SequenceGenerator.Models;
using SequenceGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace SequenceGenerator.ViewModels
{
    public class GenerateSequenceViewModel : INotifyPropertyChanged
    {
        private string _filePath;
        private string _randomSequence;
        private bool _isProceedEnabled;
        private bool _isBrowseEnabled;
        private bool _isGenerateRandomStringEnabled;
        private bool _isOutputBoxVisible;

        private List<SequenceOutput> _outputList;
        private readonly ISequenceFinder _sequenceFinder;
        private readonly IGenerateRandomString _generateRandomString;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        #region properties
        public string NoSequenceFoundText { get; set; }
        public ICommand BrowseCommand { get; set; }
        public ICommand GenerateRandomStringCommand { get; set; }
        public ICommand SequenceGeneratorCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public string RandomSequence
        {
            get { return _randomSequence; }
            set
            {
                _randomSequence = value;
                RaisePropertyChanged(nameof(RandomSequence));
            }
        }
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged(nameof(FilePath));
            }
        }

        public bool IsProceedEnabled
        {
            get { return _isProceedEnabled;}
            set
            {
                _isProceedEnabled = value;
                RaisePropertyChanged(nameof(IsProceedEnabled));
            }
        }

        public bool IsBrowseEnabled
        {
            get { return _isBrowseEnabled; }
            set
            {
                _isBrowseEnabled = value;
                RaisePropertyChanged(nameof(IsBrowseEnabled));
            }
        }

        public bool IsGenerateRandomStringEnabled
        {
            get { return _isGenerateRandomStringEnabled; }
            set
            {
                _isGenerateRandomStringEnabled = value;
                RaisePropertyChanged(nameof(IsGenerateRandomStringEnabled));
            }
        }

        public bool IsOutputBoxVisible
        {
            get { return _isOutputBoxVisible; }
            set
            {
                _isOutputBoxVisible = value;
                RaisePropertyChanged(nameof(IsOutputBoxVisible));
            }
        }

        public List<SequenceOutput> OutputList
        {
            get { return _outputList; }
            set
            {
                _outputList = value;
                RaisePropertyChanged(nameof(OutputList));
            }
        }
        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region constructor

        public GenerateSequenceViewModel(IGenerateRandomString generateRandomString, ISequenceFinder sequenceFinder)
        {
            _sequenceFinder = sequenceFinder ?? throw new ArgumentNullException(nameof(sequenceFinder));
            _generateRandomString = generateRandomString ?? throw new ArgumentNullException(nameof(generateRandomString));

            BrowseCommand = new DelegateCommand(() => BrowseFile());
            GenerateRandomStringCommand = new DelegateCommand(() => GenerateRandomAlphaNumericString());
            SequenceGeneratorCommand = new DelegateCommand(() => FindSequences());
            ResetCommand = new DelegateCommand(() => ResetSelection());

            IsBrowseEnabled = true;
            IsGenerateRandomStringEnabled = true;
            NoSequenceFoundText = $"There were no sequences found in the string {RandomSequence}";
        }
        #endregion

        #region ICommand Methods

        private void ResetSelection()
        {
            IsProceedEnabled = false;
            IsGenerateRandomStringEnabled = true;
            IsBrowseEnabled = true;
            FilePath = string.Empty;
            RandomSequence = string.Empty;
            IsOutputBoxVisible = false;
        }

        private void BrowseFile()
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Filter = Constants.TextFilter
                };

                var result = dialog.ShowDialog();
                if (result is true)
                {
                    FilePath = dialog.FileName;
                    IsGenerateRandomStringEnabled = false;
                    IsProceedEnabled = true;
                    ReadFile();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An Error Occurred while reading the file.");
            }
        }

        private void GenerateRandomAlphaNumericString()
        {
            IsBrowseEnabled = false;
            IsProceedEnabled = true;

            RandomSequence = _generateRandomString.GenerateRandomAplhaNumericString();
        }

        private void FindSequences()
        {
            OutputList = new();
            var dict =  _sequenceFinder.FindSequence(RandomSequence);
            foreach (var key in dict.Keys)
            {
                OutputList?.Add(new SequenceOutput() { Sequence = key, Count = dict[key] });
            }

            OutputList = OutputList?.OrderByDescending(x => x.Count).ThenByDescending(x => x.Sequence).ToList();
            IsOutputBoxVisible = OutputList?.Count > 0;
        }


        #endregion

        #region private methods

        private void ReadFile()
        {
            if(!File.Exists(FilePath))
            {
                RandomSequence = "Unknown File Path. Check File Path and try again";
                return;
            }
            var stream = new StreamReader(FilePath);
            var sequence = stream.ReadToEnd();
            RandomSequence = sequence;
        }

        #endregion
    }
}
