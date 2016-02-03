// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PillCountVisitor.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the PillCountVisitor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace VisitorPattern
{
    /// <summary>
    /// The pill count visitor.
    /// </summary>
    public class PillCountVisitor : IVisitor
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="blisterPack">
        /// The blister pack.
        /// </param>
        public void Visit(BlisterPack blisterPack)
        {
            Count += blisterPack.TabletPairs * 2;
        }

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="bottle">
        /// The bottle.
        /// </param>
        public void Visit(Bottle bottle)
        {
            Count += (int)bottle.Items;
        }

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="jar">
        /// The jar.
        /// </param>
        public void Visit(Jar jar)
        {
            Count += jar.Pieces;
        }
    }
}
