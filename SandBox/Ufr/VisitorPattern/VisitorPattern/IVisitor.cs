// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IVisitor.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IVisitor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VisitorPattern
{
    /// <summary>
    /// The Visitor interface.
    /// </summary>
    public interface IVisitor
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="blisterPack">
        /// The blister pack.
        /// </param>
        void Visit(BlisterPack blisterPack);

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="bottle">
        /// The bottle.
        /// </param>
        void Visit(Bottle bottle);

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="jar">
        /// The jar.
        /// </param>
        void Visit(Jar jar);
    }
}