using RPL.SharedKernel;
using RPL.SharedKernel.Interfaces;
using System;

namespace RPL.Core.Entities
{
    public class Patient : BaseEntity, IAggregateRoot
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Address Address { get; set; }
    }
}
