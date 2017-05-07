using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography.Obfuscation.Tests.Factory;

namespace Cryptography.Obfuscation.Tests.DataStructure
{
    [TestClass]
    public class UniqueDictionaryTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNoDuplicateKey()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            classUnderTest.Add(1, 'b');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNoDuplicateValue()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            classUnderTest.Add(2, 'a');
        }

        [TestMethod]
        public void TestGetFromKey()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            classUnderTest.Add(2, 'b');

            char firstValue = classUnderTest.GetFromKey(1);
            Assert.AreEqual(firstValue, 'a');
        }

        [TestMethod]
        public void TestGetFromValue()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            classUnderTest.Add(2, 'b');
            
            int secondKey = classUnderTest.GetFromValue('b');
            Assert.AreEqual(secondKey, 2);
        }
    }
}
