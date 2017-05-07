using System;
using System.Linq;
using System.Text;

namespace Cryptography.Obfuscation.Modules
{
    /// <summary>
    ///     Provides conversion capabilities to custom base specified in Settings.
    /// </summary>
    public static class BaseConverter
    {
        /// <summary>
        ///     Convert specified number to its character sequence representation specified in Settings.
        /// </summary>
        /// <param name="value">
        ///     The number to convert.
        /// </param>
        /// <returns>
        ///     The character sequence which represents the specified number.
        /// </returns>
        public static string ConvertToBase(int value)
        {
            var sb = new StringBuilder();
            
            do
            {
                // Get the character at key: value % base.
                sb.Insert(0, Settings.ValidCharacterSet.GetFromKey(value % Settings.Base));

                // Keep dividing value by base in each iteration, until value reaches 0.
                value /= Settings.Base;
            } while (value > 0);

            return sb.ToString();
        }

        /// <summary>
        ///     Convert specified character sequence bak to number.
        /// </summary>
        /// <param name="sequence">
        ///     The character sequence which represents a number, as specified in Settings.
        /// </param>
        /// <returns>
        ///     The number represented by the character sequence if valid, -1 otherwise.
        /// </returns>
        public static int ConvertFromBase(string sequence)
        {
            // Return -1 as default value if encrypted string is not valid.
            if(!IsValidSequence(sequence))
                return -1;
            
            int result = 0, n = sequence.Length - 1;
            for(int i = 0; i < sequence.Length; i++, n--)
            {
                int charValue = Settings.ValidCharacterSet.GetFromValue(sequence[i]);
                int baseValue = (int)Math.Pow(Settings.Base, n);
                result += (charValue * baseValue);
            }

            return result;
        }

        /// <summary>
        ///     Checks whether a character sequence is valid or not.
        /// </summary>
        /// <param name="sequence">
        ///     The character sequence to validate.
        /// </param>
        /// <returns>
        ///     True if valid, false otherwise.
        /// </returns>
        public static bool IsValidSequence(string sequence)
        {
            bool isValid = !string.IsNullOrEmpty(sequence) && sequence.All(x => Settings.ValidCharacterSet.ContainsValue(x));
            return isValid;
        }
    }
}
