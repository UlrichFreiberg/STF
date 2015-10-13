// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestTableUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf.Utilities.TableUtils;

namespace UnitTest
{
    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTestTableUtils
    {
        /// <summary>
        /// The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string[] header = { "et", "to", "tre" };
            var wtu = new TableUtils(header);

            Assert.IsTrue(wtu.Columns[0].Name == "et");
        }

        /// <summary>
        /// The test method 2.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            string[] header = { "fornavn", "efternavn", "gadeNavn", "gadeNummer" };
            var wtu = new TableUtils(header);
            var rowType = wtu.GetRowType();

            Assert.IsTrue(rowType.gadeNavn == string.Empty);
            rowType.gadeNavn = "Æblestien";
            Assert.IsTrue(rowType.gadeNavn == "Æblestien");
            Assert.IsTrue(wtu.Columns[0].Name == "fornavn");
        }

        /// <summary>
        /// The test method 3.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            string[] header = { "fornavn", "efternavn", "gadeNavn", "gadeNummer" };
            string[] row = { "Ulrich", "Freiberg", "Æblestien", "13" };
            var wtu = new TableUtils(header);
            var rowType = wtu.GetRowType(row);

            Assert.IsTrue(rowType.gadeNavn == "Æblestien");
            Assert.IsTrue(wtu.Columns[0].Name == "fornavn");
        }

        /// <summary>
        /// The test projection.
        /// </summary>
        [TestMethod]
        public void TestProjection()
        {
            string[] header = { "fornavn", "efternavn", "gadeNavn", "gadeNummer" };
            string[] row = { "Ulrich", "Freiberg", "Æblestien", "13" };

            var tableUtils = new TableUtils(header);
            var rowType = tableUtils.Projection<UnitTestProjection>(row);

            Assert.IsTrue(rowType.Gadenavn == "Æblestien");
        }

        /// <summary>
        /// The unit test projection.
        /// </summary>
        public class UnitTestProjection
        {
            /// <summary>
            /// Gets or sets the fornavn.
            /// </summary>
            public string Fornavn { get; set; }

            /// <summary>
            /// Gets or sets the gadenavn.
            /// </summary>
            public string Gadenavn { get; set; }

            /// <summary>
            /// Gets or sets the not used for prjection.
            /// </summary>
            public string NotUsedForPrjection { get; set; }
        }
    }
}
