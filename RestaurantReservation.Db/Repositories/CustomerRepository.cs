using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
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

        public async Task<int> CreateCustomer(string firstName, string lastName, string email, string phoneNumber)
        {
            var newCustomer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Customer created with ID: {newCustomer.CustomerId}");
            return newCustomer.CustomerId;
        }

        public async Task ReadCustomer(int customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);

            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            Console.WriteLine($"Customer found: ID {customer.CustomerId} - {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
        }

        public async Task UpdateCustomer(int customerId, string newEmail, string newPhoneNumber)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            customer.Email = newEmail;
            customer.PhoneNumber = newPhoneNumber;

            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Customer {customerId} updated successfully.");
        }

        public async Task DeleteCustomer(int customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {customerId} not found.");
                return;
            }
            _dbContext.Remove(customer);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"Customer {customerId} deleted successfully.");
        }
    }
}
