using BuildingBlocks.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Aggregates.Customers
{
    public class ShippingAddress: Entity
    {
        public string Country { get; set; }
        public string province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

    }
}
//کشور - استان - شهر - آدرس - پلاک و کد پستی