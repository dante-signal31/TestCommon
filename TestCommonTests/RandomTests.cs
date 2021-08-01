using NUnit.Framework;
using TestCommon.Random;

namespace TestCommonTests
{
    public class RandomTests
    {
        [Test]
        public void TestRandomStringLength()
        {
            const int desiredLength = 7;
            string generatedString = Strings.RandomString(desiredLength);
            int generatedLength = generatedString.Length;
            Assert.AreEqual(desiredLength, generatedLength);
        }
    }
}