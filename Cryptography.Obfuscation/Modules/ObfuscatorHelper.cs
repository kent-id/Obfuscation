using Cryptography.Obfuscation.Extensions;
using System;
using System.Linq;
using System.Text;

namespace Cryptography.Obfuscation.Modules
{
    /// <summary>
    ///     Contains Helper functions for obfuscating objects.
    /// </summary>
    public static class ObfuscatorHelper
    {
        private static Random random = new Random((int) (DateTime.UtcNow.Ticks % Int32.MaxValue));

        /// <summary>
        ///     Given a sequence, add dummy characters based on the strategy and seed data passed.
        /// </summary>
        /// <param name="sequence">
        ///     The base sequence without dummy characters.
        /// </param>
        /// <param name="strategy">
        ///     The strategy to be used.
        /// </param>
        /// <param name="seed">
        ///     The seed value to be used.
        /// </param>
        /// <returns>
        ///     Character sequence with dummy characters merged into it, based on the strategy used.
        /// </returns>
        public static string AddDummyCharacters(string sequence, ObfuscationStrategy strategy, int seed)
        {
            if (string.IsNullOrEmpty(sequence))
                return sequence;

            var sb = new StringBuilder();
            sb.Append(sequence);

            int dummyCharacterIndex = 0, insertionIndex = 0;
            while (sb.Length < Settings.MinimumLength)
            {
                int hash = Math.Abs(sb.ToString().GetStableHashCode());
                
                if (strategy == ObfuscationStrategy.Constant)
                {
                    // Compute insertionIndex in a constant way (using hash).
                    // While allowing the possibility of first and last index.
                    // Note that insertionIndex can exceed sb.Length by 1 (i.e. insert after last character).
                    insertionIndex = (hash ^ seed) % sb.Length;
                    if ((hash % seed % 3) > (sb.Length / 2)) insertionIndex++;

                    // Compute 'randomized' dummy character index constantly (using hash).
                    // Note that insertionIndex cannot exceed dummyCharacterSet.Length.
                    dummyCharacterIndex = (hash ^ seed) % Settings.DummyCharacterSet.Length;
                } else
                {
                    // Randomize insertion index.
                    // Note that Min-Max boundary has inclusive min, exclusive max
                    insertionIndex = random.Next(0, sb.Length + 1);     // insertion can be at last character (i.e. sb.Length)
                    dummyCharacterIndex = random.Next(0, Settings.DummyCharacterSet.Length);
                }

                sb.Insert(insertionIndex, Settings.DummyCharacterSet[dummyCharacterIndex]);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Remove dummy characters from the specified sequence.
        /// </summary>
        /// <param name="sequence">
        ///     The character sequence which has dummy characters to be removed.
        /// </param>
        /// <returns>
        ///     The base sequence without dummy characters.
        /// </returns>
        public static string RemoveDummyCharacters(string sequence)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < sequence.Length; i++)
            {
                bool isDummy = Settings.DummyCharacterSet.Contains(sequence[i]);
                if (!isDummy)
                    sb.Append(sequence[i]);
            }

            return sb.ToString();
        }
    }
}
