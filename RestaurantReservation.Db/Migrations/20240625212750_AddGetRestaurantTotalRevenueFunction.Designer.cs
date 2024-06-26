﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantReservation.Db;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    [DbContext(typeof(RestaurantReservationDbContext))]
    [Migration("20240625212750_AddGetRestaurantTotalRevenueFunction")]
    partial class AddGetRestaurantTotalRevenueFunction
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantReservation.Db.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = "1234567890"
                        },
                        new
                        {
                            CustomerId = 2,
                            Email = "jane.doe@example.com",
                            FirstName = "Jane",
                            LastName = "Doe",
                            PhoneNumber = "1234567891"
                        },
                        new
                        {
                            CustomerId = 3,
                            Email = "bob.smith@example.com",
                            FirstName = "Bob",
                            LastName = "Smith",
                            PhoneNumber = "1234567892"
                        },
                        new
                        {
                            CustomerId = 4,
                            Email = "alice.johnson@example.com",
                            FirstName = "Alice",
                            LastName = "Johnson",
                            PhoneNumber = "1234567893"
                        },
                        new
                        {
                            CustomerId = 5,
                            Email = "charlie.brown@example.com",
                            FirstName = "Charlie",
                            LastName = "Brown",
                            PhoneNumber = "1234567894"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            FirstName = "Sam",
                            LastName = "Wilson",
                            Position = "Manager",
                            RestaurantId = 1
                        },
                        new
                        {
                            EmployeeId = 2,
                            FirstName = "Chris",
                            LastName = "Evans",
                            Position = "Chef",
                            RestaurantId = 1
                        },
                        new
                        {
                            EmployeeId = 3,
                            FirstName = "Paul",
                            LastName = "Rudd",
                            Position = "Waiter",
                            RestaurantId = 2
                        },
                        new
                        {
                            EmployeeId = 4,
                            FirstName = "Mark",
                            LastName = "Ruffalo",
                            Position = "Manager",
                            RestaurantId = 2
                        },
                        new
                        {
                            EmployeeId = 5,
                            FirstName = "Tom",
                            LastName = "Holland",
                            Position = "Waiter",
                            RestaurantId = 3
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.EmployeeWithRestaurantDetails", b =>
                {
                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OpeningHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("EmployeesWithRestaurantDetails", (string)null);
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("MenuItemId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            MenuItemId = 1,
                            Description = "Juicy beef burger",
                            Name = "Burger",
                            Price = 10.99m,
                            RestaurantId = 1
                        },
                        new
                        {
                            MenuItemId = 2,
                            Description = "Creamy Alfredo pasta",
                            Name = "Pasta",
                            Price = 12.99m,
                            RestaurantId = 1
                        },
                        new
                        {
                            MenuItemId = 3,
                            Description = "Pepperoni pizza",
                            Name = "Pizza",
                            Price = 8.99m,
                            RestaurantId = 2
                        },
                        new
                        {
                            MenuItemId = 4,
                            Description = "Caesar salad",
                            Name = "Salad",
                            Price = 7.99m,
                            RestaurantId = 2
                        },
                        new
                        {
                            MenuItemId = 5,
                            Description = "Grilled steak",
                            Name = "Steak",
                            Price = 15.99m,
                            RestaurantId = 3
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            EmployeeId = 2,
                            OrderDate = new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservationId = 1,
                            TotalAmount = 50.00m
                        },
                        new
                        {
                            OrderId = 2,
                            EmployeeId = 1,
                            OrderDate = new DateTime(2024, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservationId = 2,
                            TotalAmount = 70.00m
                        },
                        new
                        {
                            OrderId = 3,
                            EmployeeId = 4,
                            OrderDate = new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservationId = 3,
                            TotalAmount = 40.00m
                        },
                        new
                        {
                            OrderId = 4,
                            EmployeeId = 3,
                            OrderDate = new DateTime(2024, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservationId = 4,
                            TotalAmount = 30.00m
                        },
                        new
                        {
                            OrderId = 5,
                            EmployeeId = 5,
                            OrderDate = new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReservationId = 5,
                            TotalAmount = 80.00m
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemId = 1,
                            MenuItemId = 1,
                            OrderId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderItemId = 2,
                            MenuItemId = 2,
                            OrderId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            OrderItemId = 3,
                            MenuItemId = 3,
                            OrderId = 2,
                            Quantity = 3
                        },
                        new
                        {
                            OrderItemId = 4,
                            MenuItemId = 4,
                            OrderId = 2,
                            Quantity = 2
                        },
                        new
                        {
                            OrderItemId = 5,
                            MenuItemId = 5,
                            OrderId = 3,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("PartySize")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            ReservationId = 1,
                            CustomerId = 1,
                            PartySize = 4,
                            ReservationDate = new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            TableId = 1
                        },
                        new
                        {
                            ReservationId = 2,
                            CustomerId = 2,
                            PartySize = 6,
                            ReservationDate = new DateTime(2024, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 1,
                            TableId = 2
                        },
                        new
                        {
                            ReservationId = 3,
                            CustomerId = 3,
                            PartySize = 4,
                            ReservationDate = new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 2,
                            TableId = 3
                        },
                        new
                        {
                            ReservationId = 4,
                            CustomerId = 4,
                            PartySize = 2,
                            ReservationDate = new DateTime(2024, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 2,
                            TableId = 4
                        },
                        new
                        {
                            ReservationId = 5,
                            CustomerId = 5,
                            PartySize = 4,
                            ReservationDate = new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RestaurantId = 3,
                            TableId = 5
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.ReservationWithDetails", b =>
                {
                    b.Property<string>("CustomerFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartySize")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("ReservationsWithDetails", (string)null);
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("OpeningHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RestaurantId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            RestaurantId = 1,
                            Address = "123 Main St",
                            Name = "The Great Eatery",
                            OpeningHours = "9AM - 9PM",
                            PhoneNumber = "555-1234"
                        },
                        new
                        {
                            RestaurantId = 2,
                            Address = "456 Elm St",
                            Name = "Food Palace",
                            OpeningHours = "10AM - 10PM",
                            PhoneNumber = "555-5678"
                        },
                        new
                        {
                            RestaurantId = 3,
                            Address = "789 Oak St",
                            Name = "Dine Fine",
                            OpeningHours = "8AM - 8PM",
                            PhoneNumber = "555-8765"
                        },
                        new
                        {
                            RestaurantId = 4,
                            Address = "101 Maple St",
                            Name = "Gourmet Hub",
                            OpeningHours = "11AM - 11PM",
                            PhoneNumber = "555-5432"
                        },
                        new
                        {
                            RestaurantId = 5,
                            Address = "202 Pine St",
                            Name = "Taste Buds",
                            OpeningHours = "7AM - 7PM",
                            PhoneNumber = "555-0987"
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("TableId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            TableId = 1,
                            Capacity = 4,
                            RestaurantId = 1
                        },
                        new
                        {
                            TableId = 2,
                            Capacity = 6,
                            RestaurantId = 1
                        },
                        new
                        {
                            TableId = 3,
                            Capacity = 4,
                            RestaurantId = 2
                        },
                        new
                        {
                            TableId = 4,
                            Capacity = 2,
                            RestaurantId = 2
                        },
                        new
                        {
                            TableId = 5,
                            Capacity = 4,
                            RestaurantId = 3
                        });
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("Employees")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("MenuItems")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Reservation", "Reservation")
                        .WithMany("Orders")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.OrderItem", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("Reservations")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RestaurantReservation.Db.Models.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.HasOne("RestaurantReservation.Db.Models.Restaurant", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Employee", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Reservation", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Restaurant", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("MenuItems");

                    b.Navigation("Reservations");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("RestaurantReservation.Db.Models.Table", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
