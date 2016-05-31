// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestPredicateUtils.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UnitTest.PredicateUtils
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mir.Stf;
    using Mir.Stf.Utilities.PredicateUtilities;

    /// <summary>
    /// UnitTests for PredicateUtils 
    /// </summary>
    [TestClass]
    public class UnitTestPredicateUtils
    {
        /// <summary>
        /// Test Predicate
        /// </summary>
        [TestMethod]
        public void TestMethodPredicate()
        {
            var peopleList = new List<People>()
                                 {
                                     new People { Name = "Name1", Age = 41, Height = 1.1 },
                                     new People { Name = "Name2", Age = 42, Height = 1.2 },
                                     new People { Name = "Name3", Age = 43, Height = 1.3 }
                                 };

            var predicate = new PredicateUtils();
            var filter = new PeopleFilter { AgeFilter = 42 };
            var result = predicate.FilterList(peopleList, filter);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.First().Name, "Name2");

            filter = new PeopleFilter { Name = "Name3" };
            result = predicate.FilterList(peopleList, filter);
            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.First().Age, 43);
        }

        /// <summary>
        /// Test PredicateExpression
        /// </summary>
        [TestMethod]
        public void TestMethodPredicateExpression()
        {
            var predicate = new PredicateExpression("Age = 42");

            Assert.AreEqual(predicate.LeftHandSide, "Age");
            Assert.AreEqual(predicate.RightHandSide, "42");
            Assert.AreEqual(predicate.PredicateExpr, "Age = 42");
        }

        /// <summary>
        /// Test Method ParsePredicate
        /// </summary>
        [TestMethod]
        public void TestMethodParsePredicate()
        {
            var predicateUtils = new PredicateUtils();
            var filter = predicateUtils.ParsePredicate<PeopleFilter>("Age = 42");

            Assert.AreEqual(filter.AgeFilter, 42);
        }

        /// <summary>
        /// Test Method GeneratePredicate
        /// </summary>
        [TestMethod]
        public void TestMethodGeneratePredicate1()
        {
            var predicate = new PredicateUtils();
            var filterClass = new PeopleFilter { Height = 182 };
            var filter = predicate.GeneratePredicate(filterClass);

            Assert.AreEqual(filter, "Height=182");
        }

        /// <summary>
        /// Test Method GeneratePredicate
        /// </summary>
        [TestMethod]
        public void TestMethodGeneratePredicate2()
        {
            var predicate = new PredicateUtils();
            var filterClass = new PeopleFilter { Height = 182, Age = 42 };
            var filter = predicate.GeneratePredicate(filterClass);

            Assert.AreEqual(filter, "Height=182");
        }

        /// <summary>
        /// Test Method GeneratePredicate
        /// </summary>
        [TestMethod]
        public void TestMethodGeneratePredicate3()
        {
            var predicate = new PredicateUtils();
            var filterClass = new PeopleFilter { Height = 182, AgeFilter = 42 };
            var filter = predicate.GeneratePredicate(filterClass);

            Assert.AreEqual(filter, "Age=42 ; Height=182");
        }
    }
}
