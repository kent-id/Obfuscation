using Cryptography.Obfuscation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptography.ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var obfuscator = new Obfuscator();
            obfuscator.Strategy = ObfuscationStrategy.Constant;
            var obfuscator2 = new Obfuscator();
            obfuscator2.Strategy = ObfuscationStrategy.Randomize;

            for (int i = 0; i <= 1000; i++)
            {
                Console.WriteLine($"{i}\n---------------");
                Console.WriteLine($"Constant Mode: {obfuscator.Obfuscate(i)}, {obfuscator.Obfuscate(i)}");
                Console.WriteLine($"Randomized Mode: {obfuscator2.Obfuscate(i)}, {obfuscator2.Obfuscate(i)}");
                Console.WriteLine();
            }

            // Prevent console from closing immediately.
            Console.ReadLine();
        }
        
    }
}
