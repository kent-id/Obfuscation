using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cryptography.Obfuscation.Tests.Factory;
using System.Collections.Generic;

namespace Cryptography.Obfuscation.Tests
{
    [TestClass]
    public class ObfuscatorTest
    {
        [TestMethod]
        public void EnsureCorrectConversionConstant()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Constant;
            EnsureCorrectConvertionHelper(classUnderTest);
        }

        [TestMethod]
        public void EnsureCorrectConversionRandomized()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Randomize;
            EnsureCorrectConvertionHelper(classUnderTest);
        }

        [TestMethod]
        public void EnsureNoCollisionConstant()
        {
            var classUnderTest = ObfuscatorFactory.NewInstance;
            classUnderTest.Strategy = ObfuscationStrategy.Constant;
            EnsureNoCollisionHelper(classUnderTest);
        }

        [TestMethod]
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
                    Assert.Fail();
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
                    Assert.Fail();
                }
            }
        }
    }
}
