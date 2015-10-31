using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mir.Stf;

namespace DocDemo
{
    [TestClass]
    public class DocDemoTests : StfTestScriptBase
    {
        [TestMethod]
        public void TestLogInfo()
        {
            MyLogger.LogInfo("Demo");
            MyAssert.LessThan("Some Test Step", 5, 42);
        }
    }
}
