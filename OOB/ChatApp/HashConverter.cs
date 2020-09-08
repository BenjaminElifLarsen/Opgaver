using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp
{
    static class HashConverter
    {
        static public string StringToHash(string input)
        {
            byte[] inputArray = Encoding.ASCII.GetBytes(input);
            byte[] hashArray;
            using (SHA256 converter = SHA256.Create())
            {
                hashArray = converter.ComputeHash(inputArray);
            }
            string outputHash = "";
            foreach (byte hash in hashArray)
                outputHash += hash.ToString();
            return outputHash;
        }

    }
}
