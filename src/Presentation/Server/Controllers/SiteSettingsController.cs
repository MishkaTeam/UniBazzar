using Microsoft.AspNetCore.Mvc;
using Application.Aggregates.SiteSettings;
using Application.Aggregates.PriceLists;
using Application.ViewModels.SiteSettings;
using Application.Aggregates.PriceLists.ViewModels.PriceList;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Domain.Context;
using Domain.Aggregates.PriceLists;
using Domain.Aggregates.SiteSettings;

namespace UniBazzar.Server.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class SiteSettingsController : ControllerBase
{
    private readonly SiteSettingsApplication _siteSettingsApplication;
    private readonly PriceListsApplication _priceListsApplication;

    public SiteSettingsController(
        SiteSettingsApplication siteSettingsApplication,
        PriceListsApplication priceListsApplication)
    {
        _siteSettingsApplication = siteSettingsApplication;
        _priceListsApplication = priceListsApplication;
    }

    [HttpPost]
    public async Task<ActionResult<SiteSettingViewModel>> CreateSiteSetting([FromBody] CreateSiteSettingViewModel viewModel)
    {
        try
        {
            var result = await _siteSettingsApplication.CreateSiteSettingAsync(viewModel);
            return CreatedAtAction(nameof(GetSiteSettingById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<SiteSettingViewModel>>> GetAllSiteSettings()
    {
        try
        {
            var result = await _siteSettingsApplication.GetAllSiteSettingsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SiteSettingViewModel>> GetSiteSettingById(Guid id)
    {
        try
        {
            var result = await _siteSettingsApplication.GetSiteSettingByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("store/{storeId:guid}")]
    public async Task<ActionResult<SiteSettingViewModel>> GetSiteSettingByStoreId(Guid storeId)
    {
        try
        {
            var result = await _siteSettingsApplication.GetSiteSettingByStoreIdAsync(storeId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SiteSettingViewModel>> UpdateSiteSetting(Guid id, [FromBody] UpdateSiteSettingViewModel viewModel)
    {
        try
        {
            if (id != viewModel.Id)
                return BadRequest("ID mismatch");

            var result = await _siteSettingsApplication.UpdateSiteSettingAsync(viewModel);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteSiteSetting(Guid id)
    {
        try
        {
            await _siteSettingsApplication.DeleteSiteSettingAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        try
        {
            var priceLists = await _priceListsApplication.GetAllPriceListAsync();
            var siteSettings = await _siteSettingsApplication.GetAllSiteSettingsAsync();
            
            return Ok(new
            {
                PriceLists = new
                {
                    Count = priceLists.Count,
                    Items = priceLists.Select(p => new { p.Id, p.Title }).ToList()
                },
                SiteSettings = new
                {
                    Count = siteSettings.Count,
                    Items = siteSettings.Select(s => new { s.Id, s.Name, s.PriceListID }).ToList()
                },
                Message = "Test completed successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message,
                StackTrace = ex.StackTrace,
                Message = "Error in test"
            });
        }
    }

    [HttpGet("test-pricelists-raw")]
    public async Task<IActionResult> TestPriceListsRaw()
    {
        try
        {
            // Get raw data from database without any filters
            var context = HttpContext.RequestServices.GetRequiredService<UniBazzarContext>();
            var rawPriceLists = await context.Set<PriceList>()
                .AsNoTracking()
                .ToListAsync();
            
            return Ok(new
            {
                RawCount = rawPriceLists.Count,
                RawItems = rawPriceLists.Select(p => new { p.Id, p.Title, p.StoreId, p.OwnerId }).ToList(),
                Message = "Raw PriceLists loaded successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message,
                StackTrace = ex.StackTrace,
                Message = "Error loading raw PriceLists"
            });
        }
    }

    [HttpGet("check-context")]
    public async Task<IActionResult> CheckContext()
    {
        try
        {
            var executionContext = HttpContext.RequestServices.GetRequiredService<IExecutionContextAccessor>();
            var context = HttpContext.RequestServices.GetRequiredService<UniBazzarContext>();
            
            // Check if there are any PriceLists in database
            var totalPriceLists = await context.Set<PriceList>()
                .AsNoTracking()
                .CountAsync();
            
            // Check if there are any PriceLists with current StoreId
            var filteredPriceLists = await context.Set<PriceList>()
                .AsNoTracking()
                .Where(x => x.StoreId == executionContext.StoreId)
                .CountAsync();
            
            return Ok(new
            {
                CurrentStoreId = executionContext.StoreId.ToString(),
                CurrentUserId = executionContext.UserId?.ToString() ?? "null",
                CurrentRole = executionContext.Role,
                TotalPriceListsInDatabase = totalPriceLists,
                PriceListsWithCurrentStoreId = filteredPriceLists,
                IsDefaultStoreId = executionContext.StoreId == Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Message = "Context check completed"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message,
                StackTrace = ex.StackTrace,
                Message = "Error checking context"
            });
        }
    }

    [HttpGet("test-database")]
    public async Task<IActionResult> TestDatabase()
    {
        try
        {
            var context = HttpContext.RequestServices.GetRequiredService<UniBazzarContext>();
            
            // Test database connection
            var canConnect = await context.Database.CanConnectAsync();
            
            // Check if PriceLists table exists and has data
            var priceListsCount = await context.Set<PriceList>()
                .AsNoTracking()
                .CountAsync();
            
            return Ok(new
            {
                DatabaseConnection = canConnect ? "Success" : "Failed",
                PriceListsTableExists = true,
                CurrentPriceListsCount = priceListsCount,
                Message = "Database test completed successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message,
                StackTrace = ex.StackTrace,
                Message = "Database test failed"
            });
        }
    }



    [HttpPost("ensure-database")]
    public async Task<IActionResult> EnsureDatabase()
    {
        try
        {
            var context = HttpContext.RequestServices.GetRequiredService<UniBazzarContext>();
            
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();
            
            // Check if tables exist
            var priceListsTableExists = await context.Database.CanConnectAsync();
            var siteSettingsTableExists = context.Model.FindEntityType(typeof(SiteSetting)) != null;
            
            return Ok(new
            {
                DatabaseCreated = true,
                PriceListsTableExists = priceListsTableExists,
                SiteSettingsTableExists = siteSettingsTableExists,
                Message = "Database ensured successfully"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Error = ex.Message,
                StackTrace = ex.StackTrace,
                Message = "Error ensuring database"
            });
        }
    }
}

