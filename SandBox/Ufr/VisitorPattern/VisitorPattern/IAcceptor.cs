// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAcceptor.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the IAcceptor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VisitorPattern
{
    /// <summary>
    /// The i acceptor.
    /// </summary>
    public interface IAcceptor
    {
        /// <summary>
        /// The accept.
        /// </summary>
        /// <param name="visitor">
        /// The visitor.
        /// </param>
        void Accept(IVisitor visitor);
    }
}
