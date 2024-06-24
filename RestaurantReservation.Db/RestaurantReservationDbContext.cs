using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db
{
    public class RestaurantReservationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // later remove
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = localhost\\SQLEXPRESS; Initial Catalog = RestaurantReservationCore; Trusted_Connection=True; TrustServerCertificate=True;"
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>()
              .Property(mi => mi.Price)
              .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
              .Property(mi => mi.TotalAmount)
              .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany(rest => rest.Reservations)
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(rest => rest.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(r => r.Order)
                .WithMany(rest => rest.OrderItems)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
