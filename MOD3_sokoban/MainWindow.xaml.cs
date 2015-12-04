using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MOD3_sokoban.Controller;
using MOD3_sokoban.Model;
using MOD3_sokoban.Model.Enums;
using MOD3_sokoban.Model.Tiles;

namespace MOD3_sokoban
{
    public partial class MainWindow : Window
    {
        private GameLayer GameObjectLayer { get; set; }
        private GameLayer BoardLayer { get; set; }

        // Game settings
        private int     _timePlaying, _playerMoves, _boxMoves;
        private bool    _levelFinished;
        private string  _levelTextFileName;
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            Timer();
            NewGame("level1");
        }

        public void NewGame(string textFileName)
        {
            _levelTextFileName  = textFileName;
            _playerMoves        = 0;
            _boxMoves           = 0;
            _levelFinished      = false;
            _timePlaying        = 0;
            var level           = new Level(textFileName);

            BoardLayer          = new Board(level.levelInput, BoardLayers.GameLayer);
            GameObjectLayer     = new GameObjects(level.levelInput, BoardLayers.ObjectLayer);

            this.boardLayer.Children.Clear();
            this.boardLayer.Children.Add(BoardLayer.ShowGrid());
            RedrawGame();
            StartTimer();
        }

        public void RedrawGame()
        {
            UpdatePlayerMoves();
            UpdateBoxMoves();
            this.objectLayer.Children.Clear();
            this.objectLayer.Children.Add(GameObjectLayer.ShowGrid());
        }

        private KeyValuePair<int, int> GetPlayerPosition()
        {
            int row         = 0;
            int position    = 0;
            bool playerFound = false;
            foreach (List<Tile> t in GameObjectLayer.tileLayer)
            {
                foreach (Tile tile in t)
                {
                    if (tile is Player) { playerFound = true; break; }
                    else { position++; }
                }
                if (playerFound) { break; }
                position = 0;
                row++;
            }

            return new KeyValuePair<int, int>(row, position);
        }

        public void MovePlayer(int xPos, int yPos)
        {
            bool _levelCompletedCheck    = false;
            bool _levelFailedCheck  = false;
            KeyValuePair<int, int> playerPosition = GetPlayerPosition();
            int x = playerPosition.Value    + xPos;
            int y = playerPosition.Key      + yPos;

            if (BoardLayer.tileLayer[y][x] is Floor || BoardLayer.tileLayer[y][x] is Destination)
            {
                if (GameObjectLayer.tileLayer[y][x] is Box || GameObjectLayer.tileLayer[y][x] is BoxDone)
                {
                    if (BoardLayer.tileLayer[y + yPos][x + xPos] is Floor &&
                        !(GameObjectLayer.tileLayer[y + yPos][x + xPos] is Box ||
                          GameObjectLayer.tileLayer[y + yPos][x + xPos] is BoxDone)
                        ||
                        BoardLayer.tileLayer[y + yPos][x + xPos] is Destination &&
                        !(GameObjectLayer.tileLayer[y + yPos][x + xPos] is Box ||
                          GameObjectLayer.tileLayer[y + yPos][x + xPos] is BoxDone))
                    {
                        GameObjectLayer.tileLayer[y][x] = new Empty();
                        if (BoardLayer.tileLayer[y + yPos][x + xPos] is Destination)
                        {
                            GameObjectLayer.tileLayer[y + yPos][x + xPos] = new BoxDone();
                            _levelCompletedCheck = true;
                            _levelFailedCheck = true;
                        }
                        else
                        {
                            GameObjectLayer.tileLayer[y + yPos][x + xPos] = new Box();
                            _levelFailedCheck = true;
                        }
                        _boxMoves++;

                        GameObjectLayer.tileLayer[playerPosition.Key][playerPosition.Value] = new Empty();
                        GameObjectLayer.tileLayer[y][x] = new Player();
                        _playerMoves++;
                    }
                }
                else
                {
                    GameObjectLayer.tileLayer[playerPosition.Key][playerPosition.Value] = new Empty();
                    GameObjectLayer.tileLayer[y][x] = new Player();
                    _playerMoves++;
                }

                // Update and redraw
                RedrawGame();
                if (_levelCompletedCheck || _levelFailedCheck) { UpdateLevel(_levelCompletedCheck, _levelFailedCheck); }
            }
        }

