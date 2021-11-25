using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPL.Core.Entities;

namespace RPL.Infrastructure.Data.Config
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.OwnsOne(p => p.Address);
        }
    }
}
