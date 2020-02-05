using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Entities.Models
{
   public partial  class Flight
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public int FlightFromId { get; set; }
        public int FlightToId { get; set; }
        public double FlightComsuption { get; set; }
        public Plane Plane { get; set; }
        public Airport FlightFrom { get; set; }
        public Airport FlightTo { get; set; }
        public DateTime FlightStartTime { get; set; }
        //flight duration in hours
        public double FlightDuration { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
