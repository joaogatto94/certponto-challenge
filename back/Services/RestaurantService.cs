using back.Repositories.Interfaces;
using back.Services.Interfaces;

namespace back.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;


        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;

        }
        public async Task<List<Restaurant>> GetRestaurants()
        {
            return await restaurantRepository.GetRestaurants();
        }

    }
}