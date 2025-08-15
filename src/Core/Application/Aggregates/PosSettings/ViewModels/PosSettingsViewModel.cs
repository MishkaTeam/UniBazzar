using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.PosSettings.ViewModels
{
    public class PosSettingsViewModel
    {
        [System.ComponentModel.DataAnnotations.Display
    (ResourceType = typeof(Resources.DataDictionary),
    Name = nameof(Resources.DataDictionary.Id))]
        public Guid Id { get; set; }

    
    }
}
