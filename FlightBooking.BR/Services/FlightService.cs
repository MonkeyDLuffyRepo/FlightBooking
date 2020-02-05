using FlightBooking.BR.GeoHelper;
using FlightBooking.BR.Interfaces;
using FlightBooking.Entities.Context;
using FlightBooking.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.BR.Services
{
    public class FlightService : IFlightInterface
    {
        private readonly FlightDBContext _context;
        public FlightService(FlightDBContext context)
        {
            _context = context;
        }
        // get all Flights
        public IEnumerable<Flight> GetAllFlight()
        {
            var flights = _context.Flights
                  .Include(fl => fl.Plane)
                  .Include(fl => fl.FlightFrom)
                  .Include(fl => fl.FlightTo)
                  .Where(fl => !fl.IsDeleted)
                  .ToList();

            return flights;
        }
        // get flight by id
        public Flight GetFlightById(int flightId)
        {
            var flight = _context.Flights
                    .Include(fl => fl.Plane)
                    .Include(fl => fl.FlightFrom)
                    .Include(fl => fl.FlightTo)
                    .FirstOrDefault(fl => fl.Id == flightId && !fl.IsDeleted);

            return flight;
        }
        //get all Planes
        public IEnumerable<Plane> GetAllPlane()
        {
            var planes = _context.Planes.ToList();
            return planes;
        }
        //get all Airports
        public IEnumerable<Airport> GetAllAirport()
        {
            var airports = _context.Airports.ToList();
            return airports;
        }
        // delete flight by id
        public bool DeleteFlight(int flightId)
        {
            var flight = _context.Flights.FirstOrDefault(fl => fl.Id == flightId);
            if (flight == null) return false;
            flight.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
        // create flight
        public double CreateFlight(Flight flightModel)
        {
            flightModel.IsDeleted = false;
            flightModel.CreationDate = DateTime.Now;
            flightModel.FlightComsuption = CalculateComsumption(flightModel);
            _context.Flights.Add(flightModel);
            _context.SaveChanges();

            return flightModel.Id;
        }
        public double UpdateFlight(Flight flightModel)
        {
            var flight = _context.Flights
                 .Where(fl =>fl.Id == flightModel.Id && !fl.IsDeleted)
                 .FirstOrDefault();

            if (flight == null) return 0;

            flight.PlaneId = flightModel.PlaneId;
            flight.FlightFromId = flightModel.FlightFromId;
            flight.FlightToId = flightModel.FlightToId;
            flight.FlightDuration = flightModel.FlightDuration;
            flight.FlightStartTime = flightModel.FlightStartTime;
            flight.FlightComsuption = CalculateComsumption(flight);

            _context.Entry(flight).State = EntityState.Modified;
            _context.SaveChanges();
            return flight.Id;
        }
        //calculate Comsuption
        public double CalculateComsumption(Flight flight)
        {
        
            var plane = GetPlaneById(flight.PlaneId);
            var airportFrom = GetAirportById(flight.FlightFromId);
            var airportTo = GetAirportById(flight.FlightToId);
            if (plane == null || airportFrom == null || airportTo == null) return 0;
            var distance = CalculateDistanceBetweenAirports(airportFrom, airportTo);

            return ((distance / plane.Speed) * plane.ComsumptionRate) + plane.ComsumptionEffort;
        }
        //get Plane by id
        private Plane GetPlaneById(int planeId)
        {
            var plane = _context.Planes
                  
                    .FirstOrDefault(pl => pl.Id == planeId);

            return plane;
        }
        //get Airport by id
        private Airport GetAirportById(int airportId)
        {
            var airport = _context.Airports
                    .FirstOrDefault(pl => pl.Id == airportId);

            return airport;
        }
        //calculate Distance between airports
        private double CalculateDistanceBetweenAirports(Airport from, Airport to)
        {
            var geoFrom = new GeoCoordinate() { Latitude = from.Latitude, Longitude = from.Longitude };
            var geoTo = new GeoCoordinate() { Latitude = to.Latitude, Longitude = to.Longitude };

            return GeoCoordinateHelper.Distance(geoFrom, geoTo,2);
        }
    }
}
