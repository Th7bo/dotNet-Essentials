using System.Collections.Generic;
using System.IO;
using WinForms = System.Windows.Forms;

namespace Reservation
{
    public static class DataFactory
    {
        public static List<Hall> CreateHalls()
        {
            List<Hall> list = new List<Hall>();
            string[] names = { "zaal1", "zaal2", "zaal3", "zaal4" };
            int[] maxCapacity = { 300, 160, 500, 160 };
            string[] type = { "filmtheaterzaal", "filmtheaterzaal", "evenementzaal", "filmtheaterzaal" };
            for (int i = 0; i < names.Length; i++)
            {
                if (type[i] == "evenementzaal")
                {
                    list.Add(new EventHall(names[i], maxCapacity[i]));
                }
                else
                {
                    list.Add(new MovieTheaterHall(names[i], maxCapacity[i]));
                }
            }
            return list;
        }

        public static Dictionary<string, Hall> CreateDictionary()
        {
            List<Hall> halls = CreateHalls();
            Dictionary<string, Hall> lookup = new Dictionary<string, Hall>();
            foreach (Hall hall in halls)
            {
                lookup.Add(hall.Name, hall);
            }
            return lookup;
        }

        public static List<Movieshow> CreateMovieShows(out string folderPath)
        {
            folderPath = null;
            List<Movieshow> eventList = new List<Movieshow>();
            var dialog = new WinForms.FolderBrowserDialog();
            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                folderPath = dialog.SelectedPath;
                var files = Directory.GetFiles(folderPath, "*.txt");
                foreach (var file in files)
                {
                    string movieName = Path.GetFileNameWithoutExtension(file);
                    using (var reader = new StreamReader(file))
                    {
                        string infoLine = reader.ReadLine();
                        if (string.IsNullOrWhiteSpace(infoLine)) continue;
                        var infoParts = infoLine.Split(';');
                        if (infoParts.Length < 4) continue;
                        string hallName = infoParts[0];
                        string startTime = infoParts[1];
                        string endTime = infoParts[2];
                        string supplement = infoParts[3];
                        Hall hall;
                        // Find the right hall type
                        var halls = CreateHalls();
                        hall = halls.Find(h => h.Name == hallName);
                        if (hall == null) continue;
                        Hall hallCopy;
                        if (hall is MovieTheaterHall mth)
                        {
                            hallCopy = new MovieTheaterHall(mth.Name, mth.MaxCapacity);
                        }
                        else
                        {
                            hallCopy = new EventHall(hall.Name, hall.MaxCapacity);
                        }
                        var movieshow = new Movieshow(movieName, startTime, endTime, hallCopy, supplement, file);
                        ReadReservations(movieshow, reader);
                        eventList.Add(movieshow);
                    }
                }
            }
            return eventList;
        }

        public static void ReadReservations(Movieshow movieshow, StreamReader reader)
        {
            if (movieshow.ReservationHall is MovieTheaterHall hall)
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(' ');
                    if (parts.Length > 0)
                    {
                        string seat = parts[0];
                        if (seat.Length >= 2)
                        {
                            int row = seat[0] - 'A';
                            if (int.TryParse(seat.Substring(1), out int col))
                            {
                                if (row >= 0 && row < hall.NumberOfRows &&
                                    col - 1 >= 0 && col - 1 < ReservationConstants.SeatsOnRow)
                                {
                                    hall.IsSeatReserved[row, col - 1] = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
