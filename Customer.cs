using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication
{
    class Customer
    {
        private string name;
        private string email;
        private string password;

        private const int maxPropertiesOwned = 5;
        private int propertyNum;
        private string[] UserProperties;
       
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            private set
            {
                email = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            private set
            {
                password = value;
            }
        }

        public Customer(string MyName, string MyEmail, string MyPassword)
        {
            this.Name = MyName;
            this.Email = MyEmail;
            this.Password = MyPassword;
            ClearUserProperties();
        }

        public string CustomerName()
        {
            return Name;
        }

        public string CustomerEmail()
        {
            return Email;
        }

        public string CustomerPassword()
        {
            return Password;
        }

        //clears user propertys array for new customer object
        public void ClearUserProperties()
        {
            UserProperties = new string[maxPropertiesOwned];
            for (int i = 0; i < maxPropertiesOwned; i++)
            {
                UserProperties[i] = "";
                propertyNum = 0;
            }
        }

        //checks if property exists in the current userproperties[i] array
        public bool CheckIfCustomerAddressExists(string PropertyAddress)
        {
            for (int i = 0; i < maxPropertiesOwned; i++)
            {
                if (UserProperties[i] != null) 
                {
                    if (PropertyAddress.Equals(UserProperties[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //add new item to userproperties
        public void NewCustomerProperty(string MyAddress)
        {
            if (propertyNum == maxPropertiesOwned)
            {
                Console.WriteLine("You have reached the Properties Limit - Max per Customer = 5");
                return;
            }
            else
            {
                for (int i = 0; i < UserProperties.Length; i++)
                {
                    
                    if (CheckIfCustomerAddressExists(MyAddress) == false)
                    {
                        if (UserProperties[i] == "") 
                        {
                            UserProperties[i] = MyAddress;
                            propertyNum++;
                            //debug
                            //Console.WriteLine("Adding new property in customer class array - UserProperties"); //debug 
                        }
                    }
                }    
            }
        }

        //checks if the customer owns the parms string array (address of property)
        public bool CheckIfCustomerOwnsProperty(string[] MyAddress)
        {
            for (int i = 0; i < maxPropertiesOwned; i++)
            {
                if (MyAddress[i] != null)
                {
                    if (MyAddress[i].Equals(UserProperties[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //lists all propertys the current user has (within this class)
        public string[] ListPropertiesOwned()
        {
            string[] AddressOwned = new string[maxPropertiesOwned];
            for (int i = 0; i < maxPropertiesOwned; i++)
            {
                if(UserProperties[i] != "")
                {
                    return (UserProperties);
                }
            }
            //Debug
            //Console.WriteLine("Array Address Owned is empty. - customer class - listproperties."); //Debug
            return (AddressOwned);
        }

        //clear this peroperty from user propertys update propertynum for current customer
        public void SellUserProperty(string MyAddress)
        {
            for (int i = 0; i < UserProperties.Length; i++)
            {
                if (MyAddress == UserProperties[i])
                {
                    UserProperties[i] = "";
                    propertyNum--;
                }
            }
        }
    }
}
