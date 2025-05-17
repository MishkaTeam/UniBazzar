using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates.CheckoutCounter.ViewModels
{
    public class CreateCheckoutCounterViewModels
    {
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Name))]
        public string Name { get; set; }

    }
}
