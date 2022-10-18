using System.Text.Json.Serialization;

namespace FlightPlanner
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportName { get; set; }

        public bool Equals(Airport airport)
        {
            var CountryCheck = this.Country == airport.Country;
            var CityCheck = this.City == airport.City;
            var AirportNameCheck = this.AirportName == airport.AirportName;
            return CountryCheck && CityCheck && AirportNameCheck;
        }
    }
}
