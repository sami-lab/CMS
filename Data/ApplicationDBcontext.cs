using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebBuilder.Data.Models;
using WebBuilder.Data.Models.Contact;
using WebBuilder.Data.Models.User;
using Microsoft.AspNetCore.Identity;

namespace WebBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SetRelations(modelBuilder);

        }

        private void SetRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>()
   .HasMany(a => a.Catergories)
   .WithOne(b => b.Company)
   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Companies>()
          .HasMany(a => a.Products)
          .WithOne(b => b.Company)
          .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<ApplicationUser>()
          .HasOne(a => a.Companies)
          .WithOne(b => b.User)
          .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ApplicationUser>()
       .HasOne(a => a.Address)
       .WithOne(b => b.User)
       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
             .HasMany(a => a.UserStats)
             .WithOne(b => b.User)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Catergories>()
         .HasMany(a => a.Products)
         .WithOne(b => b.Categories)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Products>()
       .HasMany(a => a.Images)
       .WithOne(b => b.Product)
       .OnDelete(DeleteBehavior.Cascade);




            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasIndex(e => e.CompanyOwner).IsUnique();
            });
            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasIndex(e => e.CompanyName).IsUnique();
            });

            //modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

            //modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            //modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            //modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        }
        public DbSet<Catergories> Categories { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Address> UserAddresses { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<UsersLocation> UsersLocations { get; set; }
    }
}
