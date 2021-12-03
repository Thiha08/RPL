using RPL.SharedKernel;
using RPL.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace RPL.Core.Entities
{
    public class Clinic : BaseEntity, IAggregateRoot
    {
        public string ClinicName { get; set; }

        public Address ClinicAddress { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
