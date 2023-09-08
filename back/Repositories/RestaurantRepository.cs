using back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace back.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly LunchContext context;
        
        public RestaurantRepository(LunchContext context)
        {
            this.context = context;
        }
        public Task<List<Restaurant>> GetRestaurants()
        {
            return context.Restaurants.ToListAsync();
        }
    }
}