// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPageTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the MainPageTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WrapTrackWebTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;

    using WrapTrack.Stf.WrapTrackWeb.Interfaces;

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
        public void TestLogin()
        {
            var wrapTrackShell = Get<IWrapTrackWebShell>();
            var collection = wrapTrackShell.Collection();

            StfAssert.IsNotNull("wrapTrackShell", wrapTrackShell);
            StfAssert.IsNotNull("collection", collection);
        }
    }
}
