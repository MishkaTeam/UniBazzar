using Domain.Aggregates.Units;
using BuildingBlocks.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.CheckoutCounters
{
    public class CheckoutCounter : Entity
    {
        public CheckoutCounter() { }
        public string Name { get; private set; }

        private CheckoutCounter(string name)
        {
            Name = name;
        }
        public static CheckoutCounter Create(string Name)
        {
            var checkoutCounter = new CheckoutCounter(Name);
  
            return checkoutCounter;
        }
        public void Update(string Name)
        {
            this.Name = Name;
            SetUpdateDateTime();
        }
    }
}
