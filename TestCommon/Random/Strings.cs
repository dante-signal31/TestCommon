using System;
using System.Linq;

namespace TestCommon.Random
{
    /// <summary>
    /// Random strings related functions.
    /// </summary>
    public class Strings
    {
        /// <summary>
        /// Create a random string with given length.
        ///
        /// Generated string will have any of these characters: "qwertyuiopasdfghjklzxcvbnm1234567890".
        /// </summary>
        /// <param name="length">Length of desired generated string.</param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            string alphanumericAlphabet = "qwertyuiopasdfghjklzxcvbnm1234567890";
            int alphanumericAlphabetLength = alphanumericAlphabet.Length;
            global::System.Random rnd = new global::System.Random();
            string generatedString = "";
            foreach (int i in Enumerable.Range(0, length))
            {
                char selectedChar = alphanumericAlphabet[rnd.Next(alphanumericAlphabetLength)];
                generatedString= String.Concat(generatedString, selectedChar);
            }
            return generatedString;
        }
    }
}