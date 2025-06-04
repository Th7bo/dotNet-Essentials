using System.Collections.Generic;
using System.Windows;

namespace Reservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Movieshow> _moviesShows; // movies shown in the eventListBox
        private string _folderPath; // path of the directory we have choosen at the start of the application

        //ToDo: Make a list of all movies. Make use of the CreateMovieShows method from the class DataFactory
        //      Ensure that the right info is shown in the eventListBox
        public MainWindow()
        {
            InitializeComponent();
            
            Title = "Geplande filmvoorstellingen";
            _moviesShows = DataFactory.CreateMovieShows(out _folderPath);
            eventListBox.ItemsSource = _moviesShows;
            eventListBox.DisplayMemberPath = "Name";
            eventListBox.MouseDoubleClick += EventListBox_MouseDoubleClick;
        }

        private void EventListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (eventListBox.SelectedItem is Movieshow selectedShow)
            {
                var window = new MovieTheaterWindow(selectedShow, _folderPath);
                window.ShowDialog();
            }
        }

        //ToDo: Doubleclick on a film in the list => MovieTheaterWindow should open.
        //      The film you clicked on and the _folderPath should be the arguments of the constructor
        //      This movieTheaterWindow should be shown as a dialog
        
    }
}
