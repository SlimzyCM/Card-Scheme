using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CardScheme.Commons
{
    /// <summary>
    /// All method that can be used across 
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Create the hash algorithm
        /// </summary>
        /// <param name="inputString">string to hash</param>
        /// <returns></returns>
        private static IEnumerable<byte> GetHash(string inputString)
        {
            using HashAlgorithm algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        /// <summary>
        /// convert hash byte to string 
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static string GetHashString(string inputString)
        {
            var sb = new StringBuilder();

            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        /// <summary>
        /// method to compare the hashed key and the current key
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="timeStamp"></param>
        /// <param name="token"></param>
        public static void CompareHash(string appKey, string timeStamp, string token)
        {
            var inputString = appKey + timeStamp;

            var hashString = GetHashString(inputString);

            if (hashString.Where((t, i) => t != token[i]).Any())
            {
                throw new InvalidDataException("Invalid authorization key");
            }

        }

        /// <summary>
        /// Method to remove invalid character from the card number
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static string NormalizeCardNumber(string cardNumber)
        {
            cardNumber ??= string.Empty;

            var sb = new StringBuilder();

            foreach (var c in cardNumber.Where(char.IsDigit))
            {
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Checks if the card is valid using Lohn Algorithm
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static bool IsCardNumberValid(string cardNumber)
        {
            
            int i, checkSum = 0;

            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                var val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            // Number is valid if sum of both check sums MOD 10 equals 0
            return ((checkSum % 10) == 0 && cardNumber.Length > 12);
        }

        
    }
}
