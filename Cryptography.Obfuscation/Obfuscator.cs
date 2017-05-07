using System;
using Cryptography.Obfuscation.Modules;

namespace Cryptography.Obfuscation
{
    public class Obfuscator
    {
        public ObfuscationStrategy Strategy { get; set; }

        private int seed;
        public int Seed {
            get
            {
                return seed;
            }
            set
            {
                // Ensure seed value has to be greater than 1,
                // Otherwise x XOR seed will always be equal to x or x + 1.
                if(value <= 1)
                {
                    throw new InvalidOperationException("Seed value has to be greater than 0.");
                }

                seed = value;
            }
        }
       
        public Obfuscator()
        {
            // Set defaults:
            this.Strategy = ObfuscationStrategy.Constant;
            this.Seed = 312;
        }

        public string Obfuscate(int value)
        {
            var baseValue =  BaseConverter.ConvertToBase(value);
            return Settings.AddDummyCharacters(baseValue, Strategy, Seed);
        }

        public int Deobfuscate(string value)
        {
            var valueWithoutDummyCharacters = Settings.RemoveDummyCharacters(value);
            return BaseConverter.ConvertFromBase(valueWithoutDummyCharacters);
        }
    }
}
