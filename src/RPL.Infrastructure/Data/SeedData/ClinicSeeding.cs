using RPL.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RPL.Infrastructure.Data.SeedData
{
    public class ClinicSeeding
    {
        public static void PopulateData(MainDbContext context)
        {
            if (!context.Clinics.Any())
            {
                var clinics = GetDummyData();
                context.Clinics.AddRange(clinics);
                context.SaveChanges();
            }
        }

        public static List<Clinic> GetDummyData()
        {
            return new List<Clinic>
            {
                new Clinic
                {
                    ClinicName = "Aung Clinic - Baho Street",
                    PhoneNumber = "959402288410",
                    ClinicAddress = new Address
                    {
                        AddressBody = "215 Baho Street, Yangon",
                        Latitude = 16.833063004159218,
                        Longitude = 96.12319636412894
                    },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },

                new Clinic
                {
                    ClinicName = "Myat Myint Mo Clinic",
                    PhoneNumber = "959886008003",
                    ClinicAddress = new Address
                    {
                        AddressBody = "Aung Myintar 4 Street, Hling ward 1, 11501",
                        Latitude = 16.836266946423653,
                        Longitude = 96.1260287768032
                    },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },

                new Clinic
                {
                    ClinicName = "SSB Clinic (Kamaryut)",
                    PhoneNumber = "951535270",
                    ClinicAddress = new Address
                    {
                        AddressBody = "Insein Rd, Yangon",
                        Latitude = 16.83176180724997,
                        Longitude = 96.12944078604244
                    },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },

                new Clinic
                {
                    ClinicName = "Fever Clinic Hlaing",
                    PhoneNumber = "95943067059",
                    ClinicAddress = new Address
                    {
                        AddressBody = "R4QF+XRW, Yangon",
                        Latitude = 16.840123337086098,
                        Longitude = 96.12462000299159
                    },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },

                new Clinic
                {
                    ClinicName = "Dr Maung Maung Oo Clinic",
                    PhoneNumber = "9598573020",
                    ClinicAddress = new Address
                    {
                        AddressBody = "40, Mahar Social street",
                        Latitude = 16.826074949848017, 
                        Longitude = 96.12687617902068
                    },
                    CreatedBy = "seed",
                    UpdatedBy = "seed"
                },
            };
        }
    }
}
