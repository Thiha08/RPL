using RPL.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RPL.Infrastructure.Data.Initializer
{
    public class IdentityDbInitializer
    {
        public static void Initialize(IdentityDbContext context)
        {
            context.Database.EnsureCreated();
        }

        public static void PopulateUsersData(IdentityDbContext context)
        {
            if (!context.Users.Any())
            {
                var users = GetDummyPatientUsers();

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        public static List<ApplicationUser> GetDummyPatientUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser { UserName = "959424432870", FullName = "P. Thiha", EmailConfirmed = true, PhoneNumber = "959424432870", IsResetPasswordUponLoginNeeded = false },
                new ApplicationUser { UserName = "959756036447", FullName = "P. Ko Saw", EmailConfirmed = true, PhoneNumber = "959756036447", IsResetPasswordUponLoginNeeded = false },
                new ApplicationUser { UserName = "959950129153", FullName = "P. Aye Myat Min", EmailConfirmed = true, PhoneNumber = "959950129153", IsResetPasswordUponLoginNeeded = false },
                new ApplicationUser { UserName = "959441022791", FullName = "P. Yu Mon San", EmailConfirmed = true, PhoneNumber = "959441022791", IsResetPasswordUponLoginNeeded = false }
            };
        }
    }
}
