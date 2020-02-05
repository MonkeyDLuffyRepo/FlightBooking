using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Entities.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightFroms = new HashSet<Flight>();
            FlightTos = new HashSet<Flight>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Flight> FlightFroms { get; set; }

        public virtual ICollection<Flight> FlightTos { get; set; }
    }
}
