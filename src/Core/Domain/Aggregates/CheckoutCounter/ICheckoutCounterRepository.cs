using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.CheckoutCounter
{
    public interface ICheckoutCounterRepository
    {
        void AddCheckoutCounter(CheckoutCounter entity);
        Task<List<CheckoutCounter>> GetAllCheckoutCountersAsync();
        Task<List<CheckoutCounter>> GetRootCheckoutCountersAsync();
        Task<CheckoutCounter> GetCheckoutCounterAsync(Guid Id);
        void Remove(CheckoutCounter entity);
    }
}
