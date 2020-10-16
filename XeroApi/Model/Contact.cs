﻿using System;

namespace XeroApi.Model
{
    public class Contact : EndpointModelBase
    {
        private readonly SalesTrackingCategories _tracking = new SalesTrackingCategories();
        [ItemId]
        public Guid ContactID { get; set; }
        
        [ItemNumber]
        public string ContactNumber { get; set; }

        public string AccountNumber { get; set; }

        [ItemUpdatedDate]
        public DateTime? UpdatedDateUTC { get; set; }

        public string ContactStatus { get; set; }
        
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }
        
        public string SkypeUserName { get; set; }
        
        public string BankAccountDetails { get; set; }
        
        public string TaxNumber { get; set; }
        
        public string AccountsReceivableTaxType { get; set; }
        
        public string AccountsPayableTaxType { get; set; }
        
        public Addresses Addresses { get; set; }
        
        public Phones Phones { get; set; }
        
        public ContactGroups ContactGroups { get; set; }
        public string TrackingCategoryName { get; set; }
        public string TrackingCategoryOption { get; set; }
        public SalesTrackingCategories SalesTrackingCategories { get { return _tracking; } }
        
        [ReadOnly]
        public bool IsSupplier { get; set; }

        [ReadOnly]
        public bool IsCustomer { get; set; }
        
        public string DefaultCurrency { get;  set; }

        public override string ToString()
        {
            return string.Format("Contact:{0}", Name);
        }
    }

    public class Contacts : ModelList<Contact>
    {
    }
    
}