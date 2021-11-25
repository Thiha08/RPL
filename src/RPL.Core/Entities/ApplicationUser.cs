using Microsoft.AspNetCore.Identity;
using System;

namespace RPL.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public bool IsResetPasswordUponLoginNeeded { get; set; }

        public string VerificationCode { get; set; }

        public DateTime VerificationCodeExpiryDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual string UpdatedBy { get; set; }

        public bool Status { get; set; }
    }
}
