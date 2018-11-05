using System.Collections.Generic;
using System.Linq;

namespace WordChain.Commands
{
    public class WordDictionaryHash
    {
        private readonly Dictionary<int, HashSet<string>> _dictHash;

        public WordDictionaryHash()
        {
            _dictHash = new Dictionary<int, HashSet<string>>();
        }

        public WordDictionaryHash(IEnumerable<string> words) : this()
        {
            foreach (string word in words)
                Add(word);
        }

        public HashSet<string> this[int key] => _dictHash.ContainsKey(key) ? _dictHash[key] : new HashSet<string>();

        public void Add(string newWord)
        {
            if (!_dictHash.ContainsKey(newWord.Length))
                _dictHash[newWord.Length] = new HashSet<string>();
            _dictHash[newWord.Length].Add(newWord);
        }

        public bool Has(string search)
        {
            return _dictHash.ContainsKey(search.Length) ? _dictHash[search.Length].Contains(search) : false;
        }
    }
}
