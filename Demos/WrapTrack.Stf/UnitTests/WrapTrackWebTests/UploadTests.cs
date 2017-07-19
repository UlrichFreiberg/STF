// -----------------------------------------!---------------------------------------------------------------------------
// <copyright file="UploadTests.cs" company="Mir Software">
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
    public class UploadTests : StfTestScriptBase
    {
        /// <summary>
        /// The test learn more.
        /// </summary>
        [TestMethod]
        public void TestUploadProfilePictExistingImage()
        {
            var wrapTrackShell = Get<IWrapTrackWebShell>();
            var me = wrapTrackShell.Me();
            var uploadProfileImage = me.UploadProfileImage();
            var oldId = uploadProfileImage.Identification;

            uploadProfileImage.FileName = @"C:\temp\TestData\Kurt.png";
            uploadProfileImage.Upload();

            var newId = uploadProfileImage.Identification;

            StfAssert.AreNotEqual("New Picture Id", oldId, newId);
        }
    }
}
