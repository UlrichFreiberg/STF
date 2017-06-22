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

    using Mir.Stf.Utilities.Exceptions;

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
            StfAssert.IsNotNull("Get<IStfUnitTestPlugin1>", sp1);
            StfAssert.AreEqual("sp1.StfUnitTestPlugin1Func", 101, sp1.StfUnitTestPlugin1Func());

            var sp2 = Get<IStfUnitTestPlugin2>();
            StfAssert.IsNotNull("Get<IStfUnitTestPlugin2>", sp2);
            StfAssert.AreEqual("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());
        }

        /// <summary>
        /// The test method plugin settings overlayed.
        /// </summary>
        [TestMethod]
        public void TestMethodPluginSettingsOverlayed()
        {
            var configuration = Get<StfConfiguration>();
            StfAssert.AreEqual("DefaulEnvironment == TESTENVIRONMENT1", "TESTENVIRONMENT1", configuration.DefaultEnvironment);
            
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            StfAssert.AreEqual("DefaulEnvironment == ENV2", "ENV2", configuration.DefaultEnvironment);
            
            var env2Keyvalue = configuration.GetConfigValue("UnitTestPlugin1.UnitTestPlugin1Key");
            StfAssert.IsNotNull("Key not null", env2Keyvalue);
            StfAssert.AreEqual("ENV2 keyvalue", "ENV2_Value", env2Keyvalue);
        }

        /// <summary>
        /// The test method plugin 2 settings overlayed.
        /// </summary>
        [TestMethod]
        public void TestMethodPlugin2SettingsOverlayed()
        {
            var configuration = Get<StfConfiguration>();
            StfAssert.AreEqual("DefaulEnvironment == TESTENVIRONMENT1", "TESTENVIRONMENT1", configuration.DefaultEnvironment);

            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            StfAssert.AreEqual("DefaulEnvironment == ENV2", "ENV2", configuration.DefaultEnvironment);

            var env2Keyvalue = configuration.GetConfigValue("UnitTestPlugin2.UnitTestPlugin2Key");
            StfAssert.IsNotNull("Key not null", env2Keyvalue);
            StfAssert.AreEqual("ENV2 keyvalue", "ENV2_Value", env2Keyvalue);
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
            StfAssert.IsNotNull("Get<IStfUnitTestPlugin2>", sp2);
            StfAssert.AreEqual("sp2.StfUnitTestPlugin2Func", 102, sp2.StfUnitTestPlugin2Func());

            var plugin2Type = Get<ITestPluginModel>();
            StfAssert.IsNotNull("Get<ITestPluginModel>", plugin2Type);
            StfAssert.AreEqual("plugin2Type.TestPluginFunc", 202, plugin2Type.TestPluginFunc());

            var plugin2Type2 = Get<ITestPluginModel2>();
            StfAssert.IsNotNull("Get<ITestPluginModel>", plugin2Type2);
            StfAssert.AreEqual("plugin2Type.TestPluginFunc", "2+2=4", plugin2Type2.TestPlugin2FuncWithParams("2+2", 4));

            StfAssert.AreEqual("plugin2Type.TestProp", "Default", plugin2Type2.TestProp);
            plugin2Type2.TestProp = "NewValue";
            StfAssert.AreEqual("plugin2Type.TestProp", "NewValue", plugin2Type2.TestProp);

            StfAssert.AreEqual("Plugin2Func", 203, plugin2Type2.TestPlugin2Func());
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
            StfAssert.IsNotNull("Not null", pluginModel);
        }

        /// <summary>
        /// The test method unknown type throws exception.
        /// </summary>
        [TestMethod]
        public void TestMethodUnknownTypeThrowsException()
        {
            StfAssert.AssertThrows<StfTypeResolutionException>("Resolve unknown for STF type", () => Get<StfTypeResolutionException>());
            StfAssert.AssertThrows<StfTypeResolutionException>("Resolve unknown for STF type", () => Get<int>());
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
            StfAssert.IsNotNull("sp2.StfContainer != null", sp2.StfContainer);

            var pluginModel = sp2.StfContainer.Get<ITestPluginModel>();
            StfAssert.IsNotNull("sp1.IsInitialized", pluginModel);

            var pluginModel2 = sp2.StfContainer.Get<ITestPluginModel2>();
            StfAssert.IsNotNull("sp1.IsInitialized", pluginModel2);

            var pluginAdapter = sp2.StfContainer.Get<ITestPluginModel2>();
            StfAssert.IsNotNull("sp1.IsInitialized", pluginAdapter);

            var pluginTypeWithoutInterface = sp2.StfContainer.Get<TestPluginTypeWithoutInterface>();
            StfAssert.IsNotNull("sp1.IsInitialized", pluginTypeWithoutInterface);

            var testAdapterWithoutInterface = sp2.StfContainer.Get<TestAdapterWithoutInterface>();
            StfAssert.IsNotNull("sp1.IsInitialized", testAdapterWithoutInterface);
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
            StfAssert.IsNotNull("sp2.StfContainer != null", sp2.StfContainer);
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
            StfAssert.IsNotNull("sp2.StfLogger != null", sp2.StfLogger);
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
            StfAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestPluginTypeWithoutInterface>();
            StfAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            StfAssert.IsNotNull("typeWithoutInterface.StfContainer != null", typeWithoutInterface.StfContainer);
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
            StfAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestPluginTypeWithoutInterface>();
            StfAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            StfAssert.IsNotNull("typeWithoutInterface.StfLogger != null", typeWithoutInterface.StfLogger);
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
            StfAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestAdapterWithoutInterface>();
            StfAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            StfAssert.IsNotNull("typeWithoutInterface.StfContainer != null", typeWithoutInterface.StfContainer);
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
            StfAssert.IsNotNull("sp2 != null", sp2);

            var typeWithoutInterface = sp2.StfContainer.Get<TestAdapterWithoutInterface>();
            StfAssert.IsNotNull("typeWithoutInterface != null", typeWithoutInterface);
            StfAssert.IsNotNull("typeWithoutInterface.StfLogger != null", typeWithoutInterface.StfLogger);
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
            StfAssert.IsNotNull("sp2 != null", sp2);

            var testPluginModel = Get<ITestPluginModel>();
            StfAssert.IsNotNull("testPluginModel != null", testPluginModel);
            StfAssert.IsTrue("Use ModelBase internally", testPluginModel.CanUseModelBaseInternally());
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
            StfAssert.IsNotNull("sp2 != null", sp2);

            var testPluginModel = Get<ITestPluginAdapter>();
            StfAssert.IsNotNull("testPluginModel != null", testPluginModel);
            StfAssert.IsTrue("Use ModelBase internally", testPluginModel.CanUseAdapterBaseInternally());
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

            StfAssert.IsNotNull(
                "Has interface",
                typeof(StfUnitTestPlugin2).GetInterface(typeof(IStfUnitTestPlugin2).Name));

            StfAssert.AssertThrows<InvalidCastException>(
                "Unit test plugin is a proxy object", 
                // ReSharper disable once UnusedVariable
                () => { var stfUnitTestPlugin2 = (StfUnitTestPlugin2)sp2; });
        }

        /// <summary>
        /// The test stf plugin loader registers singleton correctly.
        /// </summary>
        [TestMethod]
        public void TestStfPluginLoaderRegistersSingletonCorrectly()
        {
            LoadAdditionalStfPlugins(".", "Stf.UnitTestPlugin*.dll");

            var sp2 = Get<IStfUnitTestPlugin2>();
            StfAssert.IsNotNull("sp2 != null", sp2);

            var pluginObject1 = Get<ITestPluginModel2>();
            pluginObject1.TestProp = "Changed";

            var pluginObject2 = Get<ITestPluginModel2>();

            StfAssert.IsFalse("TestpluginModel is not a singleton", pluginObject1.Equals(pluginObject2));
            StfAssert.StringEquals("Object with default value", "Default", pluginObject2.TestProp);
            StfAssert.StringEquals("Object with changed value", "Changed", pluginObject1.TestProp);

            var singletonObject = Get<IStfSingletonPluginType>();
            
            StfAssert.IsFalse("Singleton bool is false", singletonObject.SingletonBool);
            StfAssert.AreEqual("SingletonInteger is 1", 1, singletonObject.SingletonInteger);

            singletonObject.SingletonBool = true;
            singletonObject.SingletonInteger++;

            var singletonObject2 = Get<IStfSingletonPluginType>();

            StfAssert.IsTrue("Singleton object is a singleton", singletonObject2.Equals(singletonObject));
            StfAssert.IsTrue("Singleton is true", singletonObject2.SingletonBool);
            StfAssert.AreEqual("SingletonInteger is 2", 2, singletonObject2.SingletonInteger);
        }
    }
}
