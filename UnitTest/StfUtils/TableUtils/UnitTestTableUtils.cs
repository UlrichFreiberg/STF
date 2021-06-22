// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestTableUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.TableUtils
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf.Utilities.TableUtilities;

    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestClass]
    public class UnitTestTableUtils : UnitTestScriptBase
    {
        /// <summary>
        /// The test method 1.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string[] header = { "et", "to", "tre" };
            var wtu = new TableUtils(header);

            StfAssert.StringEquals("Et", wtu.Columns[0].Name, "et");
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

            StfAssert.StringEmpty("Empty", rowType.gadeNavn);

            rowType.gadeNavn = "Æblestien";
            StfAssert.StringEquals("Gadenavn", "Æblestien", rowType.gadeNavn);
            StfAssert.StringEquals("fornavn", "fornavn", wtu.Columns[0].Name);
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

            StfAssert.StringEquals("Æblestien", "Æblestien", rowType.gadeNavn);
            StfAssert.StringEquals("fornavn", "fornavn", wtu.Columns[0].Name);
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

            StfAssert.StringEquals("Æblestien", "Æblestien", rowType.Gadenavn);
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
            /// Gets or sets the not used for projection.
            /// </summary>
            public string NotUsedForProjection { get; set; }
        }
    }
}
