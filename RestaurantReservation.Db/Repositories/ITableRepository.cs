using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface ITableRepository
    {
        Task CreateTableAsync(int restaurantId, Table table);
        void DeleteTableAsync(Table table);
        Task<Table?> GetTableAsync(int tableId, bool includeReservations);
        Task<List<Table>> GetTablesAsync();
        Task UpdateTableAsync(int tableId, int restaurantId, int capacity);
        Task<bool> RestaurantExistsAsync(int restaurantId);
        Task<bool> SaveChangesAsync();
    }
}