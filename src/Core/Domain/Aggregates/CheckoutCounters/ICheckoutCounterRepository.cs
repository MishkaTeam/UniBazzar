using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Domain.Data;

namespace Domain.Aggregates.CheckoutCounters;

public interface ICheckoutCounterRepository : IRepositoryBase<CheckoutCounter>
{

}
