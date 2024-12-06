using BuildingBlocks.Domain.SeedWork;
using Domain.Aggregates.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Stores
{
    public class Store :  IEntityHasUpdateInfo
    {
        public Store()
        {
            //For EF 
        }
        private Store(string name, string culture, string logo, bool isActive)
        {
            Name = name;
            Culture = culture;
            Logo = logo;
            Id=Guid.NewGuid();
            IsActivce = isActive;
        }
        public Guid Id { get; private set; } 
        public string Name { get;private set; }
        public string Culture { get;private set; }
       
        public string Logo { get;private set; }

        public bool IsActivce { get; private set; }
        public int Ordering { get; private set; }
        public long UpdateDateTime { get; private set; }
        public Guid UpdatedBy { get; private set; }

      

    
        public static Store Create(string name, string culture, string logo,bool isActive)
        {
            var store = new Store(name, culture, logo,isActive)
            {
                
            };
           

            return store;
        }
        public void Update (string name, string culture, string logo,bool isActive)
        {

            Name = name;
            Culture = culture;
            Logo = logo;
            IsActivce=isActive;

        }

      

        public void SetUpdateBy(Guid Id)
        {
            UpdatedBy = Id; 

        }

        public void SetUpdateDateTime()
        {
            UpdateDateTime = DateTimeUtility.GetCurrentUnixUTCTimeSeconds();
        }

    }
}
