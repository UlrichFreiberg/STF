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
    public class StfUnitTestPlugin2 : IStfUnitTestPlugin2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfUnitTestPlugin2"/> class.
        /// </summary>
        public StfUnitTestPlugin2()
        {
            this.Name = "StfUnitTestPlugin2";
            this.VersionInfo = "StfUnitTestPlugin2 V2.0";
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

        public int StfUnitTestPlugin2Func()
        {
            return 102;
        }
    }
}
