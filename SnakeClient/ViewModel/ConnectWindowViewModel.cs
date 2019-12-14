using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Snake
{
    class ConnectWindowViewModel : INotifyPropertyChanged
    {
        private MainWindow mainWindow;
        private RelayCommand acceptClick;
        private RelayCommand cancelClick;

        private string serverAdress = "http://";
        private string token = "";

        public string ServerAdress
        {
            get { return serverAdress; }
            set
            {
                serverAdress = value;
                OnPropertyChanged("ServerAdress");
            }

        }

        public string SecurityToken
        {
            get { return token; }
            set
            {
                token = value;
                OnPropertyChanged("SecurityToken");
            }

        }

        public RelayCommand AcceptClick
        {
            get { return acceptClick ?? (acceptClick = new RelayCommand(obj => CallMainWindow())); }
        }

        public RelayCommand CancelClick
        {
            get { return cancelClick ?? (cancelClick = new RelayCommand(obj => Exit())); }
        }

        private void CallMainWindow()
        {
            mainWindow = new MainWindow(ServerAdress, SecurityToken);
            Application.Current.MainWindow = mainWindow;
            Application.Current.Windows[0].Close();
            mainWindow.Show();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
