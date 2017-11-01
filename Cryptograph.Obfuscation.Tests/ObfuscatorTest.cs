using System;
using System.Collections.Generic;
using Xunit;
using Cryptography.Obfuscation.Tests.Factory;

namespace Cryptography.Obfuscation.Tests
{
    public class ObfuscatorTest
    {
        [Theory(DisplayName = "Ensure obfuscation is deobfuscated correctly across a big range of number")]
        [InlineData(ObfuscationStrategy.Constant)]
        [InlineData(ObfuscationStrategy.Randomize)]
        public void EnsureCorrectConversion(ObfuscationStrategy strategy)
        {
            var obfuscator = ObfuscatorFactory.NewInstance;
            obfuscator.Strategy = strategy;

            for (int i = 0; i < 100000; i++)
            {
                string obfuscatedValue = obfuscator.Obfuscate(i);
                int deobfuscatedValue = obfuscator.Deobfuscate(obfuscatedValue);

                if (deobfuscatedValue != i)
                {
                    // Assert fails.
                    Assert.True(false);
                }
            }
        }

        [Fact(DisplayName = "Ensure correct conversion from different obfuscator object with same seed")]
        public void EnsureCorrectConversionFromDifferentObfuscatorObjectWithSameSeed()
        {
            // Generate any valid number / seed.
            int numberToObfuscate = new Random().Next(0, int.MaxValue);
            int seed = new Random().Next(2, int.MaxValue);

            var obfuscator = ObfuscatorFactory.NewInstance;
            obfuscator.Seed = seed;
            string obfuscatedString = obfuscator.Obfuscate(numberToObfuscate);

            var obfuscator2 = ObfuscatorFactory.NewInstance;
            obfuscator2.Seed = seed;
            int number = obfuscator2.Deobfuscate(obfuscatedString);

            Assert.Equal(numberToObfuscate, number);
        }

        [Fact(DisplayName = "Conversion with constant strategy and same seed should never change")]
        public void ConversionWithConstantStrategyAndSameSeedShouldNeverChange()
        {
            // Generate any valid number.
            int numberToObfuscate = new Random().Next(0, int.MaxValue);

            var obfuscator = ObfuscatorFactory.NewInstance;
            string firstObfuscatedString = obfuscator.Obfuscate(numberToObfuscate);
            for (int i = 0; i <= 10; i++)
            {
                string obfuscatedString = obfuscator.Obfuscate(numberToObfuscate);
                Assert.Equal(firstObfuscatedString, obfuscatedString);
            }
        }

        [Fact(DisplayName = "Conversion with constant strategy and different seed should be different")]
        public void ConversionWithConstantStrategyAndDifferentSeedShouldBeDifferent()
        {
            // Generate any valid number.
            int numberToObfuscate = new Random().Next(0, int.MaxValue);
            
            var obfuscator = ObfuscatorFactory.NewInstance;
            obfuscator.Seed = 7;
            string obfuscatedString1 = obfuscator.Obfuscate(numberToObfuscate);

            var obfuscator2 = ObfuscatorFactory.NewInstance;
            obfuscator2.Seed = 131;
            string obfuscatedString2 = obfuscator2.Obfuscate(numberToObfuscate);

            Assert.NotEqual(obfuscatedString1, obfuscatedString2);
        }

        [Fact(DisplayName = "Conversion with randomized mode should be different")]
        public void ConversionWithRandomizedModeShouldBeDifferent()
        {
            // Generate any valid number.
            int numberToObfuscate = new Random().Next(0, int.MaxValue);

            var obfuscator = ObfuscatorFactory.NewInstance;
            obfuscator.Strategy = ObfuscationStrategy.Randomize;
            string obfuscatedString1 = obfuscator.Obfuscate(numberToObfuscate);
            string obfuscatedString2 = obfuscator.Obfuscate(numberToObfuscate);

            Assert.NotEqual(obfuscatedString1, obfuscatedString2);
        }
        
        [Theory(DisplayName = "Ensure obfuscation has no collision across a big range of number")]
        [InlineData(ObfuscationStrategy.Constant)]
        [InlineData(ObfuscationStrategy.Randomize)]
        public void EnsureNoCollision(ObfuscationStrategy strategy)
        {
            var obfuscator = ObfuscatorFactory.NewInstance;
            obfuscator.Strategy = strategy;

            var hashSet = new HashSet<string>();
            for (int i = 0; i < 1000000; i++)
            {
                string obfuscatedValue = obfuscator.Obfuscate(i);

                // HashSet.Add will return false if element already exists. 
                if (!hashSet.Add(obfuscatedValue))
                {
                    // Assert fails.
                    Assert.True(false);
                }
            }
        }

        [Fact(DisplayName = "Seed value has to be at least two")]
        public void SeedValueHasToBeAtLeastTwo()
        {
            var obfuscator = ObfuscatorFactory.NewInstance;
            Assert.Throws<InvalidOperationException>(() => obfuscator.Seed = -1);
            Assert.Throws<InvalidOperationException>(() => obfuscator.Seed = 1);

            // Should not throw.
            obfuscator.Seed = 2;
        }

        [Fact(DisplayName = "Negative value should not be supported")]
        public void NegativeValueShouldNotBeSupported()
        {
            var obfuscator = ObfuscatorFactory.NewInstance;
            Assert.Throws<InvalidOperationException>(() => obfuscator.Obfuscate(-1));

            // Should not throw.
            obfuscator.Obfuscate(0);
        }
    }
}
