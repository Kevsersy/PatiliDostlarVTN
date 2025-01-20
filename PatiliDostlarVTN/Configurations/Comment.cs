using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Models.Entities;

namespace PatiliDostlarVTN.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.AvatarUrl)
                   .HasMaxLength(255);
            builder.Property(x => x.TimeAgo)
                   .HasMaxLength(50);
            builder.Property(x => x.Message)
                   .IsRequired()
                   .HasColumnType("text");
        }
    }
}
