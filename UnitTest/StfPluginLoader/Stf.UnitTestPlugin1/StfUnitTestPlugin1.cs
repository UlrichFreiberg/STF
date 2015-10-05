// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfUnitTestPlugin1.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines one unit test IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Stf.Utilities;

namespace Stf.Unittests
{
    /// <summary>
    /// The Plugin interface.
    /// </summary>
    public class StfUnitTestPlugin1 : IStfUnitTestPlugin1
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StfUnitTestPlugin1"/> class.
        /// </summary>
        public StfUnitTestPlugin1()
        {
            this.Name = "StfUnitTestPlugin1";
            this.VersionInfo = new Version(1, 0, 0, 0);
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
        /// Gets or sets the stf container.
        /// </summary>
        public IStfContainer StfContainer { get; set; }

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
        /// The stf unit test plugin 1 func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int StfUnitTestPlugin1Func()
        {
            return 101;
        }
    }
}
