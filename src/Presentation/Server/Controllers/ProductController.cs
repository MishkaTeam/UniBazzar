using Application.ProductSearch;
using Domain.ProductSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ProductController(ProductSearchApplication productApplication) : ControllerBase
    {
        [HttpGet("search")]
        public async Task<ActionResult<SuggestionItem>> Search(string query)
        {
            var res = await productApplication.SuggestAsync(query);
            return Ok(res);
        }
    }
}
