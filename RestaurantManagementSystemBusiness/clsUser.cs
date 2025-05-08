using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using RestaurantManagementSystemDataAccess;

namespace RestaurantManagementSystemBusiness
{
    public class clsUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        private string PasswordHash { get;  set; }
        public string Password { get; set; }
        private string PasswordSalt { get; set; }
        public int Permission { get; private set; }
        public int EmployeeID {  get; set; }

        private enMode _Mode = enMode.AddNew;

        public enum enMainMenuePermissions
        {
            eAll = -1, pOrders = 1, pEmployees = 2, pUsers = 4,
            pCustomers = 8, pProducts = 16, pIngredients = 32, pPurchases = 64
        };
        enum enMode { AddNew = 1, Update = 2 }

        private string _GenerateSalt(int length = 16)
        {
            var saltBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        private  string _HashPassword(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            var argon2 = new Argon2id(passwordBytes)
            {
                Salt = Encoding.UTF8.GetBytes(salt),
                DegreeOfParallelism = 4,
                MemorySize = 65536, // 64 MB
                Iterations = 4
            };

            var hash = argon2.GetBytes(32);
            return Convert.ToBase64String(hash);
        }
        public clsUser()
        {
            UserID = -1;
            UserName = string.Empty;
            Password = string.Empty;
            PasswordHash = string.Empty;
            PasswordSalt = string.Empty;
            Permission = 0;
            _Mode = enMode.AddNew;
            EmployeeID = -1;
        }
        
        private clsUser(int userID, string userName, string passwordHash, string passwordSalt, int permission, int employeeID)
        {
            UserID = userID;
            UserName = userName;
            Password = null;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Permission = permission;
            EmployeeID = employeeID;
            _Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            PasswordSalt = _GenerateSalt();
            PasswordHash = _HashPassword(Password, PasswordSalt);
            UserID = clsUsersDataAccess.AddNewUser(UserName, PasswordHash, PasswordSalt, Permission, EmployeeID);
            return UserID != -1;
        }
        private bool _UpdateUser()
        {
            PasswordSalt = _GenerateSalt();
            PasswordHash = _HashPassword(Password, PasswordSalt);
            return clsUsersDataAccess.UpdateUser(UserID, UserName, PasswordHash, PasswordSalt, Permission, EmployeeID);
        }
        public static clsUser Find(int ID)
        {
            int  employeeID = 0, permission = 0;
            string userName = "", passwordHash = "", passwordSalt = "";

            clsUsersDataAccess.GetUserByID(ID,ref userName,ref permission,ref employeeID, ref passwordHash, ref passwordSalt);
            return new clsUser(ID, userName, passwordHash, passwordSalt, permission, employeeID);
        }
        public static clsUser Find (string userName)

        {
            int userID = 0, employeeID = 0, permission = 0;
            string passwordHash = "", passwordSalt = "";

            clsUsersDataAccess.GetUserByName(userName,ref userID, ref passwordHash, ref passwordSalt, ref permission,ref employeeID);
            return new clsUser(userID, userName, passwordHash, passwordSalt, permission, employeeID);
        }

        public bool AllowLogin(string password)
        {
            return PasswordHash == _HashPassword(password, PasswordSalt);
        }

        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if( _AddNewUser())
                {
                    _Mode = enMode.Update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return _UpdateUser();
            }
        }

        public void AddPermission(enMainMenuePermissions NewPermission)
        {
            if (NewPermission == enMainMenuePermissions.eAll)
            {
                Permission = (int)NewPermission;
                return;
            }
            Permission |= (int)NewPermission;
        }

        public void RemovePermission(enMainMenuePermissions OldPermission)
        {
            if (OldPermission == enMainMenuePermissions.eAll)
            {
                Permission = 0;
                return;
            }
            Permission &= ~(int)OldPermission;
        }

        public bool HasPermission(enMainMenuePermissions PermissionToCheck)
        {
            if(Permission ==(int) enMainMenuePermissions.eAll)
            {
                return true;
            }
            return (Permission & (int)PermissionToCheck) == (int)PermissionToCheck;
        }
        public static bool Delete(int userID)
        {
            return clsUsersDataAccess.DeleteUser(userID);
        }
        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.GetAllUsers();
        }
        public static bool isUserExists(string UserName)
        {
            return clsUsersDataAccess.isUserExist(UserName);
        }
    }
}
