using System.Collections.Generic;

namespace CapstoneProject.Models
{
    public class TruckerAndTruckViewModel
    {
        public IEnumerable<Trucker> Truckers { get; set; }
        public IEnumerable<Truck> Trucks { get; set; }
    }
}
