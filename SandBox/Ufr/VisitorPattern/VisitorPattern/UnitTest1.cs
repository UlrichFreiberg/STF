// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisitorPattern
{
    using System.Collections.Generic;

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// The visitor should count package items.
        /// </summary>
        [TestMethod]
        public void VisitorShouldCountPackageItems()
        {
            const int Expected = 600;

            var packageList = new List<object>
                    {
                        new Jar { Pieces = 275 },
                        new Jar { Pieces = 25 },
                        new Bottle { Items = 100 },
                        new BlisterPack { TabletPairs = 25 },
                        new Jar { Pieces = 150 },
                    };

            var visitor = new PillCountVisitor();

            foreach (IAcceptor item in packageList)
            {
                item.Accept(visitor);
            }

            Assert.AreEqual(Expected, visitor.Count);
        }
    }
}
