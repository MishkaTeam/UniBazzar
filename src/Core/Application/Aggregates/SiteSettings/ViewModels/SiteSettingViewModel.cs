using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.SiteSettings.ViewModels
{
    public class SiteSettingViewModel : CreateSiteSettingViewModel
    {
        [Display
        (ResourceType = typeof(Resources.DataDictionary),
        Name = nameof(Resources.DataDictionary.Id))]
        public Guid ID { get; set; }
    }
}
