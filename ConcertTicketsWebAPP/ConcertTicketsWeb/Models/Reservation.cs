using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertTicketsWeb.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        public string status { get; set; }
        
        public int SeatID { get; set; }
        public virtual Seat Seat { get; set; }

        public int ConcertID { get; set; }
        public virtual Concert Concert { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}