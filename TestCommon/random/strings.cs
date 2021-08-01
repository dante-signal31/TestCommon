using System;
using System.Linq;


namespace TestCommon.random
{
    public class strings
    {
        public static string RandomString(int length)
        {
            string alphanumericAlphabet = "qwertyuiopasdfghjklzxcvbnm1234567890";
            int alphanumericAlphabetLength = alphanumericAlphabet.Length;
            Random rnd = new Random();
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