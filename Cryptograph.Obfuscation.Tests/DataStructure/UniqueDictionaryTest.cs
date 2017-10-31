using System;
using Xunit;
using Cryptography.Obfuscation.Tests.Factory;

namespace Cryptography.Obfuscation.Tests.DataStructure
{
    public class UniqueDictionaryTest
    {
        [Fact]
        public void TestNoDuplicateKey()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            Assert.Throws<ArgumentException>(() => classUnderTest.Add(1, 'b'));
        }

        [Fact]
        public void TestNoDuplicateValue()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            Assert.Throws<ArgumentException>(() => classUnderTest.Add(2, 'a'));
        }

        [Fact]
        public void TestGetFromKey()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            classUnderTest.Add(2, 'b');

            char firstValue = classUnderTest.GetFromKey(1);
            Assert.Equal(firstValue, 'a');
        }

        [Fact]
        public void TestGetFromValue()
        {
            var classUnderTest = UniqueDictionaryFactory.NewInstance;

            classUnderTest.Add(1, 'a');
            classUnderTest.Add(2, 'b');
            
            int secondKey = classUnderTest.GetFromValue('b');
            Assert.Equal(secondKey, 2);
        }
    }
}
