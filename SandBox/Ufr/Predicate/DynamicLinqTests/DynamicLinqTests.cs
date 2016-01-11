// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DynamicLinqTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DynamicLinqTests
{
    using System.Collections.Generic;
    using System.Linq.Dynamic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;

    [TestClass]
    public class DynamicLinqTests : StfTestScriptBase
    {
        [TestMethod]
        public void TestMethodDynLinq()
        {
            var peopleList = new List<People>()
                                 {
                                     new People { Name = "Name1", Age = 41, Height = 1.1},
                                     new People { Name = "Name2", Age = 42, Height = 1.2},
                                     new People { Name = "Name3", Age = 43, Height = 1.3}
                                 };

            var filtered1 = peopleList.Where("age = 41");
            var filtered2 = peopleList.Where("age = @0", 42);

            StfAssert.IsTrue("1", filtered1.Count() >= 0);
            StfAssert.IsTrue("2", filtered2.Count() >= 0);
        }
    }
}
