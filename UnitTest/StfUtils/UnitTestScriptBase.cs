// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestScriptBase.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   Defines the UnitTestScriptBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest
{
    using Mir.Stf;
    using Mir.Stf.Utilities;
    using Mir.Stf.Utilities.Interfaces;

    /// <summary>
    /// The unit test utilities.
    /// All StfUtils classes should never be new-ed up - they should be reached by using this class
    /// In that way we test the interfaces are up to date 
    /// </summary>
    public class UnitTestScriptBase : StfTestScriptBase
    {
        /// <summary>
        /// The stf utils base.
        /// to leverage the caching of objects and ensure interface setup
        /// we are using the same methods to get as the StfUtils class
        /// </summary>
        private readonly StfUtilsBase stfUtilsBase = new StfUtilsBase();

        /// <summary>
        /// The string transformation utils.
        /// </summary>
        public IStringTransformationUtils StringTransformationUtils => stfUtilsBase.StringTransformationUtils;

        /// <summary>
        /// The file utils.
        /// </summary>
        public IFileUtils FileUtils => stfUtilsBase.FileUtils;

        /// <summary>
        /// The create file utils test file.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <param name="createFile">
        /// The create File.
        /// </param>
        /// <param name="content">
        /// The intended content of the file.
        /// </param>
        public void CreateFileUtilsTestFile(string filename, bool createFile, string content = "UnitTestStuff")
        {
            if (!createFile)
            {
                return;
            }

            var ok = FileUtils.WriteAllTextFile(filename, content);

            StfAssert.IsTrue("Was able to create the test file", ok);
        }
    }
}
