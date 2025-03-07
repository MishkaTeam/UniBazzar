using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.Products.ProductPriceLists.ViewModels;

public class ProductPriceListViewModel : CreateProductPriceListViewModel
{

    [Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }

}
