using Xunit;
using System;
using System.Linq;
using System.Text;
using Cryptography.Obfuscation.Modules;
using Cryptography.Obfuscation.Tests.Factory;

namespace Cryptography.Obfuscation.Tests.Modules
{
    public class ObfuscatorHelperTest
    {
        [Fact]
        public void TestAddDummyCharactersConstant()
        {
            var validCharacters = Settings.ValidCharacterSet;
            var dummyCharacters = Settings.DummyCharacterSet;

            // Generate valid input by taking first 3 characters of validCharacters.
            string validInput = new string(validCharacters.Dictionary.Select(x => x.Value).Take(3).ToArray());

            /* Perform operation under test with constant strategy, */
            var obfuscatorUnderTest = ObfuscatorFactory.NewInstance;
            int seed = obfuscatorUnderTest.Seed;
            var testResult = ObfuscatorHelper.AddDummyCharacters(validInput, ObfuscationStrategy.Constant, seed);

            // Remove all valid characters from result.
            var dummyResult = testResult.ToCharArray().ToList();
            dummyResult.RemoveAll(x => validCharacters.ContainsValue(x));

            // Ensure result only contains dummy characters.
            Assert.True(dummyResult.All(x => dummyCharacters.Contains(x)));
        }

        [Fact]
        public void TestAddDummyCharactersRandomized()
        {
            var validCharacters = Settings.ValidCharacterSet;
            var dummyCharacters = Settings.DummyCharacterSet;

            // Generate valid input by taking first 3 characters of validCharacters.
            string validInput = new string(validCharacters.Dictionary.Select(x => x.Value).Take(3).ToArray());

            /* Perform operation under test with randomized strategy, */
            var obfuscatorUnderTest = ObfuscatorFactory.NewInstance;
            int seed = obfuscatorUnderTest.Seed;
            var testResult = ObfuscatorHelper.AddDummyCharacters(validInput, ObfuscationStrategy.Randomize, seed);

            // Remove all valid characters from result.
            var dummyResult = testResult.ToCharArray().ToList();
            dummyResult.RemoveAll(x => validCharacters.ContainsValue(x));

            // Ensure result only contains dummy characters.
            Assert.True(dummyResult.All(x => dummyCharacters.Contains(x)));
        }

        [Fact]
        public void TestRemoveDummyCharacters()
        {
            var validCharacterSet = Settings.ValidCharacterSet;
            var dummyCharacters = Settings.DummyCharacterSet;

            // Combine input values, which should contain both valid/invalid characters.
            int minLength = Math.Min(dummyCharacters.Length, validCharacterSet.Count);

            var sb = new StringBuilder();
            for (int i = 0; i < minLength; i++) {
                // Append valid/invalid characters in alternating sequence.
                sb.AppendFormat("{0}{1}", validCharacterSet.GetFromKey(i), dummyCharacters[i]);
            }

            string input = sb.ToString();

            // Perform operation under test.
            var obfuscatorUnderTest = ObfuscatorFactory.NewInstance;
            var testResult = ObfuscatorHelper.RemoveDummyCharacters(input);

            // Ensure resulting string doesn't contain any dummy characters.
            Assert.True(testResult.All(x => !dummyCharacters.Contains(x)));
        }
    }
}
