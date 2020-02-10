using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.BR.ViewModels
{
   public  class FlightViewModel
    {
        public int Id { get; set; }
        public int PlaneId { get; set; }
        public int FlightFromId { get; set; }
        public int FlightToId { get; set; }
        public double FlightComsuption { get; set; }
        public double FlightDuration { get; set; }
        public double FlightDistance { get; set; }
      
    }
}
