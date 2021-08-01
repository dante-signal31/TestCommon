using NUnit.Framework;
using TestCommon.random;

namespace TestCommonTests
{
    public class RandomTests
    {
        [Test]
        public void TestRandomStringLength()
        {
            const int desiredLength = 7;
            string generatedString = strings.RandomString(desiredLength);
            int generatedLength = generatedString.Length;
            Assert.AreEqual(desiredLength, generatedLength);
        }
    }
}