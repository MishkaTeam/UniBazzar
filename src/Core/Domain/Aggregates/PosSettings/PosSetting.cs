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
        public Guid? PriceListId { get; set; }
        public Guid? PublicCustomer { get; set; }
        public string Address { get; set; }

        private PosSetting(string name, string description, string phoneNumber, string logoUrl, Guid? priceListId,
            Guid? publicCustomer, string address)
        {
            Name = name;
            Description = description;
            PhoneNumber = phoneNumber;
            LogoUrl = logoUrl;
            PriceListId = priceListId;
            PublicCustomer = publicCustomer;
            Address = address;

        }

        public static PosSetting Create(string name, string description, string phoneNumber, string logoUrl, Guid? priceListId,
            Guid? publicCustomer, string address)
        {
            if (!phoneNumber.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);
            var possetting = new PosSetting()
            {
                Description = description,
                Name = name,
                PhoneNumber = phoneNumber,
                LogoUrl = logoUrl,
                PublicCustomer = ValidatePriceListID(publicCustomer),
                PriceListId = ValidatePriceListID(priceListId),
                Address = address
            };
            return possetting;
        }
        private static Guid? ValidatePriceListID(Guid? priceListId)
        {
            if (priceListId == Guid.Empty)
            {
                priceListId = null;
            }
            return priceListId;
        }
        private static Guid? ValidatePublicCustomerID(Guid? pulicCustomerId)
        {
            if (pulicCustomerId == Guid.Empty)
            {
                pulicCustomerId = null;
            }
            return pulicCustomerId;
        }

        public void Update (string name, string description, string phoneNumber, string logoUrl, Guid? priceListId,
            Guid? publicCustomer, string address)
            {
            if (!phoneNumber.IsValidMobile())
                throw new ValidationException(Resources.Messages.Validations.CellPhoneNumber);
            Name = name;
            Description = description;
            PhoneNumber = phoneNumber;
            LogoUrl = logoUrl;
            PriceListId =ValidatePriceListID(priceListId);
            PublicCustomer=ValidatePublicCustomerID(publicCustomer);
            Address=address;
            SetUpdateDateTime();
            }
    }
}
