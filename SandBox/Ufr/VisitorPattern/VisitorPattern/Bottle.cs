// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bottle.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the Bottle type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VisitorPattern
{
    /// <summary>
    /// The bottle.
    /// </summary>
    public class Bottle : IAcceptor
    {
        // Unsigned
        public uint Items { get; set; }

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
