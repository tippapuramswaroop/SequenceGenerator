using SequenceGenerator.ViewModels;
using System.Windows;

namespace SequenceGenerator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        //private readonly GenerateRandomSequence _viewModel;
        public HomeWindow(GenerateSequenceViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
