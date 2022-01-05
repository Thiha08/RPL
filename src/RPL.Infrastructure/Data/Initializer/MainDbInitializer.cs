using RPL.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RPL.Infrastructure.Data.Initializer
{
    public class MainDbInitializer
    {
        public static void Initialize(MainDbContext context)
        {
            context.Database.EnsureCreated();
        }

        public static void PopulatePatientsData(MainDbContext context)
        {
            if (!context.Patients.Any())
            {
                var patients = GetDummyPatients();
                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }

        public static List<Patient> GetDummyPatients()
        {
            return new List<Patient>
            {
                new Patient { Name = "P. Thiha", PhoneNumber = "959424432870", CreatedBy = "seed", UpdatedBy = "seed" },
                new Patient { Name = "P. Ko Saw", PhoneNumber = "959756036447", CreatedBy = "seed", UpdatedBy = "seed" },
                new Patient { Name = "P. Aye Myat Min", PhoneNumber = "959950129153", CreatedBy = "seed", UpdatedBy = "seed" },
                new Patient { Name = "P. Yu Mon San", PhoneNumber = "959441022791", CreatedBy = "seed", UpdatedBy = "seed" }
            };
        }

        public static List<Clinic> GetDummyClinics()
        {
            return new List<Clinic>
            {
                new Clinic
                {
                    ClinicName = "Aung Clinic - Baho Street",
                    PhoneNumber = "959402288410",
                    ClinicAddress = new Address { AddressBody = "215 Baho Street, Yangon", Latitude = 16.833063004159218, Longitude =  96.12319636412894 },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Clinic
                {
                    ClinicName = "Aung Clinic - Baho Street",
                    PhoneNumber = "959402288410",
                    ClinicAddress = new Address { AddressBody = "", Latitude = 0, Longitude = 0 },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Clinic
                {
                    ClinicName = "Aung Clinic - Baho Street",
                    PhoneNumber = "959402288410",
                    ClinicAddress = new Address { AddressBody = "", Latitude = 0, Longitude = 0 },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Clinic
                {
                    ClinicName = "Aung Clinic - Baho Street",
                    PhoneNumber = "959402288410",
                    ClinicAddress = new Address { AddressBody = "", Latitude = 0, Longitude = 0 },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
                new Clinic
                {
                    ClinicName = "Aung Clinic - Baho Street",
                    PhoneNumber = "959402288410",
                    ClinicAddress = new Address { AddressBody = "", Latitude = 0, Longitude = 0 },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
            };
        }
    }
}
