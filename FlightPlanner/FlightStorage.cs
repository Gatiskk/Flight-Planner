using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace FlightPlanner
{
    public class FlightStorage
    {
        private static readonly List<Flight> _flights = new List<Flight>();
        private static int _id = 1;

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = _id++;
            _flights.Add(flight);
            return flight;
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(f => f.Id == id);
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        public static bool CheckFlightAndAirport(Flight flight)
        {
            if (_flights.Count == 0)
            {
                return false;
            }

            foreach (Flight itemFlight in _flights)
            {
                if (itemFlight.Equals(flight))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public static void FlightDelete(int id)
        {
            var flight = GetFlight(id);

            if (flight != null)
            {
                _flights.Remove(flight);
            }
        }

        public static bool ValidFormat(Flight flight)
        {

            if (flight == null)
            {
                return false;
            }

            if (flight.To == null || flight.From == null)
            {
                return false;
            }

            if ((string.IsNullOrEmpty(flight.Carrier) || string.IsNullOrEmpty(flight.ArrivalTime) ||
                 string.IsNullOrEmpty(flight.DepartureTime) || flight.To == null || flight.From == null) ||
                string.IsNullOrEmpty(flight.To.AirportName) || string.IsNullOrEmpty(flight.To.City) ||
                string.IsNullOrEmpty(flight.To.Country) || string.IsNullOrEmpty(flight.From.AirportName) ||
                string.IsNullOrEmpty(
                    flight.From.City) ||
                string.IsNullOrEmpty(flight.From.Country))
            {
                return false;
            }

            var departureTime = DateTime.Parse(flight.DepartureTime);
            var arrivalTime = DateTime.Parse(flight.ArrivalTime);

            if (arrivalTime <= departureTime)
            {
                return false;
            }

            return true;
        }

        public static bool HasSameAirport(Flight flight)
        {
            if (flight.From.City.ToUpper().Trim() == flight.To.City.ToUpper().Trim() &&
                flight.From.Country.ToUpper().Trim() == flight.To.Country.ToUpper().Trim() &&
                flight.From.AirportName.ToUpper().Trim() == flight.To.AirportName.ToUpper().Trim())
            {
                return true;
            }

            return false;
        }

        public static Airport[] FindAirports(string phrase)
        {
            {
                phrase = phrase.ToLower().Trim();
                var fromAirports = _flights.Where(f => f.From.AirportName.ToLower().Trim().Contains(phrase)
                                                       || f.From.City.ToLower().Trim().Contains(phrase)
                                                       || f.From.Country.ToLower().Trim().Contains(phrase))
                    .Select(a => a.From).ToArray();
                var toAirports = _flights.Where(f => f.To.AirportName.ToLower().Trim().Contains(phrase)
                                                     || f.To.City.ToLower().Trim().Contains(phrase)
                                                     || f.To.Country.ToLower().Trim().Contains(phrase))
                    .Select(f => f.To).ToArray();

                return fromAirports.Concat(toAirports).ToArray();
            }
        }

        public static PageResult SearchFlights(SearchFlight flight)
        {
            var validFlight = _flights.Where(f => f.From.AirportName == flight.From &&
                                                  f.To.AirportName == flight.To &&
                                                  f.DepartureTime == flight.DepartureDate).ToArray();
            return new PageResult(validFlight);
        }

        public static bool IsValidFormat(SearchFlight request)
        {
            if (request.From == request.To)
            {
                return false;
            }

            if (request.To == null || request.From == null || request.DepartureDate == null)
            {
                return false;
            }

            return true;
        }
    }
}