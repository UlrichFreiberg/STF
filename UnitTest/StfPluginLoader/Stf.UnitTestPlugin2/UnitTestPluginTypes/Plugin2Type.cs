// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plugin2Type.cs" company="Foobar">
//   2015
// </copyright>
// <summary>
//   Defines the Plugin2Type type.
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
        /// The plugin 2 type func.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Plugin2TypeFunc()
        {
            return 202;
        }

        public IStfContainer StfContainer { get; set; }
    }
}
