using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WinFormsApp1.DAO;
using WinFormsApp1.Entities;

namespace WinFormsApp1.BL
{
    internal class UserService
    {
        static string SetMD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new StringBuilder to hold the hashed data
                StringBuilder builder = new StringBuilder();

                // Loop through each byte of the hashed data and format it as a hexadecimal string
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string representation of the hashed data
                return builder.ToString();
            }
        }
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object. 
            MD5 md5Hasher = MD5.Create();
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes 
            // and create a string. 
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
        public static string logInToServer(string username, string password)
        {
            UserDAO users = new UserDAO();
            User user = users.getUser(username, getMd5Hash(password));
            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    return "Admin";
                }
                else
                {
                    return "Employee";
                }
            }
            else
            {
                return "Try again";
            }
        }
        public static void CreateNewEmployee(string newEmployeeName, string newEmployeeUsername, string newEmployeePassword)
        {
            UserDAO userDAO = new UserDAO();
            List<User> users = userDAO.GetListUsers();
            User user = new User(users.Count+1, newEmployeeName, "Employee", 
                newEmployeeUsername, SetMD5Hash(newEmployeePassword));
           userDAO.InsertNewUser(user);
        }
    }
}
