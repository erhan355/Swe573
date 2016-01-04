using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WforViolation.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Comments = new List<Comment>();
            this.Violations = new List<Violation>();

        }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual UserPicture Picture { get; set; }
        public virtual List<Violation> Violations { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Violation> Violations { get; set; }
        public DbSet<UserPicture> UserPictures { get; set; }

        public DbSet<ViolationPicture> ViolationPictures { get; set; }

        public DbSet<ViolationMainType> ViolationMainTypes { get; set; }
        public DbSet<ViolationSubType> ViolationSubTypes { get; set; }

        public DbSet<ViolationDetail> ViolationDetails { get; set; }

        public DbSet<Abuse> Abuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPicture>()
                .HasRequired(lu => lu.ApplicationUser)
             .WithOptional(pi => pi.Picture);
            base.OnModelCreating(modelBuilder);
        }
    }
}