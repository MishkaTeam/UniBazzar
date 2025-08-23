using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Aggregates.SiteSettings.ViewModels
{
    public class CreateSiteSettingViewModel
    {
        public CreateSiteSettingViewModel()
        {

        }
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Name))]
        public string Name { get; set; }
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.FullDescription))]
        public string Description { get; set; }
        [RegularExpression
        (Constants.RegularExpression.CellPhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Logo))]
        public string LogoURL { get; set; }
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.PriceList))]
        public Guid? PriceListID { get; set; }
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Address))]
        public string Address { get; set; }
    }
}
