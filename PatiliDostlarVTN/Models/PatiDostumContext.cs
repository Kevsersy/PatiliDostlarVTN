using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatiliDostlarVTN.Controllers;
using PatiliDostlarVTN.Models.Entities;




namespace PatiliDostlarVTN.Models
{
    public class PatiDostumContext : DbContext
    {
        public PatiDostumContext(DbContextOptions<PatiDostumContext> options)
            : base(options)
        {
        }

        // Tablo tanımları
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<ServicePT> Services { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Login> Logins{ get; set; }
        public DbSet<deneme> Denemes { get; set; }
      















        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<PatiliDostlarVTN.Models.Entities.FeaturedWork> FeaturedWork { get; set; } = default!;
        public DbSet<PatiliDostlarVTN.Models.Entities.RecentWork> RecentWork { get; set; } = default!;

    }

    
}
