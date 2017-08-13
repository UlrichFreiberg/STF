// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestPluginModel2.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The test plugin 2 model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The test plugin 2 model.
    /// </summary>
    public class TestPluginModel2 : StfModelBase, ITestPluginModel2
    {
        /// <summary>
        /// The test prop.
        /// </summary>
        private string testProp = "Default";

        /// <summary>
        /// Gets or sets the test prop.
        /// </summary>
        public string TestProp
        {
            get { return testProp; }
            set { testProp = value; }
        }

        /// <summary>
        /// The test plugin 2 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int TestPlugin2Func()
        {
            return 203;
        }

        /// <summary>
        /// The test plugin 2 func with params.
        /// </summary>
        /// <param name="param1">
        /// The param 1.
        /// </param>
        /// <param name="param2">
        /// The param 2.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string TestPlugin2FuncWithParams(string param1, int param2)
        {
            return $"{param1}={param2}";
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(ITestPluginModel2 other)
        {
            return other.TestProp == TestProp;
        }
    }
}
