using RPL.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPL.Core.DTOs.Bookings
{
    public class BookingInformationDto
    {
        public long Id { get; set; }

        public int BookingNumber { get; set; }

        public string ClinicName { get; set; }

        public string DoctorName { get; set; }

        public string Schedule { get; set; }
       


    }
}
