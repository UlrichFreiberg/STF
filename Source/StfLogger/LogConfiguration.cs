﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogConfiguration.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities
{
    using Mir.Stf.Utilities.Configuration;

    /// <summary>
    /// The log configuration.
    /// </summary>
    public partial class StfLogger
    {
        public IStfLoggerConfiguration Configuration { get; set; }
    }
}
