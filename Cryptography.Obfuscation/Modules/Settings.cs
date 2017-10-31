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
        
        static Settings()
        {
            ValidCharacterSet = new UniqueDictionary<int, char>();

            int index = 0;
            for (int i = 0; i < AllCharacterSet.Length; i++)
            {
                // Ignore dummy characters.
                if (DummyCharacterSet.Contains(AllCharacterSet[i]))
                    continue;

                ValidCharacterSet.Add(index, AllCharacterSet[i]);
                index++;
            }

            Base = ValidCharacterSet.Count;
        }
    }
}