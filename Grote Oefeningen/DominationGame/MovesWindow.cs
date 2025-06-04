using System.IO;
using System.Windows;
using System.Windows.Controls;

public class MovesWindow : Window
{
    public MovesWindow(string logPath)
    {
        this.Title = "MovesWindow";
        this.Width = 400;
        this.Height = 400;
        var textBox = new TextBox
        {
            IsReadOnly = true,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
            FontFamily = new System.Windows.Media.FontFamily("Consolas"),
            FontSize = 14,
            TextWrapping = TextWrapping.NoWrap
        };
        if (File.Exists(logPath))
        {
            textBox.Text = File.ReadAllText(logPath);
        }
        this.Content = textBox;
    }
} 