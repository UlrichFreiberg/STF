using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrentDirectoryTest
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    using Mir.Stf;

    [TestClass]
    public class CurrentDirectoryTest : StfTestScriptBase
    {
        [TestMethod]
        public void TestFromTestMethod()
        {
            var info = "From TestMethods" + Environment.NewLine + GetCurrentDirInfo();

            MyLogger.Configuration.MapNewlinesToBr = true;
            MyLogger.LogInfo(info);
        }

        private string GetCurrentDirInfo()
        {
            var retVal = string.Empty;
            var assemblyGetEntryAssembly = Assembly.GetEntryAssembly();

            retVal = addOneFunctionResult(retVal, "Environment.CurrentDirectory", Path.GetDirectoryName(Environment.CurrentDirectory));
            retVal = addOneFunctionResult(retVal, "Directory.GetCurrentDirectory()", Path.GetDirectoryName(Directory.GetCurrentDirectory()));

            if (assemblyGetEntryAssembly != null)
            {
                retVal = addOneFunctionResult(retVal, "getEntryAssembly.Location", Path.GetDirectoryName(assemblyGetEntryAssembly.Location));
                retVal = addOneFunctionResult(retVal, "new Uri(getEntryAssembly.CodeBase).LocalPath", Path.GetDirectoryName(new Uri(assemblyGetEntryAssembly.CodeBase).LocalPath));
            }

            retVal = addOneFunctionResult(retVal, "Environment.GetCommandLineArgs()[0]", Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
            retVal = addOneFunctionResult(retVal, "Process.GetCurrentProcess().MainModule.FileName", Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));

            if (TestContext != null)
            {
                retVal = addOneFunctionResult(retVal, "TestContext.ResultsDirectory", Path.GetDirectoryName(TestContext.ResultsDirectory));
                retVal = addOneFunctionResult(retVal, "TestContext.DeploymentDirectory", Path.GetDirectoryName(TestContext.DeploymentDirectory));
                retVal = addOneFunctionResult(retVal, "TestContext.TestResultsDirectory", Path.GetDirectoryName(TestContext.TestResultsDirectory));
                retVal = addOneFunctionResult(retVal, "TestContext.TestRunDirectory", Path.GetDirectoryName(TestContext.TestRunDirectory));
                retVal = addOneFunctionResult(retVal, "TestContext.TestRunResultsDirectory", Path.GetDirectoryName(TestContext.TestRunResultsDirectory));
            }

            retVal = addOneFunctionResult(retVal, "AppDomain.CurrentDomain.BaseDirectory", Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));
            // retVal = addOneFunctionResult(retVal, "Application.StartupPath", Path.GetDirectoryName(Application.StartupPath));
            // retVal = addOneFunctionResult(retVal, "Application.ExecutablePath", Path.GetDirectoryName(Application.ExecutablePath));

            return retVal;
        }

        private string addOneFunctionResult(string result, string function, string functionResult)
        {
            var retVal = string.Format("{0} --> [{1}]{2}", function.PadLeft(40), functionResult, Environment.NewLine);

            retVal += result;
            return retVal;
        }
    }
}
