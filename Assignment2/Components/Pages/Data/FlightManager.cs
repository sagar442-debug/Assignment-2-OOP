using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment2.Components.Pages.Data
{
    internal class FlightManager
    {
        public static string WEEKDAY_ANY = "Any";
        public static string WEEKDAY_SUNDAY = "Sunday";
        public static string WEEKDAY_MONDAY = "Monday";
        public static string WEEKDAY_TUESDAY = "Tuesday";
        public static string WEEKDAY_WEDNESDAY = "Wednesday";
        public static string WEEKDAY_THURSDAY = "Thursday";
        public static string WEEKDAY_FRIDAY = "Friday";
        public static string WEEKDAY_SATURDAY = "Saturday";

        public static string FLIGHTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\flights.csv");
        public static string AIRPORTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\airports.csv");

        public static List<Flight> Flights { get; private set; }
        public static List<string> airports { get; private set; }

        public FlightManager()
        {
            Flights = new List<Flight>();
            airports = new List<string>();
            PopulateAirports();
            PopulateFlights();
        }

        public string findAirportByCode(string code)
        {
            return airports.FirstOrDefault(airport => airport.Equals(code));
        }

        public static Flight findFlightByCode(string code)
        {
            return Flights.FirstOrDefault(flight => flight.Code.Equals(code));
        }

        public static List<Flight> findFlights(string from, string to, string weekday)
        {
            return Flights.Where(flight =>
                (from == WEEKDAY_ANY || flight.From.Equals(from)) &&
                (to == WEEKDAY_ANY || flight.To.Equals(to)) &&
                (weekday == WEEKDAY_ANY || flight.Weekday.Equals(weekday))).ToList();
        }

        private void PopulateFlights()
        {
            try
            {
                foreach (string line in File.ReadLines(FLIGHTS_TEXT).Skip(1))
                {
                    string[] parts = line.Split(",");
                    string code = parts[0];
                    string airline = parts[1];
                    string from = parts[2];
                    string to = parts[3];
                    string weekday = parts[4];
                    string time = parts[5];
                    int seatsAvailable = int.Parse(parts[6]);
                    double pricePerSeat = double.Parse(parts[7]);
                    Flights.Add(new Flight(code, airline, from, to, weekday, time, seatsAvailable, pricePerSeat));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading flights file: " + e.Message);
            }
        }

        private void PopulateAirports()
        {
            try
            {
                foreach (string line in File.ReadLines(AIRPORTS_TEXT))
                {
                    string[] parts = line.Split(",");
                    string code = parts[0];
                    airports.Add(code);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading airports file: " + e.Message);
            }
        }
    }
}
