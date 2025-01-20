using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class AboutUsConfiguration : IEntityTypeConfiguration<AboutUs>
    {
        public void Configure(EntityTypeBuilder<AboutUs> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasColumnType("text");
        }
    }
}

