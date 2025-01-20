using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<ServicePT>
    {
        public void Configure(EntityTypeBuilder<ServicePT> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(x => x.Category)
                   .HasMaxLength(100);
            builder.Property(x => x.Link)
                   .HasMaxLength(255);
        }
    }
}
