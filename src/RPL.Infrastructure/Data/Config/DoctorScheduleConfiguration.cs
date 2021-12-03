using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPL.Core.Entities;

namespace RPL.Infrastructure.Data.Config
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.HasOne(sl => sl.Doctor)
                .WithMany(d => d.DoctorSchedule)
                .HasForeignKey(sl => sl.DoctorId);

        }
    }
}
