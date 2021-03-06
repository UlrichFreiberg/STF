﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StfUnitTestPlugin1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The Plugin interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Mir.Stf.Utilities;
using Mir.Stf.Utilities.Interfaces;

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
        /// Gets or sets the my logger.
        /// </summary>
        public IStfLogger StfLogger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is initialized.
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <summary>
        /// The init.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Init()
        {
            IsInitialized = true;
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
