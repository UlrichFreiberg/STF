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

            retVal = addOneFunctionResult(retVal, "Environment.CurrentDirectory", Environment.CurrentDirectory);
            retVal = addOneFunctionResult(retVal, "Directory.GetCurrentDirectory()", Directory.GetCurrentDirectory());

            if (assemblyGetEntryAssembly != null)
            {
                retVal = addOneFunctionResult(retVal, "getEntryAssembly.Location", assemblyGetEntryAssembly.Location);
                retVal = addOneFunctionResult(retVal, "new Uri(getEntryAssembly.CodeBase).LocalPath", new Uri(assemblyGetEntryAssembly.CodeBase).LocalPath);
            }

            retVal = addOneFunctionResult(retVal, "Environment.GetCommandLineArgs()[0]", Environment.GetCommandLineArgs()[0]);
            retVal = addOneFunctionResult(retVal, "Process.GetCurrentProcess().MainModule.FileName", Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));

            if (TestContext != null)
            {
                retVal = addOneFunctionResult(retVal, "TestContext.ResultsDirectory", TestContext.ResultsDirectory);
                retVal = addOneFunctionResult(retVal, "TestContext.DeploymentDirectory", TestContext.DeploymentDirectory);
                retVal = addOneFunctionResult(retVal, "TestContext.TestResultsDirectory", TestContext.TestResultsDirectory);
                retVal = addOneFunctionResult(retVal, "TestContext.TestRunDirectory", TestContext.TestRunDirectory);
                retVal = addOneFunctionResult(retVal, "TestContext.TestRunResultsDirectory", TestContext.TestRunResultsDirectory);
            }

            retVal = addOneFunctionResult(retVal, "AppDomain.CurrentDomain.BaseDirectory", AppDomain.CurrentDomain.BaseDirectory);
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
