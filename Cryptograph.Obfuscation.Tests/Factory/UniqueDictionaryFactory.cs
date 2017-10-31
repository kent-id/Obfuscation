using Cryptography.Obfuscation.DataStructure;

namespace Cryptography.Obfuscation.Tests.Factory
{
    public static class UniqueDictionaryFactory
    {
        public static UniqueDictionary<int, char> NewInstance
        {
            get
            {
                return new UniqueDictionary<int, char>();
            }
        }
    }
}
