using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    using Mir.Stf;
    using Mir.Stf.Utilities;

    [TestClass]
    public class UnitTest1 : StfTestScriptBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            StfLogger.LogScreenshot(StfLogLevel.Info, "Bare en test");
        }
    }
}
