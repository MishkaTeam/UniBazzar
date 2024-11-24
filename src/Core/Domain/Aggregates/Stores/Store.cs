using Domain.Aggregates.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Stores
{
    public class Store
    {
        public Store()
        {
            //For EF 
        }
        private Store(string name, string culture, string logo, string address, string telephone)
        {
            Name = name;
            Culture = culture;
            Logo = logo;
            Address = address;
            Telephone = telephone;

        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get;private set; }
        public string Culture { get;private set; }
        public string Address { get;private set; }
        public string Logo { get;private set; }
        public string Telephone { get;private set; }


        private static string ValidateTelephone(string tel)
        {
            if (tel.Length!=14)
            {
                var message = string.Format(Resources.Messages.Validations.CellPhoneNumber, Resources.DataDictionary.CellPhonenumber);

                throw new ArgumentException(message: message);
            }

            return tel;
        }
        public static Store Create(string name, string culture, string logo,string address,string telephone)
        {
            var store = new Store(name, culture, logo,address,telephone)
            {
                Telephone = ValidateTelephone(telephone)
            };
           

            return store;
        }
        public void Update (string name, string culture, string logo, string address, string telephone)
        {

            Name = name;
            Culture = culture;
            Logo = logo;
            Address = address;
            Telephone = telephone;

        }


    }
}
