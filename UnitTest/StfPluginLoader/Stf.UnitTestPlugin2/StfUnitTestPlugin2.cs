// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfUnitTestPlugin2.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines one unit test IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Stf.Unittests
{
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
            this.VersionInfo = new Version(2, 0, 0, 0);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the version info.
        /// </summary>
        public Version VersionInfo { get; private set; }

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

        /// <summary>
        /// The stf unit test plugin 2 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int StfUnitTestPlugin2Func()
        {
            return 102;
        }
    }
}
