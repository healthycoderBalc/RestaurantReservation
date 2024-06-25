using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var customerList = new Customer[]
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", 
                    Email = "john.doe@example.com", PhoneNumber = "1234567890" },
                new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Doe",
                    Email = "jane.doe@example.com", PhoneNumber = "1234567891" },
                new Customer { CustomerId = 3, FirstName = "Bob", LastName = "Smith",
                    Email = "bob.smith@example.com", PhoneNumber = "1234567892" },
                new Customer { CustomerId = 4, FirstName = "Alice", LastName = "Johnson",
                    Email = "alice.johnson@example.com", PhoneNumber = "1234567893" },
                new Customer { CustomerId = 5, FirstName = "Charlie", LastName = "Brown",
                    Email = "charlie.brown@example.com", PhoneNumber = "1234567894" }
            };

            var restaurantList = new Restaurant[]
            {
                new Restaurant { RestaurantId = 1, Name = "The Great Eatery", Address = "123 Main St",
                    PhoneNumber = "555-1234", OpeningHours = "9AM - 9PM" },
                new Restaurant { RestaurantId = 2, Name = "Food Palace", Address = "456 Elm St",
                    PhoneNumber = "555-5678", OpeningHours = "10AM - 10PM" },
                new Restaurant { RestaurantId = 3, Name = "Dine Fine", Address = "789 Oak St",
                    PhoneNumber = "555-8765", OpeningHours = "8AM - 8PM" },
                new Restaurant { RestaurantId = 4, Name = "Gourmet Hub", Address = "101 Maple St",
                    PhoneNumber = "555-5432", OpeningHours = "11AM - 11PM" },
                new Restaurant { RestaurantId = 5, Name = "Taste Buds", Address = "202 Pine St",
                    PhoneNumber = "555-0987", OpeningHours = "7AM - 7PM" }
            };

            var tableList = new Table[]
            {
                new Table { TableId = 1, RestaurantId = 1, Capacity = 4 },
                new Table { TableId = 2, RestaurantId = 1, Capacity = 6 },
                new Table { TableId = 3, RestaurantId = 2, Capacity = 4 },
                new Table { TableId = 4, RestaurantId = 2, Capacity = 2 },
                new Table { TableId = 5, RestaurantId = 3, Capacity = 4 }
            };

            var employeeList = new Employee[]
            {
                new Employee { EmployeeId = 1, RestaurantId = 1, FirstName = "Sam", LastName = "Wilson",
                    Position = "Manager" },
                new Employee { EmployeeId = 2, RestaurantId = 1, FirstName = "Chris", LastName = "Evans",
                    Position = "Chef" },
                new Employee { EmployeeId = 3, RestaurantId = 2, FirstName = "Paul", LastName = "Rudd",
                    Position = "Waiter" },
                new Employee { EmployeeId = 4, RestaurantId = 2, FirstName = "Mark", LastName = "Ruffalo",
                    Position = "Manager" },
                new Employee { EmployeeId = 5, RestaurantId = 3, FirstName = "Tom", LastName = "Holland",
                    Position = "Waiter" }
            };

            var menuItemList = new MenuItem[]
            {
                new MenuItem { MenuItemId = 1, RestaurantId = 1, Name = "Burger",
                    Description = "Juicy beef burger", Price = 10.99m },
                new MenuItem { MenuItemId = 2, RestaurantId = 1, Name = "Pasta",
                    Description = "Creamy Alfredo pasta", Price = 12.99m },
                new MenuItem { MenuItemId = 3, RestaurantId = 2, Name = "Pizza",
                    Description = "Pepperoni pizza", Price = 8.99m },
                new MenuItem { MenuItemId = 4, RestaurantId = 2, Name = "Salad",
                    Description = "Caesar salad", Price = 7.99m },
                new MenuItem { MenuItemId = 5, RestaurantId = 3, Name = "Steak",
                    Description = "Grilled steak", Price = 15.99m }
            };

            var reservationList = new Reservation[] {
                new Reservation { ReservationId = 1, CustomerId = 1, RestaurantId = 1, TableId = 1,
                    ReservationDate = new DateTime(2024, 6, 26), PartySize = 4 },
                new Reservation { ReservationId = 2, CustomerId = 2, RestaurantId = 1, TableId = 2,
                    ReservationDate = new DateTime(2024, 6, 27), PartySize = 6 },
                new Reservation { ReservationId = 3, CustomerId = 3, RestaurantId = 2, TableId = 3,
                    ReservationDate = new DateTime(2024, 6, 28), PartySize = 4 },
                new Reservation { ReservationId = 4, CustomerId = 4, RestaurantId = 2, TableId = 4,
                    ReservationDate = new DateTime(2024, 6, 29), PartySize = 2 },
                new Reservation { ReservationId = 5, CustomerId = 5, RestaurantId = 3, TableId = 5,
                    ReservationDate = new DateTime(2024, 6, 30), PartySize = 4 }
            };

            var orderList = new Order[]
            {
                new Order { OrderId = 1, ReservationId = 1, EmployeeId = 2,
                       OrderDate = new DateTime(2024, 6, 26), TotalAmount = 50.00m },
                new Order { OrderId = 2, ReservationId = 2, EmployeeId = 1,
                    OrderDate = new DateTime(2024, 6, 27), TotalAmount = 70.00m },
                new Order { OrderId = 3, ReservationId = 3, EmployeeId = 4,
                    OrderDate = new DateTime(2024, 6, 28), TotalAmount = 40.00m },
                new Order { OrderId = 4, ReservationId = 4, EmployeeId = 3,
                    OrderDate = new DateTime(2024, 6, 29), TotalAmount = 30.00m },
                new Order { OrderId = 5, ReservationId = 5, EmployeeId = 5,
                    OrderDate = new DateTime(2024, 6, 30), TotalAmount = 80.00m }
            };

            var orderItemList = new OrderItem[]
            {
                new OrderItem { OrderItemId = 1, OrderId = 1, MenuItemId = 1, Quantity = 2 },
                new OrderItem { OrderItemId = 2, OrderId = 1, MenuItemId = 2, Quantity = 1 },
                new OrderItem { OrderItemId = 3, OrderId = 2, MenuItemId = 3, Quantity = 3 },
                new OrderItem { OrderItemId = 4, OrderId = 2, MenuItemId = 4, Quantity = 2 },
                new OrderItem { OrderItemId = 5, OrderId = 3, MenuItemId = 5, Quantity = 1 }
            };

            modelBuilder.Entity<Customer>().HasData(customerList);
            modelBuilder.Entity<Restaurant>().HasData(restaurantList);
            modelBuilder.Entity<Table>().HasData(tableList);
            modelBuilder.Entity<Employee>().HasData(employeeList);
            modelBuilder.Entity<MenuItem>().HasData(menuItemList);
            modelBuilder.Entity<Reservation>().HasData(reservationList);
            modelBuilder.Entity<Order>().HasData(orderList);
            modelBuilder.Entity<OrderItem>().HasData(orderItemList);

        }

    }
}
