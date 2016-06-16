// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the MainPageTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DemoCorpWebTests
{
    using DemoCorp.Stf.DemoCorpWeb;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;

    /// <summary>
    /// The main page tests.
    /// </summary>
    [TestClass]
    public class MainPageTests : StfTestScriptBase
    {
        /// <summary>
        /// The test learn more.
        /// </summary>
        [TestMethod]
        public void TestLearnMore()
        {
            var dcwShell = Get<IDemoCorpWebShell>();
            var learnMore = dcwShell.LearnMore();

            StfAssert.IsNotNull("learnMore", learnMore);
        }
    }
}
