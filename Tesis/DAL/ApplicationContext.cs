using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
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

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");//.Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<Semester>().HasMany(x => x.Sections).WithOptional().HasForeignKey(e => e.SemesterId).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Product>().ToTable("Products");
            ////modelBuilder.Entity<Section>().ToTable("Sections");
            //modelBuilder.Entity<Semester>().ToTable("Semesters");
            ////modelBuilder.Entity<Group>().ToTable("Groups");
            //modelBuilder.Entity<InitialCharge>().ToTable("InitialCharges");
            //modelBuilder.Entity<ApplicationUser>().HasOptional(c => c.Section).WithMany(t => t.Users).Map(m => m.MapKey("Section_Id"));
            //modelBuilder.Entity<CustomRole>().HasMany(c => c.Menus).WithMany(p => p.Roles).Map(m => { m.MapLeftKey("RoleId"); m.MapRightKey("MenuId"); m.ToTable("MenusRoles"); });
            //modelBuilder.Entity<Product>().HasRequired(c => c.User).WithMany(t => t.Products).Map(m => m.MapKey("UserId"));
        }
    }
}