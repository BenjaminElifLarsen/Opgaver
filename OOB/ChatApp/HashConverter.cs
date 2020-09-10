using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatApp
{
    /// <summary>
    /// Class that converts variables into hashes. 
    /// </summary>
    static class HashConverter
    {
        /// <summary>
        /// Converts a string to a hash using SHA256 and returns the hash as a string. 
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <returns>The hash, converted to a string, of <paramref name="input"/>.</returns>
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
