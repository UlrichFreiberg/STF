// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestStfPluginLoader.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Mir.Stf;
using Mir.Stf.Utilities;
using Stf.Unittests.UnitTestPluginTypes;

namespace UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Stf.Unittests;

    /// <summary>
    /// The unit test stf asserts.
    /// </summary>
    [TestClass]
    public class UnitTestStfPluginLoader : StfTestScriptBase
    {
        /// <summary>
        /// The test method plugin test get.
        /// </summary>
        [TestMethod]
        public void TestMethodPluginTestGet()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp1 = Get<IStfUnitTestPlugin1>();
            MyAssert.IsNotNull("Get<IStfUnitTestPlugin1>", sp1);
            MyAssert.AreEqual("sp1.StfUnitTestPlugin1Func", 101, sp1.StfUnitTestPlugin1Func());

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("Get<IStfUnitTestPlugin2>", sp2);
            MyAssert.AreEqual("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());
        }

        /// <summary>
        /// The test method plugin settings overlayed.
        /// </summary>
        [TestMethod]
        public void TestMethodPluginSettingsOverlayed()
        {
            var configuration = Get<StfConfiguration>();
            MyAssert.AreEqual("DefaulEnvironment == ENV1", "ENV1", configuration.DefaultEnvironment);
            
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            MyAssert.AreEqual("DefaulEnvironment == ENV2", "ENV2", configuration.DefaultEnvironment);
            
            var env2Keyvalue = configuration.GetConfigValue("UnitTestPlugin1.UnitTestPlugin1Key");
            MyAssert.IsNotNull("Key not null", env2Keyvalue);
            MyAssert.AreEqual("ENV2 keyvalue", "ENV2_Value", env2Keyvalue);
        }

        /// <summary>
        /// The test method get plugin type test.
        /// </summary>
        [TestMethod]
        public void TestMethodGetPluginTypeTest()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("Get<IStfUnitTestPlugin2>", sp2);
            MyAssert.AreEqual("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());

            var plugin2Type = Get<ITestPluginModel>();
            MyAssert.IsNotNull("Get<ITestPluginModel>", plugin2Type);
            MyAssert.AreEqual("plugin2Type.TestPluginFunc", 202, plugin2Type.TestPluginFunc());

            var plugin2Type2 = Get<ITestPluginModel2>();
            MyAssert.IsNotNull("Get<ITestPluginModel>", plugin2Type2);
            MyAssert.AreEqual("plugin2Type.TestPluginFunc", "2+2=4", plugin2Type2.TestPlugin2FuncWithParams("2+2", 4));

            MyAssert.AreEqual("plugin2Type.TestProp", "Default", plugin2Type2.TestProp);
            plugin2Type2.TestProp = "NewValue";
            MyAssert.AreEqual("plugin2Type.TestProp", "NewValue", plugin2Type2.TestProp);
        }

        /// <summary>
        /// The test method getter on stf container.
        /// </summary>
        [TestMethod]
        public void TestMethodGetterOnStfContainer()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            var pluginModel = sp2.StfContainer.Get<ITestPluginModel>();
            MyAssert.IsNotNull("Not null", pluginModel);
        }

        /// <summary>
        /// The test method container extension initializes plugin.
        /// </summary>
        [TestMethod]
        public void TestMethodContainerExtensionInitializesPlugin()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2.StfContainer != null", sp2.StfContainer);

            var pluginModel = sp2.StfContainer.Get<ITestPluginModel>();
            MyAssert.IsNotNull("sp1.IsInitialized", pluginModel);

            var pluginModel2 = sp2.StfContainer.Get<ITestPluginModel2>();
            MyAssert.IsNotNull("sp1.IsInitialized", pluginModel2);

            var pluginAdapter = sp2.StfContainer.Get<ITestPluginModel2>();
            MyAssert.IsNotNull("sp1.IsInitialized", pluginAdapter);

            var pluginTypeWithoutInterface = sp2.StfContainer.Get<TestPluginTypeWithoutInterface>();
            MyAssert.IsNotNull("sp1.IsInitialized", pluginTypeWithoutInterface);

            var testAdapterWithoutInterface = sp2.StfContainer.Get<TestAdapterWithoutInterface>();
            MyAssert.IsNotNull("sp1.IsInitialized", testAdapterWithoutInterface);
        }

        /// <summary>
        /// The test method stf container is injected.
        /// </summary>
        [TestMethod]
        public void TestMethodStfContainerIsInjected()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2.StfContainer != null", sp2.StfContainer);
        }

        /// <summary>
        /// The test method stf logger is injected.
        /// </summary>
        [TestMethod]
        public void TestMethodStfLoggerIsInjected()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");
            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2.MyLogger != null", sp2.MyLogger);
        }

        /// <summary>
        /// The test method stf container is injected on model base.
        /// </summary>
        [TestMethod]
        public void TestMethodStfContainerIsInjectedOnModelBase()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestPluginTypeWithoutInterface>();
            MyAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            MyAssert.IsNotNull("typeWithoutInterface.StfContainer != null", typeWithoutInterface.StfContainer);
        }

        /// <summary>
        /// The test method stf logger is injected on model base.
        /// </summary>
        [TestMethod]
        public void TestMethodStfLoggerIsInjectedOnModelBase()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestPluginTypeWithoutInterface>();
            MyAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            MyAssert.IsNotNull("typeWithoutInterface.MyLogger != null", typeWithoutInterface.MyLogger);
        }

        /// <summary>
        /// The test method stf container is injected on adapter base.
        /// </summary>
        [TestMethod]
        public void TestMethodStfContainerIsInjectedOnAdapterBase()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestAdapterWithoutInterface>();
            MyAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            MyAssert.IsNotNull("typeWithoutInterface.StfContainer != null", typeWithoutInterface.StfContainer);
        }

        /// <summary>
        /// The test method stf logger is injected on adapter base.
        /// </summary>
        [TestMethod]
        public void TestMethodStfLoggerIsInjectedOnAdapterBase()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestAdapterWithoutInterface>();
            MyAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            MyAssert.IsNotNull("typeWithoutInterface.MyLogger != null", typeWithoutInterface.MyLogger);
        }

        /// <summary>
        /// The test method stf logger is injected on adapter base.
        /// </summary>
        [TestMethod]
        public void TestMethodModelBaseCanBeUsed()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2 != null", sp2);

            var testPluginModel = Get<ITestPluginModel>();
            MyAssert.IsNotNull("testPluginModel != null", testPluginModel);
            MyAssert.IsTrue("Use ModelBase internally", testPluginModel.CanUseModelBaseInternally());
        }

        /// <summary>
        /// The test method stf logger is injected on adapter base.
        /// </summary>
        [TestMethod]
        public void TestMethodAdapterCanBeUsed()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            MyAssert.IsNotNull("sp2 != null", sp2);

            var testPluginModel = Get<ITestPluginAdapter>();
            MyAssert.IsNotNull("testPluginModel != null", testPluginModel);
            MyAssert.IsTrue("Use ModelBase internally", testPluginModel.CanUseAdapterBaseInternally());
        }

        /// <summary>
        /// The test method proxy object created for plugin with matching interface.
        /// </summary>
        [TestMethod]
        public void TestMethodProxyObjectCreatedForPluginWithMatchingInterface()
        {
            // need to load the UnitTest plug-ins
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();

            MyAssert.IsNotNull(
                "Has interface",
                typeof(StfUnitTestPlugin2).GetInterface(typeof(IStfUnitTestPlugin2).Name));

            MyAssert.AssertThrows<InvalidCastException>(
                "Unit test plugin is a proxy object", 
                // ReSharper disable once UnusedVariable
                () => { var stfUnitTestPlugin2 = (StfUnitTestPlugin2)sp2; });
        }
    }
}
