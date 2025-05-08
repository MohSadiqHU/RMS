using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsCustomer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> Phones { get; set; } = new List<string>();
        enMode _Mode = enMode.AddNew;

        public clsCustomer()
        {
            ID = -1;
            FirstName = string.Empty;
            MidName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            Phones = new List<string>();
        }
        private clsCustomer(int employeeID, string firstName, string midName, string lastName, string address, string email, List<string> phones)
        {
            ID = employeeID;
            FirstName = firstName;
            MidName = midName;
            LastName = lastName;
            Address = address;
            Email = email;
            Phones = phones;
           
        }

        enum enMode { AddNew = 1, Update }

        private bool _AddNewCustomer()
        {

            ID = clsCustomerDataAccess.AddNewCustomer(FirstName, MidName, LastName, Address, Email, Phones.ToArray());
            return (ID != -1);
        }

        private bool _UpdateCustomer()
        {
            return clsCustomerDataAccess.UpdateCustomer(ID, FirstName, MidName, LastName,Address,Email,Phones.ToArray());
        }

        public bool DeleteCustomer(int id)
        {
            if (!isCustomerExist(id)) {
                return false;
            }
            return clsCustomerDataAccess.DeleteCustomer(id);
        }

        public bool isCustomerExist(int id)
        {
            return clsCustomerDataAccess.isCustomerExist(id);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCustomer())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCustomer();

            }
            return false;
        }

        static public DataTable GetAllCustomer()
        {
            return clsCustomerDataAccess.GetAllCustomers();
        }
        static public clsCustomer Find(int customerID)
        {
            string FirstName = "", MidName = "", LastName = "", Address = "", Email = "";
            string[] Phones = new string[10];
            if (clsCustomerDataAccess.GetCustomerByID(customerID, ref FirstName, ref MidName, ref LastName, ref Address, ref Email, ref Phones))
            {
                return new clsCustomer(employeeID: customerID, firstName: FirstName, midName: MidName, lastName: LastName, address: Address, email: Email, phones: Phones.ToList());
            }
            else
            {
                return null;
            }
        }

    }
}
