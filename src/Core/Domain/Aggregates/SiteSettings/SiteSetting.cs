using BuildingBlocks.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.DataType;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.SiteSettings
{
    public class SiteSetting : Entity
    {
        public SiteSetting() { }

        public string Desciption { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string LogoURL { get; private set; }
        public Guid? PriceListID { get; private set; }
        public string Address { get; private set; }

        private SiteSetting(string name, string discription, string phonenumber, string logourl
            , Guid? priceListID, string address)
        {
            Desciption = discription;
            Name = name;
            PhoneNumber = phonenumber;
            Address = address;
            LogoURL = logourl;
            PriceListID = priceListID;
        }

        public static SiteSetting Create(string description, string name, string phonenumber, string logourl
            , Guid? priceListID, string address)
        {
            if (!phonenumber.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);

            var sitesetting = new SiteSetting()
            {
                Desciption = description,
                Name = name,
                LogoURL = logourl,
                PriceListID = ValidatePriceListID(priceListID),
                Address = address
            };
            return sitesetting;
        }

        private static Guid? ValidatePriceListID(Guid? PriceListID)
        {
            if (PriceListID == Guid.Empty)
            {
                PriceListID = null;
            }

            return PriceListID;
        }

        public void Update(string description, string name, string phonenumber, string logourl
            , Guid? priceListID, string address)
        {
            if (!phonenumber.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);
            Desciption = description;
            Name = name;
            PhoneNumber = phonenumber;
            LogoURL = logourl;
            PriceListID = ValidatePriceListID(priceListID);
            Address = address;
            SetUpdateDateTime();

        }

    }
}
