using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _locker = new object();
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.GetFlight(id);
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

                if (FlightStorage.CheckFlightAndAirport(flight))
                {
                    return Conflict();
                }

                flight = FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
        }
            

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult FlightDelete(int id)
        {
            lock (_locker)
            {
                FlightStorage.FlightDelete(id);
                return Ok();
            }
        }
    }
}