using System;
using System.Linq;
using System.Text;
using Cryptography.Obfuscation.DataStructure;

namespace Cryptography.Obfuscation.Modules
{
    public static class Settings
    {
        public static readonly UniqueDictionary<int, char> ValidCharacterSet;
        public static readonly int Base;

        public readonly static int MinimumLength = 8;
        public readonly static char[] AllCharacterSet =
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9'
        };

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

        public static string AddDummyCharacters(string key, ObfuscationStrategy strategy, int seed) {
            if (string.IsNullOrEmpty(key))
                return key;

            var sb = new StringBuilder();
            sb.Append(key);

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

        public static string RemoveDummyCharacters(string key)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < key.Length; i++)
            {
                bool isDummy = DummyCharacterSet.Contains(key[i]);
                if (!isDummy)
                    sb.Append(key[i]);
            }

            return sb.ToString();
        }
    }
}
