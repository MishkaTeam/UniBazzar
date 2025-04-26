using Domain.Aggregates.Units;
using Domain.BuildingBlocks.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.CheckoutCounter
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
