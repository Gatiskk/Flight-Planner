using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations
{
    public interface IFlightValidator
    {
        public bool IsValid(Flight flight);
    }
}
