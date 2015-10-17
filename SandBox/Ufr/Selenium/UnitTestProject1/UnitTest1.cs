// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTestProject1
{
    using System.IO;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WebTablesToWebTableUtils;

    /// <summary>
    /// Unit tests for TableUtils
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Url to the static web table
        /// </summary>
        private string url;

        /// <summary>
        /// Gets or sets the testcontext
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Init the url to point to the deployed webtable
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            url = Path.Combine(TestContext.TestDeploymentDir, @"XMLFile2.xml");
        }

        /// <summary>
        /// Test for headers
        /// </summary>
        [TestMethod]
        public void TestFindHeaders()
        {
            var webTableHeadersAndRows = new WebTableHeadersAndRows();
            webTableHeadersAndRows.Init(url);

            var headers = webTableHeadersAndRows.FindHeaders();
            Assert.AreEqual(21, headers.Count);
        }

        /// <summary>
        /// Test for rows
        /// </summary>
        [TestMethod]
        public void TestFindAllRows()
        {
            var webTableHeadersAndRows = new WebTableHeadersAndRows();
            webTableHeadersAndRows.Init(url);

            var rows = webTableHeadersAndRows.FindAllRows();
            Assert.AreEqual(14, rows.Count);
        }
    }
}
