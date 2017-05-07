using System.Collections.Generic;
using System.Linq;

namespace Cryptography.Obfuscation.DataStructure
{
    public class UniqueDictionary<TKey, TValue>
    {
        public Dictionary<TKey, TValue> Dictionary { get; set; }
        public Dictionary<TValue, TKey> InverseDictionary { get; set; }

        public UniqueDictionary()
        {
            Dictionary = new Dictionary<TKey, TValue>();
            InverseDictionary = new Dictionary<TValue, TKey>();
        }
        
        public TValue GetFromKey(TKey key)
        {
            return Dictionary[key];
        }

        public TKey GetFromValue(TValue value)
        {
            return InverseDictionary[value];
        }

        public int Count
        {
            get
            {
                return Dictionary.Count;
            }
        }
        
        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);
            InverseDictionary.Add(value, key);
        }
        
        public bool Remove(TKey key)
        {
            foreach (var inverseKey in InverseDictionary.Where(kvp => kvp.Value.Equals(key)).ToList())
            {
                InverseDictionary.Remove(inverseKey.Key);
            }
            return Dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        public bool ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }
        public bool ContainsValue(TValue value)
        {
            return InverseDictionary.ContainsKey(value);
        }
    }
}
