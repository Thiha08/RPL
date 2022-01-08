using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPL.Infrastructure.Data.SeedData;
using System;

namespace RPL.Infrastructure.Data.Initializer
{
    public class MainDbInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var dbContext = new MainDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<MainDbContext>>(), null))
            {
                dbContext.Database.EnsureCreated();

                ClinicSeeding.PopulateData(dbContext);
                PatientSeeding.PopulateData(dbContext);
            } 
        }
    }
}
