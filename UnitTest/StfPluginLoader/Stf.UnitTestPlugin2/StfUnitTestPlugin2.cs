// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfUnitTestPlugin2.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines one unit test IPlugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Mir.Stf.Utilities;
using Stf.Unittests.UnitTestPluginTypes;

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
            try
            {
                StfContainer.RegisterType<IPlugin2Type, Plugin2Type>();
            }
            catch (Exception)
            {
                return false;
            }

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
