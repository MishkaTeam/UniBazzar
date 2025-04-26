using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.CheckoutCounter;
using Microsoft.EntityFrameworkCore;
using Persistence.Aggregates.CheckoutCounter;
namespace Persistence.Aggregates.CheckoutCounter
{
    public class CheckoutCounterRepository(UniBazzarContext uniBazzarContext) : ICheckoutCounterRepository
    {
        public void AddCheckoutCounter(Domain.Aggregates.CheckoutCounter.CheckoutCounter entity)
        {
            uniBazzarContext.Add(entity);
        }

        public Task<List<Domain.Aggregates.CheckoutCounter.CheckoutCounter>> GetAllCheckoutCountersAsync()
        {
            return uniBazzarContext.CheckoutCounter
                                   .Include(x => x.Domain.Aggregates.CheckoutCounter.BaseCheckoutCounter).ToListAsync();
        }

        public async Task<Domain.Aggregates.CheckoutCounter.CheckoutCounter> GetRootCheckoutCountersAsync()
        {
            return await uniBazzarContext.CheckoutCounter.Where(x => x.BaseCheckoutCounterID == null).FirstOrDefaultAsync();
        }

        public async Task<Domain.Aggregates.CheckoutCounter.CheckoutCounter> GetCheckoutCountersAsync(Guid? id)
        {
            var checkoutCounter = await _uniBazzarContext.CheckoutCounter
                                       .FirstOrDefaultAsync(x => x.Id == id);
            return checkoutCounter ?? new Domain.Aggregates.CheckoutCounter.CheckoutCounter();
        }

        public void Remove(Domain.Aggregates.CheckoutCounter.CheckoutCounter entity)
        {
            _uniBazzarContext.CheckoutCounter.Remove(entity);
        }
    }

}
