using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPL.Core.DTOs.Bookings
{
    public class ConfirmBookingDto
    {
        public long BookingId { get; set; }

        public string Description { get; set; }
    }
}
