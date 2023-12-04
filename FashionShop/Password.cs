using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WindowsFormsApplication2.Utilities
{
    class Password
    {
        //ma hoa mat khau
        public static string Hash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");//.ToLower();
        }
        //kiem tra mat khau
        public static bool verify(string password, string hashPassword, string algorithm = "bcrypt")
        {
          //  if (algorithm == "bcrypt") return BCrypt.Net.BCrypt.Verify(password, hashPassword);
            if (algorithm == "md5") return Password.Create_MD5(password) == hashPassword;
            if (algorithm == "sha1") return Password.Create_SHA1(password) == hashPassword;
            if (algorithm == "sha256") return Password.Create_SHA256(password) == hashPassword;
            return false;
        }
        public static string Create_MD5(string text)
        {
            return Password.Hash(text, new MD5CryptoServiceProvider());

          
        }

        public static string Create_Bcrypt(string text)
        {
           // return BCrypt.Net.BCrypt.HashPassword(text);
            return "";
        }

        public static string Create_SHA1(string text)
        {
            return Hash(text, new SHA1CryptoServiceProvider());
           }
        public static string Create_SHA256(string text)
        {

            return Hash(text, new SHA256CryptoServiceProvider());
           
        }

    }
}
