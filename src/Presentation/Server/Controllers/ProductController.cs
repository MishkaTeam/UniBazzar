using Application.ProductSearch;
using Domain.ProductSearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Aggregates.PriceLists;
using Application.Aggregates.PriceLists.ViewModels.PriceList;

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

        //[HttpGet("test-pricelists")]
        //public async Task<IActionResult> TestPriceLists()
        //{
        //    try
        //    {
        //        var priceLists = await .GetAllPriceListAsync();
        //        return Ok(new
        //        {
        //            Count = priceLists.Count,
        //            PriceLists = priceLists.Select(p => new { p.Id, p.Title }).ToList(),
        //            Message = "PriceLists loaded successfully"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            Error = ex.Message,
        //            StackTrace = ex.StackTrace,
        //            Message = "Error loading PriceLists"
        //        });
        //    }
        //}
    }
}
