using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication
{
    class CustomerCollections
    {
        //change this to enable more customers on the application
        private const int CustomerLimit = 5;
        public Customer[] CustomerArray;
        private int CurrentCustNum;

        public CustomerCollections()
        {
            CustomerArray = new Customer[CustomerLimit];
            CurrentCustNum = 0;
        }

        //checks if customer exists
        public bool CheckCustomer(string name)
        {
            for (int i = 0; i < CurrentCustNum; i++)
            {
                if (name.Equals(CustomerArray[i].CustomerName()) || (name.Equals(CustomerArray[i].CustomerEmail())))
                {
                    return true;
                }
            }
            return false;
        }

        //add new customer object - check if customer already exists or max limit is reached
        public void AddNewCustomer(Customer customer)
        {
            if (customer != null)
            {
                if ((CheckCustomer(customer.Name) == true) && (CheckCustomer(customer.Email) == true))
                {
                    Console.WriteLine("Customer Already Exisits - Please Try again!");
                    return;
                }
                else if (CurrentCustNum == CustomerLimit)
                {
                    Console.WriteLine("Maximum Customer Limit Reached - Max Limit is 5 Customers!");
                    return;
                }
                else
                {
                    CustomerArray[CurrentCustNum] = customer;
                    CurrentCustNum++;
                    UserInterface.Message($"{customer.Name} registered successfully. There is now {CurrentCustNum} registered customers.");
                }
            }
            else
            {
                UserInterface.Message("Invalid Customer Credentials - Try again.");
            }
        }

        //login this customer - check if details are correct and if there are any registered customers
        public Customer CustomerLogIn(string email, string password)
        {
            if (CustomerArray[0] != null)
            {
                for (int i = 0; i < CurrentCustNum; i++)
                {
                    if ((email == CustomerArray[i].CustomerEmail()) && (password == CustomerArray[i].CustomerPassword()))
                    {
                        return CustomerArray[i];
                    }
                }
                Console.WriteLine("Login information is incorrect.\n");
                return null;
            }
            else
            {
                Console.WriteLine("There are no Customers registered.\n");
                return null;
            }
        }
    }
}
