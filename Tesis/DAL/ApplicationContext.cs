using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Tesis.Models;

namespace Tesis.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(): base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<CaseStudy> CaseStudies { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<InitialCharge> InitialCharges { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Section> Sections { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }

        public virtual DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<Semester>().HasMany(x => x.Sections).WithOptional().HasForeignKey(e => e.SemesterId).WillCascadeOnDelete(false);
        }
    }
}