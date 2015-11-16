using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StatePrinterDemo
{
    using LogTestOutputValues;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var testObj1 = new { Col = 10, Name = "NameOfIt1", Row = 15 };
            var testObj2 = new Test1 { Col = 20, Name = "NameOfIt2", Row = 25 };
            var logger = new LoggerForTestOutputValues
            {
                LogFilename = @"c:\temp\stf\temp\LoggerForTestOutputValues.csv",
                FieldSeperator = ","
            };


            logger.LogTestOutputValues(testObj1);
            logger.LogTestOutputValues(testObj2);
        }
    }


    class Test1
    {
        public int Row;

        public int Col;

        public string Name;
    }
}
