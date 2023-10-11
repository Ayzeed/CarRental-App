using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class Utils
    {
        public static bool FormIsOpen(string name)
        {
            var openForms = System.Windows.Forms.Application.OpenForms.Cast<System.Windows.Forms.Form>();
            var isOpen = openForms.Any(q => q.Name == name);
            return isOpen;
        }
        public static string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();

            // convert the input string to a byte array and compute the hash
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            // create a new Stringbuilder to collect the bytes and create a string
            StringBuilder sBuilder = new StringBuilder();

            // loop through each byte of the hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // return the hexadecimal string
            return sBuilder.ToString();
        }

        public static string DefaultHashPassword()
        {
            SHA256 sha = SHA256.Create();

            // convert the input string to a byte array and compute the hash
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes("Password@123"));

            // create a new Stringbuilder to collect the bytes and create a string
            StringBuilder sBuilder = new StringBuilder();

            // loop through each byte of the hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // return the hexadecimal string
            return sBuilder.ToString();
        }
    }
}
