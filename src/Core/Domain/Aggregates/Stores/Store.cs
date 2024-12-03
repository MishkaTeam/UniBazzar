using BuildingBlocks.Domain.SeedWork;
using Domain.Aggregates.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Stores
{
    public class Store : IsEntityHasVersionControl, IEntityHasUpdateInfo, IEntityHasTenant, IEntityHasOwner
    {
        public Store()
        {
            //For EF 
        }
        private Store(string name, string culture, string logo, string address, string telephone,bool isActive)
        {
            Name = name;
            Culture = culture;
            Logo = logo;
            Address = address;
            Telephone = telephone;
            Id=Guid.NewGuid();
            IsActice = isActive;
        }
        public Guid Id { get; private set; } 
        public string Name { get;private set; }
        public string Culture { get;private set; }
        public string Address { get;private set; }
        public string Logo { get;private set; }
        public string Telephone { get;private set; }
        public bool IsActice { get; private set; }
        public int Ordering { get; }
        public long InsertDateTime { get; }
        public Guid InsertedByDate { get; }
        void SetInserDateTime() { }
        void SetInserBy() { }


        public int Version => throw new NotImplementedException();

        public long UpdateDateTime => throw new NotImplementedException();

        public Guid UpdatedBy => throw new NotImplementedException();

        public Guid TenantId => throw new NotImplementedException();

        public Guid OwnerId => throw new NotImplementedException();

        private static string ValidateTelephone(string tel)
        {
            if (tel.Length!=14)
            {
                var message = string.Format(Resources.Messages.Validations.CellPhoneNumber, Resources.DataDictionary.CellPhonenumber);

                throw new ArgumentException(message: message);
            }

            return tel;
        }
        public static Store Create(string name, string culture, string logo,string address,string telephone,bool isActive)
        {
            var store = new Store(name, culture, logo,address,telephone,isActive)
            {
                Telephone = ValidateTelephone(telephone)
            };
           

            return store;
        }
        public void Update (string name, string culture, string logo, string address, string telephone,bool isActive)
        {

            Name = name;
            Culture = culture;
            Logo = logo;
            Address = address;
            Telephone = telephone;
            IsActice=isActive;

        }

        public void SetUpdateDateTime()
        {
            throw new NotImplementedException();
        }

        public void SetUpdateBy(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void SetTenant(Guid tenantId)
        {
            throw new NotImplementedException();
        }

        public Guid GetTenant()
        {
            throw new NotImplementedException();
        }

        public void SetOwner(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Guid GetOwner()
        {
            throw new NotImplementedException();
        }
    }
}
