using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DominationGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board _board;
        private Player _currentPlayer;
        private int _boardSize = 8;
        private double _sizeBlock;
        private double _marge = 0;
        private bool _gameStarted = false;
        private double _canvasSize = 480;

        public MainWindow()
        {
            InitializeComponent();
            _sizeBlock = _canvasSize / _boardSize;
            boardCanvas.Width = _canvasSize;
            boardCanvas.Height = _canvasSize;
            InitBoard();
            UpdateUI();
            boardCanvas.MouseUp += BoardCanvas_MouseUp;
        }

        private void InitBoard()
        {
            _board = new Board(_boardSize, _sizeBlock, _marge);
            _board.DisplayBoardOnCanvas(boardCanvas);
            _currentPlayer = (new Random().Next(2) == 0) ? Player.Red : Player.Blue;
            _gameStarted = false;
            movesMenuItem.IsEnabled = false;
        }

        private void StartGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _board.Restart();
            _board.DisplayBoardOnCanvas(boardCanvas);
            _currentPlayer = (new Random().Next(2) == 0) ? Player.Red : Player.Blue;
            _gameStarted = true;
            movesMenuItem.IsEnabled = false;
            UpdateUI();
        }

        private void MovesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!_gameStarted)
            {
                var movesWindow = new MovesWindow(_board.LogPath);
                movesWindow.ShowDialog();
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BoardCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_gameStarted) return;
            var pos = e.GetPosition(boardCanvas);
            int col = (int)(pos.X / _sizeBlock);
            int row = (int)(pos.Y / _sizeBlock);
            xCoordLabel.Content = $"X Coordinate {Math.Round(pos.X)}";
            yCoordLabel.Content = $"Y Coordinate {Math.Round(pos.Y, 2)}";
            cellLabel.Content = $"({row}, {col})";
            try
            {
                _board.ClaimBlocks(row, col, _currentPlayer);
                _board.DisplayBoardOnCanvas(boardCanvas);
                if (!_board.HasMoveLeftFor(_currentPlayer == Player.Red ? Player.Blue : Player.Red))
                {
                    _gameStarted = false;
                    _board.EndGame();
                    movesMenuItem.IsEnabled = true;
                    MessageBox.Show($"No moves possible. {_currentPlayer} player wins.", "Game Over");
                }
                else
                {
                    _currentPlayer = _currentPlayer == Player.Red ? Player.Blue : Player.Red;
                }
                UpdateUI();
            }
            catch (DominationException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void UpdateUI()
        {
            playerTurnLabel.Content = _currentPlayer == Player.Red ? "Red player's turn." : "Blue player's turn.";
        }
    }
}