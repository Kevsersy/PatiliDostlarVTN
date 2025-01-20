using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.Role)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.ImagePath)
                   .HasMaxLength(255);
        }
    }
}
