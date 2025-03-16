using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.Categories.ViewModels;

public class MenuCategoryViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MenuCategoryViewModel> ChildCategories{ get; set; }
}