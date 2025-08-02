using BuildingBlocks.Domain.Aggregates;
using Framework.DataType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.PosSettings
{
    public class PosSetting : Entity
    {
        public PosSetting() { }


        public string Name { get; private set; }
        public string Description { get; private set; }
        public string PhoneNumber { get; private set; }
        public string LogoUrl { get; private set; }
        public Guid? PricelistID { get; set; }
        public Guid? PublicCustomer { get; set; }
        public string Address { get; set; }

        private PosSetting(string name, string description, string phonenumber, string logourl, Guid? pricelistID,
            Guid? publiccustomer, string address)
        {
            Name = name;
            Description = description;
            PhoneNumber = phonenumber;
            LogoUrl = logourl;
            PricelistID = pricelistID;
            PublicCustomer = publiccustomer;
            Address = address;

        }

        public static PosSetting Create(string name, string description, string phonenumber, string logourl, Guid? pricelistID,
            Guid? publiccustomer, string address)
        {
            if (!phonenumber.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);
            var possetting = new PosSetting()
            {
                Description = description,
                Name = name,
                PhoneNumber = phonenumber,
                LogoUrl = logourl,
                PublicCustomer = ValidatePriceListID(publiccustomer),
                PricelistID = ValidatePriceListID(pricelistID),
                Address = address
            };
            return possetting;
        }
        private static Guid? ValidatePriceListID(Guid? priceListID)
        {
            if (priceListID == Guid.Empty)
            {
                priceListID = null;
            }
            return priceListID;
        }
        private static Guid? ValidatePublicCustomerID(Guid? puliccustomerID)
        {
            if (puliccustomerID == Guid.Empty)
            {
                puliccustomerID = null;
            }
            return puliccustomerID;
        }

        public void Update (string name, string description, string phonenumber, string logourl, Guid? pricelistID,
            Guid? publiccustomer, string address)
            {
            if (!phonenumber.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);
            Name = name;
            Description = description;
            PhoneNumber = phonenumber;
            LogoUrl = logourl;
            PricelistID=ValidatePriceListID(pricelistID);
            PublicCustomer=ValidatePublicCustomerID(publiccustomer);
            Address=address;
            SetUpdateDateTime();
            }
    }
}
