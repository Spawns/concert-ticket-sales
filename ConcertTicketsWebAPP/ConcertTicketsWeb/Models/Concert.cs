using System;
using System.Collections.Generic;

namespace ConcertTicketsWeb.Models
{
    public class Concert
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
     
        public virtual List<Reservation> Reservations { get; set; }    
    }

}