using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstateApplication
{
    class TestUserInterface
    {
        Menu menu;
        Menu Submenu;

        //creating variables for other classes in this application
        private CustomerCollections CustomerDetails;
        private PropertyCollections PropertyDetails;
        private Customer CurrentLoggedInUser;

        //constructor for testuserinterface
        public TestUserInterface()
        {
            menu = new Menu();
            Submenu = new Menu();
            CustomerDetails = new CustomerCollections();
            PropertyDetails = new PropertyCollections();
        }

        public string CurrentlyLoggedUserName()
        {
            return ($"{CurrentLoggedInUser.CustomerName()}");
        }

        public string CurrentlyLoggedUserEmail()
        {
            return ($"{CurrentLoggedInUser.CustomerEmail()}");
        }

        //returns a customer class object, method called register customer to prompt for details and return a new customer 
        //constraints:
        //CustomerName more than 3 in length
        //CustomerEmail more than 5 and contain '@'
        //Password greater than 5 and less than 33
        public Customer RegisterCustomer()
        {
            string CustomerName = UserInterface.GetInput("Full name");
            if (CustomerName.Length > 3)
            {
                string CustomerEmail = UserInterface.GetInput("Email");
                if (CustomerEmail.Length > 4 && CustomerEmail.Contains("@"))
                {
                    string Password = UserInterface.GetPassword("Enter your Password");
                    if (Password.Length > 3 && Password.Length <=32)
                    {
                        return new Customer(CustomerName, CustomerEmail, Password);
                    }
                    else
                    {
                        UserInterface.Message("Password must be between 4 and 32 characters.");
                    }
                }
                else
                {
                    UserInterface.Message("Email must be more than 5 characters and contain '@' symbol.");
                }
            }
            else
            {
                UserInterface.Message("Name must be more than 3 characters.");
            }
            return null;
        }

        //void method called adduser which calls the customer deatils method addnewcustomer with the parameters register customer
        public void AddUser()
        {
            CustomerDetails.AddNewCustomer(RegisterCustomer());
        }

        //void method called userloginmenu which prompts the user to enter details and if true add newloggedinuser
        public void UserLoginMenu()
        {
            string CustomerLoginEmail = UserInterface.GetInput("Email");
            string CustomerPassword = UserInterface.GetPassword("Password");
            if (CustomerDetails.CustomerLogIn(CustomerLoginEmail, CustomerPassword) == null)
            {
                return;
            }
            else
            {
                CurrentLoggedInUser = CustomerDetails.CustomerLogIn(CustomerLoginEmail, CustomerPassword);
                UserInterface.Message($"Welcome {CurrentLoggedInUser.CustomerName()} ({CustomerLoginEmail})");
                Submenu.Display();
            }
        }

        //void method called logout to return the currently logged in user to the main menu - clears userproperties array for next customer
        public void LogOut()
        {
            Console.WriteLine($"{CurrentLoggedInUser.CustomerName()} logged out\n");
            DisplayMainMenu();
        }

        //method called register land to prompt user for details and return new landproperty object
        //constraints:
        //Address must have a lenght more than 5 and not empty 
        //Postcode must be 4 digits and not empty
        //Area must have a lenght more than 1 and not empty
        //Property must not already exist (CheckIfPropertyExists)
        public Property RegisterLand()
        {
            string Address = UserInterface.GetInput("Address");
            if (Address.Length > 5)
            {
                string Postcode = UserInterface.GetInput("Postcode (4-digit)");
                int PostcodeNum = int.Parse(Postcode);
                if (PostcodeNum > 999 && PostcodeNum < 10000)
                {
                    string Area = UserInterface.GetInput("Land size (sqm)");
                    if (Area.Length > 1)
                    {
                        if (PropertyDetails.CheckIfPropertyExists(Address) == false)
                        {
                            CurrentLoggedInUser.NewCustomerProperty(Address);
                            UserInterface.Message($"{Address}, {Postcode}," +
                                    $" Land only {Area}sqm registered successfully");
                            return new Land(Address, Postcode, Area);
                        }
                        else
                        {
                            UserInterface.Message("\nProperty Already Exists.");
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        //void method called addland which calls registerland to add new landproperty object in propertydetails
        public void AddLand()
        {
            PropertyDetails.AddNewProperty(RegisterLand(), CurrentlyLoggedUserName());
            Submenu.Display();
        }

        //method registerhouse to prompt user for details and return new houseproperty object
        //constraints:
        //Address must have a lenght more than 5 and not empty
        //Postcode must be 4 digits and not empty
        //Description must have a lenght more than 5 and not empty
        //Property must not already exist (CheckIfPropertyExists)
        public Property RegisterHouse()
        {
            string Address = UserInterface.GetInput("Address");
            if (Address.Length > 5)
            {
                string Postcode = UserInterface.GetInput("Postcode (4-digit)");
                int PostcodeNum = int.Parse(Postcode);
                if (PostcodeNum > 999 && PostcodeNum < 10000)
                {
                    string Description = UserInterface.GetInput("Enter description of house");
                    if (Description.Length > 5)
                    {
                        if (PropertyDetails.CheckIfPropertyExists(Address) == false)
                        {
                            CurrentLoggedInUser.NewCustomerProperty(Address);
                            UserInterface.Message($"{Address}, {Postcode}," +
                                    $" House and Land {Description} registered successfully");
                            return new House(Address, Postcode, Description);
                        }
                        else
                        {
                            UserInterface.Message("\nProperty Already Exists.");
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        //void method called addhouse which calls registerhouse to add new houseproperty object in propertydetails
        public void AddHouse()
        {
            PropertyDetails.AddNewProperty(RegisterHouse(), CurrentlyLoggedUserName());
            Submenu.Display();
        }

        //void method called listmyproperties which displays the properties owned of the customer from
        //the customer class array userproperties - if current customer owns an address in that array 
        //prompt propertydetails class to printproperties which calls the userproperties array in the customer class
        public void ListMyProperties()
        {
            if (CurrentLoggedInUser.CheckIfCustomerOwnsProperty(CurrentLoggedInUser.ListPropertiesOwned())==true)
            {
                UserInterface.DisplayList($"Properties owned by {CurrentLoggedInUser.CustomerName()}:", 
                    PropertyDetails.PrintProperties(CurrentLoggedInUser.ListPropertiesOwned()));
            }
            else
            {
                UserInterface.Message($"No Properties Registered, Customer: {CurrentLoggedInUser.CustomerName()}.");
            }
            Submenu.Display();
        }

        //void method called List All Properties which displays all properties with the given postcode
        public void ListAllProperties()
        {
            string Postcode = UserInterface.GetInput("Postcode");
            int PostcodeNum = int.Parse(Postcode);
            if (PostcodeNum > 999 && PostcodeNum < 10000)
            {
                PropertyDetails.PrintPropertiesInPostcode(Postcode);
            }
            else
            {
                UserInterface.Message("Postcode Invalid.");
            }
            Submenu.Display();
        }

        //void method called Bid on property that asks for a post code and returns a list of properties with 
        //that postcode, then asks the user to "Please choose one of the follow:" followed by
        //the list of properties and the user chooses an option and then prompted to "Enter Bid ($):" 
        //follow by "CurrentUserName - (CurrentUserEmail) bid ($$$$ amount).
        public void BidOnProperty()
        {

            string Postcode = UserInterface.GetInput("Postcode");
            int PostcodeNum = int.Parse(Postcode);
            if (PostcodeNum > 999 && PostcodeNum < 10000)
            {
                //calling propertydetails to make a new bid on property, with postcode, 
                //currently logged user name and email and currently owned properties
                PropertyDetails.NewBidOnProperty(Postcode, CurrentlyLoggedUserName(), CurrentlyLoggedUserEmail(),
                    CurrentLoggedInUser);
            }
            else
            {
                UserInterface.Message("Postcode Invalid.");
            }
            Submenu.Display();
        }

        //public void method called bidsreceived that shows a list of properties owned
        //asks the customer to pick a property, Bid recevied - list of bids on the particular property
        //customer name (customer email) bid $$
        public void BidsReceived()
        {
            if (CurrentLoggedInUser.CheckIfCustomerOwnsProperty(CurrentLoggedInUser.ListPropertiesOwned()) == true)
            {
                UserInterface.Message($"Properties owned by {CurrentLoggedInUser.CustomerName()}.");
                PropertyDetails.ListBidsRecieved(CurrentLoggedInUser.ListPropertiesOwned());
            }
            else
            {
                UserInterface.Message($"No Properties Registered, Customer: {CurrentLoggedInUser.CustomerName()}.");
            }
            Submenu.Display();
        }

        //public void method called Sellproperty that shows a list of the users properties 
        //asks the user to select from the list -and sells to the highest bidder 
        //Address, postcode, House and land, description (or land only sqm) sold to Customer name
        //customer email for bid amount $ - tax payable $
        public void SellProperty()
        {
            if (CurrentLoggedInUser.CheckIfCustomerOwnsProperty(CurrentLoggedInUser.ListPropertiesOwned()) == true)
            {
                UserInterface.Message($"Properties owned by {CurrentLoggedInUser.CustomerName()}.");
                //customer CurrentlyloggedInUser - calling selluserproperty to remove it from the userproperties array in customer class.
                //property details calling sell this property which takes in the user propertyies array 
                CurrentLoggedInUser.SellUserProperty(PropertyDetails.SellThisProperty
                    (CurrentLoggedInUser.ListPropertiesOwned()));
            }
            else
            {
                UserInterface.Message($"No Properties Registered, Customer: {CurrentLoggedInUser.CustomerName()}.");
            }
            Submenu.Display();
        }

        public void Run()
        {
            //main menu
            menu.Add("Register as a new Customer", AddUser);
            menu.Add("Login as exisitng Customer", UserLoginMenu);

            //customer menu/submenu
            Submenu.Add("Register new land for sale", AddLand);
            Submenu.Add("Register a new house for sale", AddHouse);
            Submenu.Add("List my properties", ListMyProperties);
            Submenu.Add("List bids received for property", BidsReceived);
            Submenu.Add("Sell one of my properties to highest bidder", SellProperty);
            Submenu.Add("Search for a property for sale", ListAllProperties);
            Submenu.Add("Place a bid on a property", BidOnProperty);
            Submenu.Add("Logout", LogOut);
            DisplayMainMenu();
        }

        //forever while loop to display the main menu
        public void DisplayMainMenu()
        {
            while (true)
                menu.Display();
        }

        //main method
        static void Main(string[] args)
        {
            TestUserInterface test = new TestUserInterface();
            test.Run();
        }
    }
}