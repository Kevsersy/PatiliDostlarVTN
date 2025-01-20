using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;
namespace PatiliDostlarVTN.Configurations
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
    {
        public void Configure(EntityTypeBuilder<Progress> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Metric)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Percentage)
                   .IsRequired();
        }
    }
}
