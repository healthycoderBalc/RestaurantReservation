using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(int customerId, bool includeReservations);
        void CreateCustomerAsync(Customer customer);
        void DeleteCustomerAsync(Customer customer);
        Task<List<Customer>> CustomerWithPartySizeGreaterThanAsync(int partySize);
        Task<bool> SaveChangesAsync();
    }
}