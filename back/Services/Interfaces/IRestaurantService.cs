namespace back.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<Restaurant>> GetRestaurants();
    }
}