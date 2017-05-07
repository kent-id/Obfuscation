using System;
using System.Linq;
using System.Text;
using Cryptography.Obfuscation.DataStructure;

namespace Cryptography.Obfuscation.Modules
{

    /// <summary>
    ///     Provides global settings for Obfuscation.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        ///     Read-only character dictionary,
        ///     Generated from AllCharacterSet - DummyCharacterSet.
        /// </summary>
        public static readonly UniqueDictionary<int, char> ValidCharacterSet;

        /// <summary>
        ///     Read-only character count dictionary,
        ///     Which denotes the base number to use in number to character conversion.
        /// </summary>
        public static readonly int Base;

        /// <summary>
        ///     Read-only minimum length of the generated sequence.
        /// </summary>
        public readonly static int MinimumLength = 8;

        /// <summary>
        ///     Read-only character set that represents all the characters which can be used for conversion.
        /// </summary>
        public readonly static char[] AllCharacterSet =
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9'
        };

        /// <summary>
        ///     Read-only character set that represents dummy values.
        /// </summary>
        public readonly static char[] DummyCharacterSet =
        {
            'A','D','M','N','Q','V','Z','c','d','g','j','k','n','q','r','s','u','x','z','2','4','7'
        };


        private static Random random = new Random();

        static Settings()
        {
            ValidCharacterSet = new UniqueDictionary<int, char>();

            int index = 0;
            for(int i = 0; i < AllCharacterSet.Length; i++)
            {
                // Ignore dummy characters.
                if (DummyCharacterSet.Contains(AllCharacterSet[i]))
                    continue;

                ValidCharacterSet.Add(index, AllCharacterSet[i]);
                index++;
            }

            Base = ValidCharacterSet.Count;
        }

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
        public static string AddDummyCharacters(string sequence, ObfuscationStrategy strategy, int seed) {
            if (string.IsNullOrEmpty(sequence))
                return sequence;

            var sb = new StringBuilder();
            sb.Append(sequence);

            int dummyCharacterIndex = 0;
            while (sb.Length < MinimumLength)
            {
                int hash = Math.Abs(sb.ToString().GetHashCode());

                // Compute insertionIndex in a constant way (using hash).
                // While allowing the possibility of first and last index.
                // Note that insertionIndex can exceed sb.Length by 1 (i.e. insert after last character).
                int insertionIndex = (hash ^ seed) % sb.Length;
                if (hash % seed % 3 == 0) insertionIndex++;

                // Compute 'randomized' dummy character index constantly (using hash).
                // Note that insertionIndex cannot exceed dummyCharacterSet.Length.
                dummyCharacterIndex = (hash ^ seed) % DummyCharacterSet.Length;

                if(strategy == ObfuscationStrategy.Randomize)
                {
                    // Randomize
                    insertionIndex = random.Next(0, insertionIndex);
                    dummyCharacterIndex = random.Next(0, dummyCharacterIndex);
                }

                sb.Insert(insertionIndex, DummyCharacterSet[dummyCharacterIndex]);
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
            for(int i = 0; i < sequence.Length; i++)
            {
                bool isDummy = DummyCharacterSet.Contains(sequence[i]);
                if (!isDummy)
                    sb.Append(sequence[i]);
            }

            return sb.ToString();
        }
    }
}