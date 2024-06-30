using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task<List<Table>> GetTablesAsync();
        Task<Table?> GetTableAsync(int tableId, bool includeReservations);
        Task CreateTableAsync(int restaurantId, Table table);
        Task UpdateTableAsync(int tableId, int restaurantId, int capacity);
        void DeleteTableAsync(Table table);
        Task<bool> RestaurantExistsAsync(int restaurantId);
        Task<bool> SaveChangesAsync();
    }
}