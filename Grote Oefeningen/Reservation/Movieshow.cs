using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace Reservation
{
    public class Movieshow
    {
        public string Name { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public Hall ReservationHall { get; }
        public double Price { get; }
        private string _filePath;

        // ToDo: provide a constructor with the following parameters
        //       name (string), startTime (string), endTime (string)
        //       reservationHall (Hall) and supplement (string)
        public Movieshow(string name, string startTime, string endTime, Hall reservationHall, string supplement, string filePath = null)
        {
            Name = name;
            StartTime = DateTime.ParseExact(startTime, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture);
            EndTime = DateTime.ParseExact(endTime, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture);
            ReservationHall = reservationHall;
            double supplementValue = 0;
            switch (supplement.Trim().ToUpper())
            {
                case "D":
                    supplementValue = ReservationConstants.Supplement3DFilm;
                    break;
                case "L":
                    supplementValue = ReservationConstants.SupplementLaserUltraFilm;
                    break;
                case "LD":
                    supplementValue = ReservationConstants.Supplement3DFilm + ReservationConstants.SupplementLaserUltraFilm;
                    break;
                case "LONG":
                    supplementValue = ReservationConstants.SupplementLongDurationFilm;
                    break;
                case "N":
                    supplementValue = 0;
                    break;
                // Add more cases as needed
            }
            TimeSpan duration = EndTime - StartTime;
            if (duration > TimeSpan.FromMinutes(135)) // 2h15min = 135min
            {
                supplementValue += ReservationConstants.SupplementLongDurationFilm;
            }
            Price = ReservationConstants.PriceFilm + supplementValue;
            _filePath = filePath;
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        // ToDo: add each reservation from the list reservations to the right file
        //       with each reservation, the national insurance number is also written to the file
        //       e.g: E17 99080500243
        public void AddReservation(List<string> reservations, string nationalSecurityNumber, string folderPath)
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                // fallback: try to find the file by name
                _filePath = System.IO.Path.Combine(folderPath, $"{Name}.txt");
            }
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                foreach (var reservation in reservations)
                {
                    writer.WriteLine($"{reservation} {nationalSecurityNumber}");
                }
            }
        }
    }
}
