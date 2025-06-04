using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;

namespace Reservation
{
    /// <summary>
    /// Interaction logic for MovieTheaterWindow.xaml
    /// </summary>
    public partial class MovieTheaterWindow : Window
    {
        private string _folderPath;// path of the directory we have chosen at the start of the application
        private List<string> _reservationsList = new List<string>(); // contains the seats the user has reserved. 
        private bool _isNationalInsuranceNumberOke = false;
        private readonly SolidColorBrush _colorOccupied = new SolidColorBrush(Colors.Red); // background color of an occupied seat
        private readonly SolidColorBrush _colorNotOccupied = new SolidColorBrush(Colors.Green); // background color of an unoccupied seat
        private Movieshow _movieshow;  // the movieshow which is shown in this window

        //ToDo: change the signature of the constructor
        //      Don't forget to fill in the title of the window and the priceTextBox
        public MovieTheaterWindow(Movieshow movieshow, string folderPath)
        {
            InitializeComponent();
            _movieshow = movieshow;
            _folderPath = folderPath;
            Title = $"Film {_movieshow.Name} in {_movieshow.ReservationHall.Name} op {StartTimeString()} startuur {StartTimeHourString()}";
            priceTextBox.Text = $"€ {_movieshow.Price:0.00}";
            PlaceOnCanvas();
        }

        private string StartTimeString() => _movieshow.StartTime.ToString("d/M");
        private string StartTimeHourString() => _movieshow.StartTime.ToString("HH:mm");

        // ToDo: uncomment the code
        private void PlaceOnCanvas()
        {
            MovieTheaterHall hall = _movieshow.ReservationHall as MovieTheaterHall;
            double heightOfRow = (paperCanvas.Height - hall.NumberOfRows * 2 - 2) / hall.NumberOfRows;
            double widthOfChair = (paperCanvas.Width - 2 * ReservationConstants.SeatsOnRow - 2) / ReservationConstants.SeatsOnRow;
            char letter = 'A';
            for (int rowNumber = 0; rowNumber < hall.NumberOfRows; rowNumber++)
            {
                for (int colNumber = 0; colNumber < ReservationConstants.SeatsOnRow; colNumber++)
                {
                    double x = 2 * (colNumber + 1) + colNumber * widthOfChair;
                    double y = 2 * (rowNumber + 1) + rowNumber * heightOfRow;
                    Button seatButton = new Button()
                    {
                        Content = letter + "" + (colNumber + 1),
                        Margin = new Thickness(x, y, 0, 0),
                        Width = widthOfChair,
                        Height = heightOfRow,
                    };
                    seatButton.Click += seatButton_Click;
                    if (hall.IsSeatReserved[rowNumber, colNumber])
                    {
                        seatButton.IsEnabled = false;
                        seatButton.Background = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        seatButton.Background = _colorNotOccupied;
                    }
                    paperCanvas.Children.Add(seatButton);
                }
                letter++;
            }
        }

        private void seatButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (!_isNationalInsuranceNumberOke)
            {
                ValidateNationalInsuranceNumber();
                return;
            }
            string seat = button.Content.ToString();
            if (_reservationsList.Contains(seat))
            {
                _reservationsList.Remove(seat);
                button.Background = _colorNotOccupied;
            }
            else
            {
                _reservationsList.Add(seat);
                button.Background = _colorOccupied;
            }
            numberOFTicketsTextBox.Text = _reservationsList.Count.ToString();
            totalPriceTextBox.Text = $"€ {(_reservationsList.Count * _movieshow.Price):0.00}";
            reservationButton.IsEnabled = _reservationsList.Count > 0;
        }

        private void reservationButton_Click(object sender, RoutedEventArgs e)
        {
            var result = System.Windows.MessageBox.Show("Bent u zeker dat u deze reservatie wilt maken?", "Bevestig reservatie", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _movieshow.AddReservation(_reservationsList, nationalInsuranceNumberTextBox.Text, _folderPath);
                if (_movieshow.ReservationHall is MovieTheaterHall hall)
                {
                    foreach (var seat in _reservationsList)
                    {
                        int row = seat[0] - 'A';
                        int col = int.Parse(seat.Substring(1)) - 1;
                        hall.IsSeatReserved[row, col] = true;
                    }
                }
                Close();
            }
        }

        private void ValidateNationalInsuranceNumber()
        {
            try
            {
                Validation.Validate(nationalInsuranceNumberTextBox.Text);
                nationalInsuranceNumberTextBox.IsEnabled = false;
                _isNationalInsuranceNumberOke = true;
                reservationButton.IsEnabled = _reservationsList.Count > 0;
            }
            catch (ValidationException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

