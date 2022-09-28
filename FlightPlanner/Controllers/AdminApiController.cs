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
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
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

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult FlightDelete(int id)
        {
            FlightStorage.FlightDelete(id);
            return Ok();
        }
    }
}