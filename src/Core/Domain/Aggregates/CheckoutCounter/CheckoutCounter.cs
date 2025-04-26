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
        public Guid? BaseCheckoutCounterID { get; private set; }
        public CheckoutCounter? BaseCheckoutCounter { get; private set; }
        private static Guid? ValidateBaseCheckoutCounter(Guid? baseCheckoutCounter)
        {
            if (baseCheckoutCounter == Guid.Empty)
            {
                baseCheckoutCounter = null;
            }
            return baseCheckoutCounter;
        }
        private CheckoutCounter(string name, Guid? baseCheckoutCounterID)
        {
            Name = name;
            BaseCheckoutCounterID = baseCheckoutCounterID;
        }
        public static CheckoutCounter Create(string Name, Guid? baseCheckoutCounterID)
        {
            var checkoutCounter = new CheckoutCounter(Name, baseCheckoutCounterID)
            {
                BaseCheckoutCounterID = ValidateBaseCheckoutCounter(baseCheckoutCounterID)
            };
            return checkoutCounter;
        }
        public void Update(string Name, Guid? baseCheckoutCounterID)
        {
            this.Name = Name;
            BaseCheckoutCounterID = ValidateBaseCheckoutCounter(baseCheckoutCounterID);
            SetUpdateDateTime();
        }
    }
}
