using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ICustomerRepository
    {
        void CreateCustomerAsync(Customer customer);
        Task<List<Customer>> CustomerWithPartySizeGreaterThanAsync(int partySize);
        void DeleteCustomerAsync(Customer customer);
        Task<Customer?> GetCustomerAsync(int customerId, bool includeReservations);
        Task<List<Customer>> GetCustomersAsync();
        Task UpdateCustomerAsync(int customerId, string newEmail, string newPhoneNumber);
        Task<bool> SaveChangesAsync();
    }
}