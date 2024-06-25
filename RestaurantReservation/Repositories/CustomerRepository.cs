using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Repositories
{
    public class CustomerRepository : IDisposable
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public CustomerRepository()
        {
            _dbContext = new RestaurantReservationDbContext();
        }

        public int CreateCustomer(string firstName, string lastName, string email, string phoneNumber)
        {
            var newCustomer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
            Console.WriteLine($"Customer created with ID: {newCustomer.CustomerId}");
            return newCustomer.CustomerId;
        }

        public void ReadCustomer(int customerId)
        {
            var customer = _dbContext.Customers.Find(customerId);

            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            Console.WriteLine($"Customer found: ID {customer.CustomerId} - {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
        }

        public void UpdateCustomer(int customerId, string newEmail, string newPhoneNumber)
        {
            var customer = _dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            customer.Email = newEmail;
            customer.PhoneNumber = newPhoneNumber;

            _dbContext.SaveChanges();
            Console.WriteLine($"Customer {customerId} updated successfully.");
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _dbContext.Customers.Find(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            _dbContext.Remove(customer);
            _dbContext.SaveChanges();
            Console.WriteLine($"Customer {customerId} deleted successfully.");
        }
    }
}
