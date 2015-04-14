// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfPlugin.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines one unit test IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stf.Unittests
{
    using Stf.Utilities;

    /// <summary>
    /// The Plugin interface.
    /// </summary>
    public class StfUnitTestPlugin1 : IStfPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfUnitTestPlugin1"/> class.
        /// </summary>
        public StfUnitTestPlugin1()
        {
            this.Name = "StfUnitTestPlugin1";
            this.VersionInfo = "StfUnitTestPlugin1 V1.0";
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the version info.
        /// </summary>
        public string VersionInfo { get; private set; }

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init()
        {
            return true;
        }
    }
}
