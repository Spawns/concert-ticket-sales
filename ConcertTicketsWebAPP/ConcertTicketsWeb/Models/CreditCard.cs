using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertTicketsWeb.Models
{
    public class CreditCard
    {

        public int ID { get; set; }
        public string Number { get; set; }
        public string ValidThru { get; set; }
        public string CVV { get; set; }

        public int ReservationID { get; set; }
        public virtual Reservation Reservation { get; set; }

        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}