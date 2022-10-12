using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : Controller
    {
        private readonly FlightPlannerDBContext _dbContext;
        public CustomerApiController(FlightPlannerDBContext context)
        {
            _dbContext = context;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            search = search.ToLower().Trim();
            var fromAirports = _dbContext.Airports.Where(f => f.City.ToLower().Trim().Contains(search)
                                                              || f.Country.ToLower().Trim().Contains(search)
                                                              || f.AirportName.ToLower().Trim().Contains(search)).ToArray();
            return Ok(fromAirports);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlight flight)
        {
            var flights = _dbContext.Flights.Where(f => f.From.AirportName == flight.From ||
                                                        f.To.AirportName == flight.To ||
                                                        f.DepartureTime == flight.DepartureDate).ToArray();
            if (!FlightStorage.IsValidFormat(flight))
            {
                return BadRequest();
            }
            return Ok(new PageResult(flights));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult SearchFlights(int id)
        {
            var flight = _dbContext.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
    }
}