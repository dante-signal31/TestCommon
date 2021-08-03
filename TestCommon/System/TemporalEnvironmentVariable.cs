using System;

namespace TestCommon.System
{
    /// <summary>
    /// Context manager to create an environment variable to perform test with it.
    /// 
    /// If environment variable already existed then former value is stored before
    /// setting the new one. Former value is restored when context manager is left.
    /// If variable did not exist before then it is removed.
    /// This context manager returns an instance that let you update env var value using
    /// its set_var method.
    ///
    /// Be aware that depending of variable context you may need administrative access
    /// to set it. 
    /// </summary>
    public class TemporalEnvironmentVariable: IDisposable
    {
        private string _oldValue;
        private string _name;
        private string _value;
        private EnvironmentVariableTarget _context;

        /// <summary>
        /// Temporal environment variable name.
        /// </summary>
        public string Name => _name;
        
        /// <summary>
        /// Temporal environment variable value.
        /// </summary>
        public string Value => _value;
        
        /// <summary>
        /// Constructor for TemporalEnvironmentVariable context manager.
        /// </summary>
        /// <param name="name">Name of environment variable.</param>
        /// <param name="value">Value that environment variable should have.</param>
        /// <param name="context">Optionally you can set context where to look for this variable.
        /// Possible values are: EnvironmentVariableTarget.Process, EnvironmentVariableTarget.User and
        /// EnvironmentVariableTarget.Machine. Default is EnvironmentVariableTarget.Process.
        /// </param>
        public TemporalEnvironmentVariable(string name, string value, EnvironmentVariableTarget context=EnvironmentVariableTarget.Process)
        {
            _oldValue = Environment.GetEnvironmentVariable(name, context);
            _name = name;
            setVar(value);
            _context = context;
        }

        /// <summary>
        /// Update temporal environment value.
        /// </summary>
        /// <param name="newValue">New value.</param>
        public void setVar(string newValue)
        {
            Environment.SetEnvironmentVariable(_name, newValue, _context);
            _value = newValue;
        }
        
        /// <summary>
        /// On dispose already existing environment variable value is restored. If it didn't exist before then
        /// variable is removed. 
        /// </summary>
        public void Dispose()
        {
            // To delete an environment variable just set it to null. So if original value was null (variable 
            // did not exist) then restoring null will remove it.
            Environment.SetEnvironmentVariable(_name, _oldValue, _context);
        }
    }
}