using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.PosSettings.ViewModels
{
    public class CreatePosSettingsViewModel
    {
        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Title))]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.FullDescription))]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.CellPhonenumber))]
        public string PhoneNumber { get; set; }

        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Logo))]
        public string LogoUrl { get; set; }

        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.PriceList))]
        public Guid PriceListId { get; set; }
        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.PublicCustomer))]

        public Guid PublicCustomer { get;set; }

        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Address))]
        public string  Address { get; set; }



    }
}
