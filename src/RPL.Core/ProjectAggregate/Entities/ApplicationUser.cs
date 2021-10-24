using Microsoft.AspNetCore.Identity;
using System;

namespace RPL.Core.ProjectAggregate.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public bool IsResetPasswordUponLoginNeeded { get; set; }

        public string VerificationCode { get; set; }

        public DateTime VerificationCodeExpiryDate { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
