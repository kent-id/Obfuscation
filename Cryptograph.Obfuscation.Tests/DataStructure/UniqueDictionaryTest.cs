using System;
using Xunit;
using Cryptography.Obfuscation.Tests.Factory;

namespace Cryptography.Obfuscation.Tests.DataStructure
{
    public class UniqueDictionaryTest
    {
        [Fact(DisplayName = "Duplicate key should not be allowed in unique dictionary")]
        public void DuplicateKeyShouldThrowException()
        {
            var dictionary = UniqueDictionaryFactory.NewInstance;

            dictionary.Add(1, 'a');
            Assert.Throws<ArgumentException>(() => dictionary.Add(1, 'b'));
        }

        [Fact(DisplayName = "Duplicate value should not be allowed in unique dictionary")]
        public void DuplicateValueSHouldThrowExeption()
        {
            var dictionary = UniqueDictionaryFactory.NewInstance;

            dictionary.Add(1, 'a');
            Assert.Throws<ArgumentException>(() => dictionary.Add(2, 'a'));
        }

        [Fact(DisplayName = "Test for getting value from key")]
        public void TestGetFromKey()
        {
            var dictionary = UniqueDictionaryFactory.NewInstance;

            dictionary.Add(1, 'a');
            dictionary.Add(2, 'b');

            char firstValue = dictionary.GetFromKey(1);
            Assert.Equal('a', firstValue);
        }

        [Fact(DisplayName = "Test for getting key from value")]
        public void TestGetFromValue()
        {
            var dictionary = UniqueDictionaryFactory.NewInstance;

            dictionary.Add(1, 'a');
            dictionary.Add(2, 'b');
            
            int secondKey = dictionary.GetFromValue('b');
            Assert.Equal(2, secondKey);
        }
    }
}
