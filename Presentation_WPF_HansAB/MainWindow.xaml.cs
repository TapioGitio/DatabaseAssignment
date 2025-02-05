using Presentation_WPF_HansAB.ViewModels;
using System.Windows;

namespace Presentation_WPF_HansAB;
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = mainViewModel;
    }
}