// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginDocDemo.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DemoCorp.Stf.PluginDocDemo.Interfaces
{
    using Mir.Stf.Utilities;

    /// <summary>
    /// Interface for PluginDoc Demo 
    /// </summary>
    public interface IPluginDocDemo : IStfPlugin
    {
        /// <summary>
        /// Print the two values from the configuration
        /// </summary>
        void PrintValues();
    }
}
