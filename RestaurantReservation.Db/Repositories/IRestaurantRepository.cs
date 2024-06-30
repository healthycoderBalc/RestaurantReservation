using RestaurantReservation.Db.Models;

namespace RestaurantReservation.Db.Repositories
{
    public interface IRestaurantRepository
    {
        void CreateRestaurantAsync(Restaurant restaurant);
        void DeleteRestaurantAsync(Restaurant restaurant);
        Task<Restaurant?> GetRestaurantAsync(int restaurantId, bool includeLists);
        Task<List<Restaurant>> GetRestaurantsAsync();
        Task<decimal> GetRestaurantTotalRevenueAsync(int restaurantId);
        Task UpdateRestaurantAsync(int restaurantId, string newAddress, string newPhoneNumber, string newOpeningHours);
        Task<bool> SaveChangesAsync();
    }
}