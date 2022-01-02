using NUnit.Framework;
using TestCommon.Lang;

namespace TestCommonTests
{
    class ExampleClass
    {
        private int add_three(int value) => value + 3;
        private int _field = 4;

        public int Field => _field;
    }
    [TestFixture]
    public class AccessPrivateTests
    {
        [Test]
        public void TestAccessPrivateMethod()
        {
            ExampleClass foo = new ExampleClass();
            int result = (int) AccessPrivateHelper.AccessPrivateMethod(foo, "add_three", 2);
            Assert.True(result == 5);
        }

        [Test]
        public void TestAccessPrivateField()
        {
            ExampleClass foo = new ExampleClass();
            int field = AccessPrivateHelper.GetPrivateField<int>(foo, "_field");
            Assert.True(field == 4);
        }

        [Test]
        public void SetPrivateField()
        {
            ExampleClass foo = new ExampleClass();
            int new_value = 5;
            AccessPrivateHelper.SetPrivateField(foo, "_field", new_value);
            Assert.True(foo.Field == new_value);
        }
    }
}