using System;
using System.Linq;
using System.Text;

namespace Cryptography.Obfuscation.Modules
{
    public static class BaseConverter
    {
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

        public static int ConvertFromBase(string encryptedString)
        {
            // Return -1 as default value if encrypted string is not valid.
            if(!IsValidString(encryptedString))
                return -1;
            
            int result = 0, n = encryptedString.Length - 1;
            for(int i = 0; i < encryptedString.Length; i++, n--)
            {
                int charValue = Settings.ValidCharacterSet.GetFromValue(encryptedString[i]);
                int baseValue = (int)Math.Pow(Settings.Base, n);
                result += (charValue * baseValue);
            }

            return result;
        }

        public static bool IsValidString(string encryptedString)
        {
            bool isValid = !string.IsNullOrEmpty(encryptedString) && encryptedString.All(x => Settings.ValidCharacterSet.ContainsValue(x));
            return isValid;
        }
    }
}
