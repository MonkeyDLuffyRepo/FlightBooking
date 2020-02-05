using FlightBooking.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.BR.Interfaces
{
   public  interface IFlightInterface
    {
        IEnumerable<Flight> GetAllFlight();
        IEnumerable<Plane> GetAllPlane();
        IEnumerable<Airport> GetAllAirport();
        Flight GetFlightById(int flightId);
        double CreateFlight(Flight flightModel);
        double UpdateFlight(Flight flightModel);
        bool DeleteFlight(int flightId);
        double CalculateComsumption(Flight flight);


    }
}
