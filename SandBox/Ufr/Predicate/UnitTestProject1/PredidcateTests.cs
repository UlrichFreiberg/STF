// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PredidcateTests.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PredidcateTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Mir.Stf;

    using Predicate;

    [TestClass]
    public class PredidcateTests : StfTestScriptBase
    {
        [TestMethod]
        public void TestMethodPredicate()
        {
            var peopleList = new List<People>()
                                 {
                                     new People { Name = "Name1", Age = 41, Height = 1.1},
                                     new People { Name = "Name2", Age = 42, Height = 1.2},
                                     new People { Name = "Name3", Age = 43, Height = 1.3}
                                 };

            var predicate = new Predicate();
            PeopleFilter filter;
            List<People> result;

            filter = new PeopleFilter { AgeFilter = 42 };
            result = predicate.FilterList(peopleList, filter);
            StfAssert.IsTrue("Found", result.Any());
            StfAssert.AreEqual("Found", result.FirstOrDefault().Name, "Name2");

            filter = new PeopleFilter { Name = "Name3" };
            result = predicate.FilterList(peopleList, filter);
            StfAssert.IsTrue("Found", result.Any());
            StfAssert.AreEqual("Found", result.FirstOrDefault().Age, 43);
        }

        [TestMethod]
        public void TestMethodPredicatePart()
        {
            var predicate = new PredicatePart("Age = 42");

            StfAssert.AreEqual("Quantifier", predicate.Quantifier, "Age");
            StfAssert.AreEqual("Value", predicate.Value, "42");
            StfAssert.AreEqual("PredicateExpr", predicate.PredicateExpr, "Age = 42");
        }

        [TestMethod]
        public void TestMethodParsePredicate()
        {
            var predicate = new Predicate();
            var filter = predicate.ParsePredicate<PeopleFilter>("Age = 42");

            StfAssert.AreEqual("Found", filter.AgeFilter, 42);
        }
    }
}
