using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsEmployee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public List<string> Phones { get; set; } = new List<string>();
        public float Salary { get; set; }
        enMode _Mode  = enMode.AddNew;
        public clsEmployee()
        {
            ID = -1;
            FirstName = string.Empty;
            MidName = string.Empty;
            LastName = string.Empty;
            Position = string.Empty;
            Email = string.Empty;
            Phones = new List<string>();
            Salary = 0;
        }
        private clsEmployee(int employeeID, string firstName, string midName, string lastName, string position, string email, List<string> phones, float salary)
        {
            ID = employeeID;
            FirstName = firstName;
            MidName = midName;
            LastName = lastName;
            Position = position;
            Email = email;
            Phones = phones;
            Salary = salary;
            _Mode = enMode.Update;
        }

        enum enMode { AddNew = 1, Update}

        private bool _AddNewEmployee()
        {
            this.ID = clsEmployeesDataAccess.AddNewEmployee(FirstName, MidName, LastName, Position, Email, Phones.ToArray(), Salary);
            return this.ID != -1;
        }

        public static clsEmployee Find(int EmployeeID)
        {
            

            string FirstName = "", MidName = "", LastName = "" , Position = "", Email = "" ;
            string[] Phones = new string[10];
            float Salary = 0;

            if (clsEmployeesDataAccess.GetEmployeeByID(EmployeeID, ref FirstName, ref MidName, ref LastName, ref Position, ref Email, ref Phones, ref Salary))
            {
                return new clsEmployee(employeeID: EmployeeID, firstName: FirstName, midName: MidName, lastName: LastName, position: Position, email: Email, phones: Phones.ToList(), salary: Salary);
            }
            else
            {
                return null;
            }
        }

        private bool _UpdateEmployee()
        {
            return clsEmployeesDataAccess.UpdateEmployee(ID, FirstName, MidName, LastName, Position, Email, Salary , Phones.ToArray());
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewEmployee())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return (_UpdateEmployee());

            
            }
            return false;
        }

        static public bool Delete(int ID)
        {
            if (!isEmployeeExist(ID)) 
                return false;
            return clsEmployeesDataAccess.DeleteEmployee(ID);
        }

        public static DataTable GetAllEmployees()
        {
            return clsEmployeesDataAccess.GetAllEmployees();
        }

        public static bool isEmployeeExist(int EmployeeID)
        {
            return clsEmployeesDataAccess.isEmployeeExist(EmployeeID);
        }
        

    }
    
    
}
