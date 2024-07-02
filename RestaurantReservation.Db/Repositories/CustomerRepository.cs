using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class CustomerRepository : IDisposable, ICustomerRepository
    {
        private readonly RestaurantReservationDbContext _dbContext;

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        public CustomerRepository(RestaurantReservationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(int customerId, bool includeLists)
        {
            if (includeLists)
            {
                return await _dbContext.Customers
                    .Include(c => c.Reservations)
                    .ThenInclude(r => r.Restaurant)
                    .Include(c => c.Reservations)
                    .ThenInclude(r => r.Table)
                    .Where(c => c.CustomerId == customerId)
                    .FirstOrDefaultAsync();
            }
            return await _dbContext.Customers
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefaultAsync();
        }

        public void CreateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Add(customer);
        }

        public void DeleteCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Remove(customer);
        }

        public async Task<List<Customer>> CustomerWithPartySizeGreaterThanAsync(int partySize)
        {
            var partySizeParam = new SqlParameter("@partysize", partySize);
            List<Customer> customers = await _dbContext.Customers
                .FromSqlRaw("sp_CustomersWithPartySizeGreaterThan @partysize", partySizeParam)
                .ToListAsync();
            return customers;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
