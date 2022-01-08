using RPL.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RPL.Infrastructure.Data.SeedData
{
    public class PatientSeeding
    {
        public static void PopulateData(MainDbContext context)
        {
            if (!context.Patients.Any())
            {
                var patients = GetDummyData();
                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }

        public static List<Patient> GetDummyData()
        {
            return new List<Patient>
            {
                new Patient
                {
                    Name = "P. Thiha",
                    PhoneNumber = "959424432870",
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Patient
                {
                    Name = "P. Ko Saw",
                    PhoneNumber = "959756036447",
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Patient
                {
                    Name = "P. Aye Myat Min",
                    PhoneNumber = "959950129153",
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Patient
                {
                    Name = "P. Yu Mon San",
                    PhoneNumber = "959441022791",
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                }
            };
        }
    }
}
