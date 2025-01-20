using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Title)
                   .IsRequired()
                   .HasMaxLength(200);
            builder.Property(x => x.Description)
                   .IsRequired()
                   .HasColumnType("text");

            builder.Property(x => x.WorkType)
                   .HasMaxLength(100);
            builder.Property(x => x.DetailLink)
                   .HasMaxLength(255);
        }
    }
}
