using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BuildingBlocks.Data;

namespace Domain.Aggregates.CheckoutCounter;

public interface ICheckoutCounterRepository : IRepositoryBase<CheckoutCounter>
{

}
