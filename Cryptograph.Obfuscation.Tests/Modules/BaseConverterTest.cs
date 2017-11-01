using System.Text;
using Xunit;
using Cryptography.Obfuscation.Modules;

namespace Cryptography.Obfuscation.Tests.Modules
{
    public class BaseConverterTest
    {
        [Fact(DisplayName ="Converting invalid sequence should return -1")]
        public void ConvertingInvalidSequenceShouldReturnMinusOne()
        {
            string invalidSequence = string.Empty;
            int converted = BaseConverter.ConvertFromBase(invalidSequence);
            Assert.Equal(-1, converted);
        }

        [Fact(DisplayName = "Empty sequence should be invalid")]
        public void EmptySequenceShouldBeInvalid()
        {
            string sequence = string.Empty;
            Assert.False(BaseConverter.IsValidSequence(sequence));

            sequence = null;
            Assert.False(BaseConverter.IsValidSequence(sequence));
        }

        [Fact(DisplayName = "Sequence made from only dummy characters should be invalid")]
        public void SequenceWithOnlyDummyCharactersShouldBeInvalid()
        {
            var dummyKeys = Settings.DummyCharacterSet;

            string sequence = string.Empty;
            for (int i = 0; i < dummyKeys.Length; i++)
            {
                sequence = dummyKeys[i].ToString();
                Assert.False(BaseConverter.IsValidSequence(sequence));
            }
        }

        [Fact(DisplayName = "Sequence made from only valid characters should be valid")]
        public void SequenceWithOnlyValidharactersShouldBeValid()
        {
            var validKeys = Settings.ValidCharacterSet;

            string sequence = string.Empty;
            for (int i = 0; i < validKeys.Count; i++)
            {
                sequence = validKeys.GetFromKey(i).ToString();
                Assert.True(BaseConverter.IsValidSequence(sequence));
            }
        }

        [Fact(DisplayName = "Sequence with mixture of dummy/valid characters should be invalid")]
        public void SequenceWithMixOfValidAndDummyCharactersShouldBeValid()
        {
            var dummyKeys = Settings.DummyCharacterSet;
            var validKeys = Settings.ValidCharacterSet;

            // Combine dummy and valie keys to make the input sequence.
            StringBuilder sb = new StringBuilder();
            sb.Append(dummyKeys[0]);
            sb.Append(validKeys.GetFromKey(0));
            string sequence = sb.ToString();

            Assert.False(BaseConverter.IsValidSequence(sequence));
        }
    }
}
