using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Entities.Models
{
    public partial class Plane
    {
        public Plane()
        {
            Flights = new HashSet<Flight>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double ComsumptionEffort { get; set; }
        public double ComsumptionRate { get; set; }

        public double Speed { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }
}
