using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordChain.Commands
{
    public class WordTest
    {
        private readonly Regex _regex;
        private readonly string _word;

        public WordTest(string word)
        {
            _word = word;
            string pattern = CreatePattern(word);
            _regex = new Regex(pattern);
        }

        public HashSet<string> DoTest(HashSet<string> set)
        {
            return set.Where(w => w != _word).Where(w => _regex.IsMatch(w)).ToHashSet<string>();
        }

        private string CreatePattern(string word)
        {
            List<string> patterns = new List<string>();
            for (int i = 0; i < word.Length; i++)
            {
                string newPattern = string.Empty;
                for (int nn = 0; nn < word.Length; nn++)
                    newPattern = newPattern + (nn == i ? ".*?" : $"[{word[nn].ToString()}]");
                patterns.Add(newPattern);
            }
            return string.Join("|", patterns);
        }
    }
}
