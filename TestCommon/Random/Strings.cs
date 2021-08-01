using System;
using System.Linq;

namespace TestCommon.Random
{
    public class Strings
    {
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