using back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        this.restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Restaurant>>> Get()
    {
        return Ok(await restaurantService.GetRestaurants());
    }
}
