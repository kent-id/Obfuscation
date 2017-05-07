namespace Cryptography.Obfuscation
{
    /// <summary>
    ///     The obfuscation strategy to use.
    /// </summary>
    public enum ObfuscationStrategy
    {
        /// <summary>
        ///     With the same Seed value,
        ///     Constant mode will always generate the same obfuscated string for a particular number.
        /// </summary>
        Constant,

        /// <summary>
        ///     Regardless of Seed value,
        ///     Randomized mode will generate randomized obfuscated string for a particular number,
        ///     All of which will still map the same number.
        /// </summary>
        Randomize
    }
}