using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    using System.Collections.Generic;
    using System.Linq;

    using Mir.Stf;

    using Predicate;

    public class People
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public double? Height { get; set; }
    }

    public class PeopleFilter : People
    {
        [PredicateMap("Age")]
        public int? AgeFilter { get; set; }
    }

    [TestClass]
    public class UnitTest1 : StfTestScriptBase
    {
        [TestMethod]
        public void TestMethodPredicateFinal()
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

        //[TestMethod]
        //public void TestMethodDynLinq()
        //{
        //    var peopleList = new List<People>()
        //                         {
        //                             new People { Name = "Name1", Age = 41, Height = 1.1},
        //                             new People { Name = "Name2", Age = 42, Height = 1.2},
        //                             new People { Name = "Name3", Age = 43, Height = 1.3}
        //                         };

        //    var filtered1 = peopleList.Where("age = 41");
        //    var filtered2 = peopleList.Where("age = @0", 42);

        //    StfAssert.IsTrue("1", filtered1.Count() >= 0);
        //    StfAssert.IsTrue("2", filtered2.Count() >= 0);
        //}
    }
}
