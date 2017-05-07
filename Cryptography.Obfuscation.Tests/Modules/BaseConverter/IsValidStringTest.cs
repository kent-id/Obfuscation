using Cryptography.Obfuscation.Modules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cryptography.Obfuscation.Tests.Modules
{
    [TestClass]
    public class IsValidStringTest
    {
        [TestMethod]
        public void TestOnEmptyInput()
        {
            string sampleInput = string.Empty;
            Assert.IsFalse(BaseConverter.IsValidString(sampleInput));

            sampleInput = null;
            Assert.IsFalse(BaseConverter.IsValidString(sampleInput));
        }

        [TestMethod]
        public void TestOnOnlyDummyCharacter()
        {
            var dummyKeys = Settings.DummyCharacterSet;

            string sampleInput = string.Empty;
            for (int i = 0; i < dummyKeys.Length; i++)
            {
                sampleInput = dummyKeys[i].ToString();
                Assert.IsFalse(BaseConverter.IsValidString(sampleInput));
            }
        }

        [TestMethod]
        public void TestOnOnlyValidCharacter()
        {
            var validKeys = Settings.ValidCharacterSet;

            string sampleInput = string.Empty;
            for (int i = 0; i < validKeys.Count; i++)
            {
                sampleInput = validKeys.GetFromKey(i).ToString();
                Assert.IsTrue(BaseConverter.IsValidString(sampleInput));
            }
        }
    }
}
