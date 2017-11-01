using System;
using Cryptography.Obfuscation.Modules;

namespace Cryptography.Obfuscation
{
    /// <summary>
    ///     Provides obfuscation functionalties.
    /// </summary>
    public class Obfuscator
    {
        /// <summary>
        ///     The strategy to use when obfuscating a number.
        /// </summary>
        public ObfuscationStrategy Strategy { get; set; }

        private int seed;

        /// <summary>
        ///     Seed value is used to randomize the generated sequence.
        ///     Minimum value for Seed is 2, setting this to a higher 'unstructured' number is preferable.
        ///     Default seed value is 312.
        /// </summary>
        public int Seed {
            get
            {
                return seed;
            }
            set
            {
                // Ensure seed value has to be greater than 1,
                // Otherwise x XOR seed will always be equal to x or x + 1.
                if (value <= 1)
                {
                    throw new InvalidOperationException("Seed value has to be at least two.");
                }

                seed = value;
            }
        }
       
        /// <summary>
        ///     Initialize object with default strategy and seed value.
        /// </summary>
        public Obfuscator()
        {
            // Set defaults:
            this.Strategy = ObfuscationStrategy.Constant;
            this.Seed = 113;
        }

        /// <summary>
        ///     Obfuscate the specified number. 
        /// </summary>
        /// <param name="value">
        ///     Non-negative integer to obfuscate.
        /// </param>
        /// <returns>
        ///     The obfuscated string of the specified number.
        /// </returns>
        public string Obfuscate(int value)
        {
            if (value < 0)
                throw new InvalidOperationException("Negative values are not supported.");

            var baseValue =  BaseConverter.ConvertToBase(value);
            return ObfuscatorHelper.AddDummyCharacters(baseValue, Strategy, Seed);
        }

        /// <summary>
        ///     Deobfuscate an obfuscated string back to number.
        /// </summary>
        /// <param name="value">
        ///     The obfuscated string to deobfuscate.
        /// </param>
        /// <returns>
        ///     The number represented by the obfuscated string specified.
        /// </returns>
        public int Deobfuscate(string value)
        {
            var valueWithoutDummyCharacters = ObfuscatorHelper.RemoveDummyCharacters(value);
            return BaseConverter.ConvertFromBase(valueWithoutDummyCharacters);
        }
    }
}
