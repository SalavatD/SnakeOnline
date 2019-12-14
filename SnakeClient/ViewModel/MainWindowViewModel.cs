using SnakeClient.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly string token;
        private int countOfRounds;
        private readonly int gameElementWidth = 10;
        private readonly int timeUntilNextTurnMillisecondsLocal = 125;
        private int roundNumber;
        private int playersOnline;
        private List<PlayerStateResponseBody> players { get; set; } = new List<PlayerStateResponseBody>();

        private readonly MainWindow view;
        private readonly MainWindowModel model;

        public string PlayerName => $"Ваше имя: {model.GetName(token).name}";
        public string RoundNumber => $"Номер раунда: {roundNumber}";
        public string PlayerCount => $"Количество игроков: {playersOnline}";
        public BindingList<string> AllPlayersName { get; set; } = new BindingList<string>();

        public MainWindowViewModel(string serverAdress, string token, MainWindow view)
        {
            this.token = token;
            this.view = view;
            model = new MainWindowModel(serverAdress);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(timeUntilNextTurnMillisecondsLocal);
            timer.Start();

            view.KeyDown += new KeyEventHandler(OnButtonKeyDown);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            GameStateResponseBody gameState = model.GetGameState();

            Paint(gameState);

            roundNumber = gameState.roundNumber;
            playersOnline = gameState.players.Count(x => x.snake != null);

            AllPlayersName.Clear();
            foreach (var i in gameState.players)
                if (i.snake != null)
                    AllPlayersName.Add(i.Name);

            OnPropertyChanged("RoundNumber");
            OnPropertyChanged("PlayerCount");
            OnPropertyChanged("AllPlayersName");
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            string direction = "";
            switch (e.Key)
            {
                case Key.Down:
                    direction = "Down";
                    break;
                case Key.Up:
                    direction = "Up";
                    break;
                case Key.Left:
                    direction = "Left";
                    break;
                case Key.Right:
                    direction = "Right";
                    break;
            }
            if (direction != "")
                Task.Run(() =>
                    model.PostDirection(new DirectionResponseBody(direction, token)));
        }

        private void Paint(GameStateResponseBody gameState)
        {
            if (gameState.roundNumber != countOfRounds)
            {
                countOfRounds = gameState.roundNumber;
                view.containerGameField.Width = (gameState.gameBoardSize.width * gameElementWidth) * 1.6;
                view.containerGameField.Height = (gameState.gameBoardSize.height * gameElementWidth);
                players.Clear();
                
                foreach (var i in gameState.players)
                    if (i.snake != null)
                        players.Add(i);
            }
            view.gameField.Children.Clear();

            #region Отрисовка стен
            foreach (SnakeClient.DataTransferObjects.Rectangle wall in gameState.walls)
            {
                System.Windows.Shapes.Rectangle newRectangle = new System.Windows.Shapes.Rectangle
                {
                    Fill = Brushes.CadetBlue,
                    Width = gameElementWidth * wall.width,
                    Height = gameElementWidth * wall.height
                };
                Canvas.SetTop(newRectangle, wall.Y * gameElementWidth);
                Canvas.SetLeft(newRectangle, wall.X * gameElementWidth);
                view.gameField.Children.Add(newRectangle);
            }
            #endregion

            #region Отрисовка еды
            foreach (Point food in gameState.food)
            {
                Ellipse newEllipse = new Ellipse
                {
                    Fill = Brushes.DarkRed,
                    Width = gameElementWidth,
                    Height = gameElementWidth
                };
                Canvas.SetTop(newEllipse, food.Y * gameElementWidth);
                Canvas.SetLeft(newEllipse, food.X * gameElementWidth);
                view.gameField.Children.Add(newEllipse);
            }
            #endregion

            #region Отрисовка змеек
            foreach (PlayerStateResponseBody player in gameState.players)
            {
                if (player.snake != null)
                    foreach (Point playerElement in player.snake)
                    {
                        Ellipse newEllipse = new Ellipse
                        {
                            Fill = new SolidColorBrush(IntToColor(player.Name.GetHashCode())),
                            Width = gameElementWidth,
                            Height = gameElementWidth
                        };
                        Canvas.SetTop(newEllipse, playerElement.Y * gameElementWidth);
                        Canvas.SetLeft(newEllipse, playerElement.X * gameElementWidth);
                        view.gameField.Children.Add(newEllipse);
                    }
            }
            #endregion
        }

        private Color IntToColor(int value)
        {
            return new Color()
            {
                R = (byte)(((value & 0xFF0) >> 64) / 64),
                G = (byte)(((value & 0xFF00) >> 128) / 128),
                B = (byte)(((value & 0xFF0) >> 64) / 64),
                A = 255
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
