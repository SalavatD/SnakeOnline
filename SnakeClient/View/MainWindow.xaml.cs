using System.Windows;

namespace Snake
{
    public partial class MainWindow : Window
    {
        public MainWindow(string serverAdress, string token)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(serverAdress, token, this);
        }
    }
}
