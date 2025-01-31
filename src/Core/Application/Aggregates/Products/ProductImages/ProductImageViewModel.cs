﻿using Application.Aggregates.Products.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Application.Aggregates.Products.ProductImages;

public class ProductImageViewModel : CreateProductImageViewModel
{
    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Id))]
    public Guid Id { get; set; }

    [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Product))]
    public ProductViewModel? Product { get; set; }
}
