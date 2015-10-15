// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStfAdapterBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IStfAdapterBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Mir.Stf.Utilities.Interfaces
{
    /// <summary>
    /// The StfAdapterBase interface.
    /// </summary>
    public interface IStfAdapterBase : IStfLoggable, IStfGettable
    {
        /// <summary>
        /// The get.
        /// </summary>
        /// <typeparam name="T">
        /// The requested type to be resolved by the container
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T Get<T>();
    }
}
