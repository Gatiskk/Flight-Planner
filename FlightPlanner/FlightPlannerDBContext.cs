using Microsoft.EntityFrameworkCore;

namespace FlightPlanner
{
    public class FlightPlannerDBContext : DbContext
    {
        public FlightPlannerDBContext(DbContextOptions options): base (options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}