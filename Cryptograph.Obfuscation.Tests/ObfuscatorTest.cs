using Xunit;
using Cryptography.Obfuscation.Tests.Factory;
using System.Collections.Generic;

namespace Cryptography.Obfuscation.Tests
{
    public class ObfuscatorTest
    {
        [Fact]
        public void EnsureCorrectConversionConstant()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Constant;
            EnsureCorrectConvertionHelper(classUnderTest);
        }

        [Fact]
        public void EnsureCorrectConversionRandomized()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Randomize;
            EnsureCorrectConvertionHelper(classUnderTest);
        }

        [Fact]
        public void EnsureNoCollisionConstant()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Constant;
            EnsureNoCollisionHelper(classUnderTest);
        }

        [Fact]
        public void EnsureNoCollisionRandomized()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Randomize;
            EnsureNoCollisionHelper(classUnderTest);
        }

        // Helper Test Functions:
        private void EnsureCorrectConvertionHelper(Obfuscator classUnderTest)
        {
            for (int i = 0; i < 100000; i++)
            {
                string obfuscatedValue = classUnderTest.Obfuscate(i);
                int deobfuscatedValue = classUnderTest.Deobfuscate(obfuscatedValue);

                if (deobfuscatedValue != i)
                {
                    // Assert fails.
                    Assert.True(false);
                }
            }
        }
        private void EnsureNoCollisionHelper(Obfuscator classUnderTest)
        {
            var obfuscatedResult = new HashSet<string>();
            for (int i = 0; i < 1000000; i++)
            {
                string obfuscatedValue = classUnderTest.Obfuscate(i);

                // HashSet.Add will return false if element already exists. 
                if(!obfuscatedResult.Add(obfuscatedValue))
                {
                    // Assert fails.
                    Assert.True(false);
                }
            }
        }
    }
}
