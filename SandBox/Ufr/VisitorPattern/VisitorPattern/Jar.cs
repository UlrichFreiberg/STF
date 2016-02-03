// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Jar.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Jar type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VisitorPattern
{
    /// <summary>
    /// The jar.
    /// </summary>
    public class Jar : IAcceptor
    {
        /// <summary>
        /// Gets or sets the pieces.
        /// </summary>
        public int Pieces { get; set; }

        /// <summary>
        /// The accept.
        /// </summary>
        /// <param name="visitor">
        /// The visitor.
        /// </param>
        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
