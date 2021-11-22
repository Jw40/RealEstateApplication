using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication
{
    abstract class Property
    {
        private string address;
        private string postcode;
        public List<int> Bids;
        public List<string> BidderDetails;
        public string Address
        {
            get
            {
                return address;
            }
            private set
            {
                address = value;
            }
        }

        public string Postcode
        {
            get
            {
                return postcode;
            }
            private set
            {
                postcode = value;
            }
        }

        public Property(string MyAddress, string MyPostcode)
        {
            this.Address = MyAddress;
            this.Postcode = MyPostcode;
            this.Bids = new List<int>();
            this.BidderDetails = new List<string>();
        }

        //returns Address
        public string PropertyAddress()
        {
            return Address;
        }

        //returns Postcode
        public string PropertyPostCode()
        {
            return Postcode;
        }

        //abstract methods requires all abstract methods to be in child class as override methods
        //goes to house or land depending on what the object is
        public abstract int GetBid(string Bidder, string BidderEmail);
        public abstract void PrintBid();
        public abstract double GetHighestBidder();
        public abstract string SellProperty();
        public abstract double GetSalesTax();
        public abstract string ThisOwnerName(string OwnerName);
        public abstract int ReturnBidCount();
    }
}
