using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IEmployeeRepository
    {
        Task CreateEmployeeAsync(int restaurantId, Employee employee);
        void DeleteEmployeeAsync(Employee employee);
        Task<Employee?> GetEmployeeAsync(int employeeId, bool includeOrders);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<EmployeeWithRestaurantDetails>> GetEmployeesWithRestaurantDetailsFromViewAsync();
        Task<List<Employee>> ListManagersAsync();
        Task UpdateEmployeeAsync(int employeeId, int restaurantId, string firstName, string lastName, string position);

        Task<bool> RestaurantExistsAsync(int restaurantId);
        Task<bool> SaveChangesAsync();

    }
}