# Site Settings Module

## Overview
The Site Settings module provides CRUD operations for managing site configuration information including description, name, phone number, address, logo URL, and price list ID.

## Features
- **Create**: Add new site settings
- **Read**: Retrieve site settings by ID or store ID
- **Update**: Modify existing site settings
- **Delete**: Remove site settings
- **Validation**: Phone number validation using mobile format

## API Endpoints

### Create Site Setting
```
POST /api/v1/SiteSettings
Content-Type: application/json

{
  "description": "Site description",
  "name": "Site name",
  "phoneNumber": "09123456789",
  "address": "Site address",
  "logoURL": "https://example.com/logo.png",
  "priceListID": "optional-guid-here"
}
```

### Get All Site Settings
```
GET /api/v1/SiteSettings
```

### Get Site Setting by ID
```
GET /api/v1/SiteSettings/{id}
```

### Get Site Setting by Store ID
```
GET /api/v1/SiteSettings/store/{storeId}
```

### Update Site Setting
```
PUT /api/v1/SiteSettings/{id}
Content-Type: application/json

{
  "id": "site-setting-guid",
  "description": "Updated description",
  "name": "Updated name",
  "phoneNumber": "09876543210",
  "address": "Updated address",
  "logoURL": "https://example.com/new-logo.png",
  "priceListID": "new-price-list-guid"
}
```

### Delete Site Setting
```
DELETE /api/v1/SiteSettings/{id}
```

## Domain Model

### SiteSetting Entity
- Inherits from `Entity` base class
- Includes audit fields (InsertDateTime, UpdateDateTime, etc.)
- Implements business validation rules
- Uses factory method pattern for creation

### Properties
- `Description`: Site description
- `Name`: Site name
- `PhoneNumber`: Contact phone number (validated)
- `Address`: Physical address
- `LogoURL`: Logo image URL
- `PriceListID`: Associated price list ID (optional)

## Business Rules
1. Phone number must be in valid mobile format
2. PriceListID is optional and can be null
3. All string fields are required
4. Entity versioning and audit trail are automatically managed

## Usage Examples

### In Application Service
```csharp
var siteSetting = await _siteSettingsApplication.CreateSiteSettingAsync(new CreateSiteSettingViewModel
{
    Description = "My E-commerce Site",
    Name = "MyStore",
    PhoneNumber = "09123456789",
    Address = "123 Main St, City",
    LogoURL = "https://mystore.com/logo.png",
    PriceListID = priceListId
});
```

### In Controller
```csharp
[HttpGet]
public async Task<ActionResult<List<SiteSettingViewModel>>> GetAllSiteSettings()
{
    var result = await _siteSettingsApplication.GetAllSiteSettingsAsync();
    return Ok(result);
}
```

## Dependencies
- `BuildingBlocks.Domain` - Base entity and repository interfaces
- `Framework.DataType` - Result contracts and validation
- `Resources` - Localization and error messages
- `Mapster` - Object mapping

## Testing
Unit tests are available in `Domain.UnitTests/Aggregates/SiteSettings/` covering:
- Entity creation
- Entity updates
- Business rule validation
- Edge cases
