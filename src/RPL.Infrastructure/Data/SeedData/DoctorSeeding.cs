using RPL.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RPL.Infrastructure.Data.SeedData
{
    public class DoctorSeeding
    {
        public static void PopulateData(MainDbContext context)
        {
            if (!context.Doctors.Any())
            {
                var doctors = GetDummyData();
                context.Doctors.AddRange(doctors);
                context.SaveChanges();
            }
        }

        public static List<Doctor> GetDummyData()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    Name = "D. Thiha",
                    PhoneNumber = "959424432870",                    
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                
            };
        }
    }
}
