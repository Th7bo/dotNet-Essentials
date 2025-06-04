using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

public class Block
{
    private Rectangle _rectangle;
    private Player _owner;
    public int Row { get; private set; }
    public int Col { get; private set; }

    public Block(int row, int col, double size, double margin)
    {
        Row = row;
        Col = col;
        _owner = Player.None;
        _rectangle = new Rectangle
        {
            Width = size,
            Height = size,
            Stroke = new SolidColorBrush(Colors.Black),
            Fill = new SolidColorBrush(Colors.White),
            Margin = new System.Windows.Thickness(0)
        };
    }

    public Player Owner
    {
        get => _owner;
        set
        {
            _owner = value;
            switch (_owner)
            {
                case Player.Red:
                    _rectangle.Fill = new SolidColorBrush(Colors.Red);
                    break;
                case Player.Blue:
                    _rectangle.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                default:
                    _rectangle.Fill = new SolidColorBrush(Colors.White);
                    break;
            }
        }
    }

    public void DisplayBlockOnCanvas(Canvas canvas, double size, double margin)
    {
        Canvas.SetLeft(_rectangle, Col * size);
        Canvas.SetTop(_rectangle, Row * size);
        if (!canvas.Children.Contains(_rectangle))
            canvas.Children.Add(_rectangle);
    }

    public bool IsFree => Owner == Player.None;
} 