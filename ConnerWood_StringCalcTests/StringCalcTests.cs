using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnerWood_StringCalculator.UnitTests
{
    [TestClass()]
    public class StringCalculatorTests
    {
        [TestMethod()]
        public void Add_CommaSeperatedString_Empty_ReturnsZero()
        {
            StringCalculator sc = new StringCalculator();

            //Test empty string
            int nResult = sc.Add("");

            //Assert it is 0
            Assert.AreEqual(nResult, 0);
        }

        [TestMethod()]
        public void Add_CommaSeperatedString_NULL_ReturnsZero()
        {
            StringCalculator sc = new StringCalculator();

            //Test null string
            int nResult = sc.Add(null);

            //Assert it is 0
            Assert.AreEqual(nResult, 0);
        }

        [TestMethod()]
        public void Add_CommaSeperatedString_SingleNum_ReturnsSum()
        {
            StringCalculator sc = new StringCalculator();

            //Test single number string
            int nResult = sc.Add("2");

            //Assert it is 2
            Assert.AreEqual(nResult, 2);
        }

        [TestMethod()]
        public void Add_CommaSeperatedString_125_ReturnsEight()
        {
            StringCalculator sc = new StringCalculator();

            //Test single number string
            int nResult = sc.Add("1,2,5");

            //Assert it is 8
            Assert.AreEqual(nResult, 8);
        }

        [TestMethod()]
        public void Add_HandleNewLines_BeforeComma()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            //Test handle new line before comma string
            nResult = sc.Add("1\n,2,3");

            //Assert it is 6
            Assert.AreEqual(nResult, 6);
        }

        [TestMethod()]
        public void Add_HandleNewLines_AfterComma()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            //Test handle new line after comma string
            nResult = sc.Add("1,\n2,4");
            //Assert it is 7
            Assert.AreEqual(nResult, 7);
        }

        [TestMethod()]
        public void Add_TestCustomDelimiter_Semicolon()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            nResult = sc.Add("//;\n1;3;4");
            Assert.AreEqual(nResult, 8);
        }

        [TestMethod()]
        public void Add_TestCustomDelimiter_DollarSign()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            nResult = sc.Add("//$\n1$2$3");
            Assert.AreEqual(nResult, 6);
        }

        [TestMethod()]
        public void Add_TestCustomDelimiter_AtSign()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            nResult = sc.Add("//;\n1;3;4");
            Assert.AreEqual(nResult, 8);
        }


        [TestMethod()]
        public void Add_TestNegativeNumbers_ThrowException()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            Assert.ThrowsException<Exception>(() => nResult = sc.Add("//;\n6;99;-5;22;-4;4;14;-13;3"));
        }

        [TestMethod()]
        public void Add_TestIgnoreNumber_IgnoredLargeNumber()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            nResult = sc.Add("2,1001");
            Assert.AreEqual(nResult, 2);
        }

        [TestMethod()]
        public void Add_TestCustomDelimiter_ArbitraryDelimiterLength()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            nResult = sc.Add("//***\n1***2***3");
            Assert.AreEqual(nResult, 6);
        }

        [TestMethod()]
        public void Add_TestCustomDelimiter_MultipleCustomDelimiters()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            nResult = sc.Add("//$,@\n1$2@3");
            Assert.AreEqual(nResult, 6);
        }

        [TestMethod()]
        public void Add_TestCustomDelimiter_MultipleCustomDelimitersOfArbitraryLength()
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();

            //Note, the test did not require delimiters that might conflict with others that use part of the same characters.
            //This test would fail with the following:
            //      nResult = sc.Add("//**,***\n1**2***3");
            //      The error tells us "*3" is not able to be parsed to int as it was used by a conflicting delimiter
            //      Potentially regex could solve this problem or re-ordering delimiters to go from biggest to smallest 
            //      to avoid this particular issue of a smaller delimiter being able to mess with a bigger delimiter.

            nResult = sc.Add("//***,@@,%%\n1***2***3@@44%%25***25");
            Assert.AreEqual(nResult, 100);
        }


    }
}