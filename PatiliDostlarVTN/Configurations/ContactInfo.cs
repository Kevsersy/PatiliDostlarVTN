using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Role)
                   .HasMaxLength(100);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);

        }
    }
}