        private void UpdateBoxMoves()
        {

            boxMoves.Content = "Boxes moved: " + _boxMoves;
        }

        private void UpdatePlayerMoves()
        {
            playerMoves.Content = "Moves: " + _playerMoves;
        }
        private void UpdateLevel(bool levelCompletedCheck, bool levelFailedCheck)
        {
            if (levelCompletedCheck)
            {
                CheckLevelCompleted();
            }
            if (levelFailedCheck)
            {
                CheckLevelFailed();
            }
        }


        private void CheckLevelCompleted()
        {
            if (!GameObjectLayer.tileLayer.SelectMany(t => t).OfType<Box>().Any())
            {
                StopTimer();
                MessageBox.Show("Congratulations! You've finished this level!");
                _levelFinished = true;
            }
        }

        private void CheckLevelFailed()
        {
            int boxesCanMove = 0;
            int boxesLeft = 0;
            int row = 0;
            int position = 0;
            foreach (List<Tile> t in GameObjectLayer.tileLayer)
            {
                foreach (Tile tile in t)
                {
                    if (tile is Box)
                    {
                        // Check if box can move atleast 3 ways.
                        bool up = false, right = false, down = false, left = false;
                        if (
                            !(GameObjectLayer.tileLayer[row - 1][position] is Box ||
                              GameObjectLayer.tileLayer[row - 1][position] is BoxDone) &&
                            (BoardLayer.tileLayer[row - 1][position] is Floor ||
                             BoardLayer.tileLayer[row - 1][position] is Destination))
                        {
                            up = true;
                        }
                        if (
                            !(GameObjectLayer.tileLayer[row + 1][position] is Box ||
                              GameObjectLayer.tileLayer[row + 1][position] is BoxDone) &&
                            (BoardLayer.tileLayer[row + 1][position] is Floor ||
                             BoardLayer.tileLayer[row + 1][position] is Destination))
                        {
                            down = true;
                        }
                        if (
                            !(GameObjectLayer.tileLayer[row][position + 1] is Box ||
                              GameObjectLayer.tileLayer[row][position + 1] is BoxDone) &&
                            (BoardLayer.tileLayer[row][position + 1] is Floor ||
                             BoardLayer.tileLayer[row][position + 1] is Destination))
                        {
                            right = true;
                        }
                        if (
                            !(GameObjectLayer.tileLayer[row][position - 1] is Box ||
                              GameObjectLayer.tileLayer[row][position - 1] is BoxDone) &&
                            (BoardLayer.tileLayer[row][position - 1] is Floor ||
                             BoardLayer.tileLayer[row][position - 1] is Destination))
                        {
                            left = true;
                        }
                        if (up && down || left && right)
                        {
                            boxesCanMove++;
                        } // Not needed, but just to make it more readable..

                        // Add one new Box to counter
                        boxesLeft++;
                    }
                    position++;
                }
                position = 0;
                row++;
            }

            // Game over..
            if (boxesLeft > 0 && boxesCanMove == 0)
            {
                StopTimer();
                MessageBox.Show("Oh no.. you failed this level! Try again!");
            }
        }

        private void Timer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(Each_Tick);
        }

        private void Each_Tick(object sender, EventArgs e)
        {
            _timePlaying = _timePlaying + 1;
            timePlaying.Content = "Time: " + _timePlaying;
        }

        private void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
        }

        // Keys and Buttons
        private void KeyHandler(object sender, KeyEventArgs e)
        {
            if (!_levelFinished)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        MovePlayer(0, -1);
                        break;
                    case Key.Right:
                        MovePlayer(1, 0);
                        break;
                    case Key.Down:
                        MovePlayer(0, 1);
                        break;
                    case Key.Left:
                        MovePlayer(-1, 0);
                        break;
                }
            }
            else
            {
                MessageBox.Show("You've already completed this level!");
            }
        }

        private void StartLevel_1(object sender, RoutedEventArgs e)
        {
            NewGame("level1");
        }

        private void StartLevel_2(object sender, RoutedEventArgs e)
        {
            NewGame("level2");
        }

        private void StartLevel_3(object sender, RoutedEventArgs e)
        {
            NewGame("level3");
        }

        private void RestartButton(object sender, RoutedEventArgs e)
        {
            NewGame(_levelTextFileName);
        }
    }
}
