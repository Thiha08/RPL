using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPL.Core.Entities;

namespace RPL.Infrastructure.Data.Config
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.Property(x => x.ClinicName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.OwnsOne(x => x.ClinicAddress);

            builder.HasIndex(x => x.ClinicName);

            builder.HasIndex(x => x.PhoneNumber);
        }
    }
}
