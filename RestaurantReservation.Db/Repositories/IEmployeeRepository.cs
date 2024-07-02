﻿using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeAsync(int employeeId, bool includeOrders);
        Task CreateEmployeeAsync(int restaurantId, Employee employee);
        void DeleteEmployeeAsync(Employee employee);
        Task<List<Employee>> ListManagersAsync();
        Task<decimal> CalculateAverageOrderAmountAsync(int employeeId);
        Task<List<EmployeeWithRestaurantDetails>> GetEmployeesWithRestaurantDetailsFromViewAsync();

        Task<Employee?> ValidateUserCredentials(string? firstName, string? lastName, int id);
        Task<bool> RestaurantExistsAsync(int restaurantId);
        Task<bool> SaveChangesAsync();

    }
}