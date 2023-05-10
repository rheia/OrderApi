using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Reflection.Metadata;
using System.Diagnostics;
using OrderApi.Controllers;

namespace OrderApi.Models
{
    public class MyDbContext: DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>().UseTpcMappingStrategy();
            modelBuilder.Entity<Rental>().HasOne(_ => _.Kind).WithOne().IsRequired(true);
            modelBuilder.Entity<Rental>().HasOne(_ => _.RentalItem).WithOne().IsRequired(false);

            modelBuilder.Entity<RentalItem>().UseTphMappingStrategy();
            modelBuilder.Entity<RentalItem>().HasOne(_ => _.Kind).WithOne().IsRequired(true);

            /*
             * todo for return different rent type
              modelBuilder.Entity<Vehicle>()
                  .HasBaseType<RentalItem>()
                  .HasDiscriminator<int>("KindTypeId")
                  .HasValue<Vehicle>(1);
              */

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("Users");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.DisplayName).HasMaxLength(60).IsUnicode(false);
                entity.Property(e => e.UserName).HasMaxLength(30).IsUnicode(false);
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false);
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.CreatedDate).IsUnicode(false);
            });
        }

        public virtual DbSet<User>? Users { get; set; }

        public DbSet<Kind> Kinds { get; set; } = null!;
        public DbSet<Rental> Rentals { get; set; } = null!;

        public DbSet<RentalItem> RentalItems { get; set; } = null!;

    }
}
