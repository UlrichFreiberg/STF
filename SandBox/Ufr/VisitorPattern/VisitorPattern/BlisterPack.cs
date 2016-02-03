// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlisterPack.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the BlisterPack type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VisitorPattern
{
    /// <summary>
    /// The blister pack.
    /// </summary>
    public class BlisterPack : IAcceptor
    {
        /// <summary>
        /// Gets or sets the tablet pairs.        // Pairs so x2
        /// </summary>
        public int TabletPairs { get; set; }

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
