using Xunit;
using Cryptography.Obfuscation.Modules;

namespace Cryptography.Obfuscation.Tests.Modules
{
    public class IsValidSequenceTest
    {
        [Fact]
        public void TestOnEmptyInput()
        {
            string sampleInput = string.Empty;
            Assert.False(BaseConverter.IsValidSequence(sampleInput));

            sampleInput = null;
            Assert.False(BaseConverter.IsValidSequence(sampleInput));
        }

        [Fact]
        public void TestOnOnlyDummyCharacter()
        {
            var dummyKeys = Settings.DummyCharacterSet;

            string sampleInput = string.Empty;
            for (int i = 0; i < dummyKeys.Length; i++)
            {
                sampleInput = dummyKeys[i].ToString();
                Assert.False(BaseConverter.IsValidSequence(sampleInput));
            }
        }

        [Fact]
        public void TestOnOnlyValidCharacter()
        {
            var validKeys = Settings.ValidCharacterSet;

            string sampleInput = string.Empty;
            for (int i = 0; i < validKeys.Count; i++)
            {
                sampleInput = validKeys.GetFromKey(i).ToString();
                Assert.True(BaseConverter.IsValidSequence(sampleInput));
            }
        }
    }
}
