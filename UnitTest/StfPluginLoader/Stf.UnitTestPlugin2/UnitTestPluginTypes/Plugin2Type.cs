// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plugin2Type.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   The plugin 2 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace Stf.Unittests.UnitTestPluginTypes
{
    /// <summary>
    /// The plugin 2 type.
    /// </summary>
    public class Plugin2Type : IPlugin2Type
    {
        /// <summary>
        /// Gets or sets the stf container.
        /// </summary>
        public IStfContainer StfContainer { get; set; }

        /// <summary>
        /// The plugin 2 type func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Plugin2TypeFunc()
        {
            return 202;
        }
    }
}
