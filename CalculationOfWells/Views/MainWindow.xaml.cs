using System.Windows;
using CalculationOfWells.ViewModels;

namespace CalculationOfWells.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new TestViewModel();
        }
    }
}