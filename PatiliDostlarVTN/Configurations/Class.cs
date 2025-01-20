using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.Phone)
                   .HasMaxLength(20);
            builder.Property(x => x.Company)
                   .HasMaxLength(100);
           
        }
    }
}
