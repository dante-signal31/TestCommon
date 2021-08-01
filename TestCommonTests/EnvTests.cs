using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TestCommon.Random;
using TestCommon.System;

namespace TestCommonTests
{
    public class EnvTests
    {
        /// <summary>
        /// Get a random name for an environment variable after checking it does not exist yet.
        /// </summary>
        /// <param name="context">Optionally you can set context where to look for this variable.
        /// Possible values are: EnvironmentVariableTarget.Process, EnvironmentVariableTarget.User and
        /// EnvironmentVariableTarget.Machine. Default is EnvironmentVariableTarget.Machine.</param>
        /// <returns></returns>
        private static string GetNotExistingEnvVarName(EnvironmentVariableTarget context = EnvironmentVariableTarget.Process)
        {
            const int desiredLength = 10;
            string randomName = "";
            string recoveredValue = "";
            do {
                randomName = Strings.RandomString(desiredLength);
                recoveredValue = Environment.GetEnvironmentVariable(randomName, context);
            } while (recoveredValue != null);
            return randomName;
        }

        [Test]
        public void TestNotPreviouslyExistingEnvironmentVariableCreation()
        {
            const string DesiredValue = "Hello";
            string newVarName = GetNotExistingEnvVarName();
            Assert.True(Environment.GetEnvironmentVariable(newVarName) == null);
            using (TemporalEnvironmentVariable temporalEnvironmentVariable = new TemporalEnvironmentVariable(newVarName, DesiredValue))
            {
                string recoveredValue = Environment.GetEnvironmentVariable(newVarName);
                if (recoveredValue == null)
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.True(recoveredValue.Equals(DesiredValue));
                }
            }
        }

        [Test]
        public void TestPreviouslyExistingEnvironmentVariableCreation()
        {
            const string oldValue = "Bye";
            const string desiredValue = "Hello";
            // Create a previous env var.
            string newVarName = GetNotExistingEnvVarName();
            Environment.SetEnvironmentVariable(newVarName, oldValue);
            // Check we can store a new value.
            using (TemporalEnvironmentVariable temporalEnvironmentVariable = new TemporalEnvironmentVariable(newVarName, desiredValue))
            {
                Assert.True(desiredValue.Equals(
                    Environment.GetEnvironmentVariable(newVarName)));
            }
            // Check old value has been restored.
            Assert.True(oldValue.Equals(Environment.GetEnvironmentVariable(newVarName)));
        }

        [Test]
        public void TestSetVar()
        {
            const string desiredValue = "Hello";
            const string newValue = "Hello World";
            // Give a previous value to env var.
            string newVarName = GetNotExistingEnvVarName();
            using (TemporalEnvironmentVariable tempTemporalEnvironmentVariableVar = new TemporalEnvironmentVariable(newVarName, desiredValue))
            {
                Assert.True(desiredValue.Equals(
                    Environment.GetEnvironmentVariable(newVarName)));
                // Update temporalEnvironmentVariable var value.
                tempTemporalEnvironmentVariableVar.setVar(newValue);
                Assert.True(newValue.Equals(
                    Environment.GetEnvironmentVariable(newVarName)));
            }
        }
    }
}