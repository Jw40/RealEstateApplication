using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication
{
    class PropertyCollections
    {
        //change this to modify the amount of properties on the application
        private const int MaxPropertyLimit = 50;
        private int currentPropNum;
        private List<Property> PropertyList;

        public PropertyCollections()
        {
            PropertyList = new List<Property>();
            currentPropNum = 0;
        }
        
        //checks if property object already exists
        public bool CheckIfPropertyExists(string PropertyAddress)
        {
            for (int i = 0; i < currentPropNum; i++)
            {
                if (PropertyList[i] != null)
                {
                    if (PropertyAddress.Equals(PropertyList[i].Address))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //add new property object, either land or house, restrictions in place so a different customer cannot
        //own the same property - stops duplicate properties.
        // if myproperty is null then either the address/postcode/land/decription is invalid or the property already exists.
        public void AddNewProperty(Property Myproperty, string OwnerName)
        {
            if (Myproperty != null)
            {
                if (CheckIfPropertyExists(Myproperty.Address) == true)
                {
                    Console.WriteLine("Property Address is already listed!\n");
                    return;
                }
                else if (currentPropNum == MaxPropertyLimit)
                {
                    Console.WriteLine("Maximum Property Limit Reached - Max Limit is 50 Properties");
                }
                else
                {
                    Myproperty.ThisOwnerName(OwnerName);
                    PropertyList.Add(Myproperty);
                    currentPropNum++;
                }
            }
            else
            {
                Console.WriteLine("Property Credentials Invalid - Try again.\n");
                return;
            }
        }

        //List property method that calls in myaddress which is userproperties[] from customer class
        //returns property list to testuserinterface
        //debug  - this prints the elements in myAddress from customer class UserProperties
        public List<Property> PrintProperties(string[] myAddress)
        {
            //debug
            //foreach (string address in myAddress){ Console.WriteLine(address); }
            List<Property> MyProperties = new List<Property>();      
            foreach (string address in myAddress)
            {
                for (int i = 0; i < currentPropNum; i++)
                {
                    if ((PropertyList[i] != null) && address.Equals(PropertyList[i].PropertyAddress()))
                    {
                        MyProperties.Add(PropertyList[i]);
                    }
                }
            }
            return MyProperties;
        }

        //Prints properties within postcode else no properties found in postcode
        public void PrintPropertiesInPostcode(string MyPostCode)
        {
            List<Property> MyProperties = new List<Property>();
            for (int i = 0; i < currentPropNum; i++)
            {
                if ((PropertyList[i] != null) && MyPostCode.Equals(PropertyList[i].PropertyPostCode()))
                {
                    MyProperties.Add(PropertyList[i]);
                }
            }
            UserInterface.DisplayList("Properties found:", MyProperties);
        }

        //Returns void, creates new list to store locally (MyProperties). AddressArray is the addresses the currently logged in customer owns.
        //if AddressArray does not Contain any address in the PropertyList (stops owner bidding on their property), If MyPostCode equals any postcode in PropertyList.
        //Add the object at PropertyList[i] to the local storage List Myproperties. 
        public void NewBidOnProperty(string MyPostCode, string CustomerName, string CustomerEmail, Customer currentuser)
        {
            List<Property> MyProperties = new List<Property>();
            string[] AddressArray = currentuser.ListPropertiesOwned();
            for (int i = 0; i < currentPropNum; i++)
            {
                if (!AddressArray.Contains(PropertyList[i].PropertyAddress()))
                {
                    if ((PropertyList[i] != null) && MyPostCode.Equals(PropertyList[i].PropertyPostCode()))
                    {
                        MyProperties.Add(PropertyList[i]);
                    }
                }
            }
            NewBidOnThisProperty(CustomerName, CustomerEmail, MyProperties);
        }

        //If MyProperties count is not 0, BidOnThisProperty calls userinterface choosefromlist(MyProperties) method
        //returns the option, local int Bid = BidOnThisProperty calls getbid method from Property class (name and email parms).
        //display bid and return - else no properties found etc..
        private static void NewBidOnThisProperty(string CustomerName, string CustomerEmail, List<Property> MyProperties)
        {
            if (MyProperties.Count != 0)
            {
                Property BidOnThisProperty = UserInterface.ChooseFromList(MyProperties);
                int Bid = BidOnThisProperty.GetBid(CustomerName, CustomerEmail);
                Console.WriteLine($"{CustomerName} ({CustomerEmail})" +
                    $" bid ${Bid}.\n");
                return;
            }
            UserInterface.Message("No Properties found to bid on in this postcode.");
        }

        //Takes in myAddress Array, MyProperties is local storage to use displaylist from UserInterface class
        //For each address in myAddress array, iterate through PropertyList, if any address in Property list is equal 
        //to the address, Add to local storage list. BidonThisProperty returns the option from local storage.
        //If the option is equal to any property in the PropertyList, call PrintBid method in Property, Land or House depending on the object. 
        public void ListBidsRecieved(string[] myAddress)
        {
            List<Property> MyProperties = new List<Property>();
            foreach (string address in myAddress)
            {
                for (int i = 0; i < currentPropNum; i++)
                {
                    if ((PropertyList[i] != null) && address.Equals(PropertyList[i].PropertyAddress()))
                    {
                        MyProperties.Add(PropertyList[i]);
                    }
                }
            }
            Property BidOnThisProperty = UserInterface.ChooseFromList(MyProperties);
            for (int i = 0; i < currentPropNum; i++)
            {
                if (BidOnThisProperty.PropertyAddress() == PropertyList[i].PropertyAddress())
                {
                    PropertyList[i].PrintBid();
                }
            }
        }

        //Takes in myAddress Array (UserProperties). MyProperties is local storage. For each Address in myAddress
        //iterate through PropertyList, If the address equals the address in PropertyList then Add to local storage.
        //returns the local storage and the string propertyAddress.
        public string SellThisProperty(string[] myAddress)//, string currentloggedinuser
        {
            string propertyAddress = "";
            List<Property> MyProperties = new List<Property>();
            foreach (string address in myAddress)
            {
                for (int i = 0; i < currentPropNum; i++)
                {
                    if ((PropertyList[i] != null) && address.Equals(PropertyList[i].PropertyAddress()))
                    {
                        MyProperties.Add(PropertyList[i]);
                    }
                }
            }
            return SellThisProperty(ref propertyAddress, MyProperties);
        }

        //Broken the method up for legibility. Takes in propertyAddress and local storage MyProperties
        //BiddedProperty is the option returned from chooseFromList MyProperties. Iterate through PropertyList.
        //If PropertyList[i] Bid count is greater than 0, If option from list (BiddedProperty) Address is equal to PropertyList Address
        //Call SellProperty method from Property class, propertyAddress is now the address of the property that is being sold.
        //RemoveAt removes the property from the application else no bids found. return the propertyAddress.
        private string SellThisProperty(ref string propertyAddress, List<Property> MyProperties)
        {
            Property BiddedProperty = UserInterface.ChooseFromList(MyProperties);
            for (int i = 0; i < currentPropNum; i++)
            {
                if (PropertyList[i].ReturnBidCount() > 0)
                {
                    if (BiddedProperty.PropertyAddress() == PropertyList[i].PropertyAddress())
                    {
                        PropertyList[i].SellProperty();
                        propertyAddress = PropertyList[i].PropertyAddress();
                        //removes object from property class list
                        PropertyList.RemoveAt(i);
                        currentPropNum--;
                        return propertyAddress;
                    }
                }
            }
            UserInterface.Message("No bids found for this property");
            return propertyAddress;
        }
    }
}
