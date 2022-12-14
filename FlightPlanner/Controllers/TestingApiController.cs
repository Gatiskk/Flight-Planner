using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        private readonly FlightPlannerDBContext _dbContext;
        public TestingApiController(FlightPlannerDBContext context)
        {
            _dbContext = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _dbContext.Flights.RemoveRange(_dbContext.Flights);
            _dbContext.Airports.RemoveRange(_dbContext.Airports);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
