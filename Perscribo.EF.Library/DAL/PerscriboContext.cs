using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Perscribo.EF.Library.Models;

namespace Perscribo.EF.Library.DAL
{
    public class PerscriboContext : DbContext
    {
        public PerscriboContext()
            : base("PerscriboContext")
        { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Engagement> Engagements { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasRequired(pm => pm.ProjectManager)
                .WithMany(p => p.Projects)
                .WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
