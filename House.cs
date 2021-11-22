using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication
{
    //house is child, property is parent or base class
    class House : Property
    {
        private string description;
        public string OwnerName { get; private set; }

        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }

        //constructor inherited the address and postcode from property class
        public House(string MyAddress, string MyPostCode, string MyDescription) : base (MyAddress, MyPostCode)
        {
            this.Description = MyDescription;
            this.OwnerName = OwnerName;
        }

        //returns the current owners name
        public override string ThisOwnerName(string ownername)
        {
            OwnerName = ownername;
            return this.OwnerName;
        }

        //returns the current bid count
        public override int ReturnBidCount()
        {
            int numOfBids = Bids.Count;
            return numOfBids;
        }

        //returns modified ToString
        public override string ToString()
        {
            return Address + ", " + Postcode + ": House and land " + Description;
        }

        //prompts for bid, then adds to the bid list and bidderdetails list
        //with the parms (bidder(name) and bidder email) returns the bid
        public override int GetBid(string Bidder, string BidderEmail)
        {
            int Bid = UserInterface.GetInteger("Enter Bid ($)");
            Bids.Add(Bid);
            BidderDetails.Add(Bidder + " " + "(" + BidderEmail + ")");
            return Bid;
        }

        //lists the bids recieved for the current property 
        public override void PrintBid()
        {
            int count = 0;
            UserInterface.Message("Bids received:");
            if (Bids.Count != 0)
            {
                for (int i = 0; i < Bids.Count; i++)
                {
                    Console.WriteLine($"   {count + 1}) {BidderDetails[i]} bid ${Bids[i]}");
                    count++;
                }
                Console.WriteLine();
            }
            else
            {
                UserInterface.Message("No bids found.");
            }

        }

        //returns the highest bid
        public override double GetHighestBidder()
        {
            var BidsArray = Bids;
            BidsArray.Sort();
            BidsArray.Reverse();
            return BidsArray[0];
        }

        //override method sell property if bids list count is > 0 turn bids list into array, find max with Max();, index that item to add it to the 
        //bidderdetails list, owner name is now the top bidders name, print message, caluculate tax
        public override string SellProperty()
        {
            if (Bids.Count != 0)
            {
                var BidsArray = Bids;
                int maxValue = BidsArray.Max();
                int maxIndex = BidsArray.ToList().IndexOf(maxValue);
                this.OwnerName = BidderDetails[maxIndex];
                UserInterface.Message($"{this.Address}, {this.Postcode}: House and land " +
                    $"{this.Description} sold to {OwnerName} for ${maxValue}");
                UserInterface.Message($"Tax payable ${GetSalesTax():00}");
                return this.Address;
            }
            else
            {
                UserInterface.Message("No bids found.");
            }
            return null;
        }

        //return highestbiggder * sales tax
        public override double GetSalesTax()
        {
            return this.GetHighestBidder() * 0.1;
        }
    }
}
