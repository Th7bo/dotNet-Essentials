using System;
using System.IO;
using System.Windows.Controls;

public class Board
{
    private Block[,] _array;
    private StreamWriter _streamWriter;
    
    private int _size;
    private double _blockSize;
    private double _margin;
    private string _logPath;

    public Board(int size, double blockSize, double margin)
    {
        _size = size;
        _blockSize = blockSize;
        _margin = margin;
        _array = new Block[_size, _size];
        for (int r = 0; r < _size; r++)
            for (int c = 0; c < _size; c++)
                _array[r, c] = new Block(r, c, _blockSize, _margin);
        _logPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "domination.txt");
        _streamWriter = new StreamWriter(_logPath);
    }

    public void DisplayBoardOnCanvas(Canvas canvas)
    {
        canvas.Children.Clear();
        for (int r = 0; r < _size; r++)
            for (int c = 0; c < _size; c++)
                _array[r, c].DisplayBlockOnCanvas(canvas, _blockSize, _margin);
    }

    public void ClaimBlocks(int row, int col, Player player)
    {
        if (!_array[row, col].IsFree)
            throw new DominationException("Move not possible.");
        if (player == Player.Red)
        {
            if (row + 1 >= _size || !_array[row + 1, col].IsFree)
                throw new DominationException("Move not possible.");
            _array[row, col].Owner = Player.Red;
            _array[row + 1, col].Owner = Player.Red;
            LogMove(player, row, col, row + 1, col);
        }
        else if (player == Player.Blue)
        {
            if (col + 1 >= _size || !_array[row, col + 1].IsFree)
                throw new DominationException("Move not possible.");
            _array[row, col].Owner = Player.Blue;
            _array[row, col + 1].Owner = Player.Blue;
            LogMove(player, row, col, row, col + 1);
        }
    }

    public bool HasMoveLeftFor(Player player)
    {
        if (player == Player.Red)
        {
            for (int r = 0; r < _size - 1; r++)
                for (int c = 0; c < _size; c++)
                    if (_array[r, c].IsFree && _array[r + 1, c].IsFree)
                        return true;
        }
        else if (player == Player.Blue)
        {
            for (int r = 0; r < _size; r++)
                for (int c = 0; c < _size - 1; c++)
                    if (_array[r, c].IsFree && _array[r, c + 1].IsFree)
                        return true;
        }
        return false;
    }

    public void Restart()
    {
        for (int r = 0; r < _size; r++)
            for (int c = 0; c < _size; c++)
                _array[r, c].Owner = Player.None;
        EndGame();
        _streamWriter = new StreamWriter(_logPath);
    }

    public void EndGame()
    {
        _streamWriter?.Close();
    }

    private void LogMove(Player player, int r1, int c1, int r2, int c2)
    {
        _streamWriter.WriteLine($"{player} player ({r1},{c1}) ({r2},{c2})");
        _streamWriter.Flush();
    }

    public string LogPath => _logPath;
} 