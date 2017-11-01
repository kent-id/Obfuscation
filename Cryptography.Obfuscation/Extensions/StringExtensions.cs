namespace Cryptography.Obfuscation.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Get stable hash code across all .NET versions.
        ///     GetHashCode() doesn't guarantee stable values across .NET versions.
        /// </summary>
        /// <seealso cref="https://stackoverflow.com/questions/53086/can-i-depend-on-the-values-of-gethashcode-to-be-consistent" />
        public static int GetStableHashCode(this string str)
        {
            unchecked
            {
                int hash1 = 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length && str[i] != '\0'; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1 || str[i + 1] == '\0')
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }
    }
}
