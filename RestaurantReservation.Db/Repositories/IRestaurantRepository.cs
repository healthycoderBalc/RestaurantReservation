using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetRestaurantsAsync();
        Task<Restaurant?> GetRestaurantAsync(int restaurantId, bool includeLists);
        void CreateRestaurantAsync(Restaurant restaurant);
        void DeleteRestaurantAsync(Restaurant restaurant);

        Task<decimal> GetRestaurantTotalRevenueAsync(int restaurantId);
        
        Task<bool> SaveChangesAsync();
    }
}