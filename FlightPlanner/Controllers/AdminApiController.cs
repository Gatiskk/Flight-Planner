using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _locker = new object();
        private readonly FlightPlannerDBContext _dbContext;
        public AdminApiController(FlightPlannerDBContext context)
        {
            _dbContext = context;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = _dbContext.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .FirstOrDefault(f => f.Id == id);
                if (flight == null)
                {
                    return NotFound();
                }
                return Ok(flight);
            }
            
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_locker)
            {
                if (flight == null)
                {
                    return NoContent();
                }

                if (!FlightStorage.ValidFormat(flight))
                {
                    return BadRequest();
                }

                if (FlightStorage.HasSameAirport(flight))
                {
                    return BadRequest();
                }

                if (_dbContext.Flights.Any(f => f.From.AirportName == flight.From.AirportName &&
                                                f.From.City == flight.From.City &&
                                                f.From.Country == flight.From.Country &&
                                                f.To.AirportName == flight.To.AirportName &&
                                                f.To.City == flight.To.City && f.To.Country == flight.To.Country &&
                                                f.ArrivalTime == flight.ArrivalTime && f.Carrier == flight.Carrier &&
                                                f.DepartureTime == flight.DepartureTime))
                {
                    return Conflict();
                }

                _dbContext.Flights.Add(flight);
                _dbContext.SaveChanges();
                return Created("", flight);
            }
        }
            

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult FlightDelete(int id)
        {
            lock (_locker)
            {
                var flight = _dbContext.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .SingleOrDefault(f => f.Id == id);
                if (flight == null) return Ok();
                _dbContext.Flights.Remove(flight);
                _dbContext.SaveChanges();
                return Ok();
            }
        }
    }
}