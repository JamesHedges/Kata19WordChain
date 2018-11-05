using System;
using System.Collections.Generic;
using System.Linq;

namespace WordChain.Commands
{
    public class WordChainProcessor
    {
        public List<string> ProcessChain(string start, string end, WordDictionaryHash words)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(start);
            var result = FindTarget(start, end, queue, words);
            return result.Item1 ? result.Item2.ToList() : new List<string>();
        }

        private Tuple<bool, Queue<string>> FindTarget(string seed, string target, Queue<string> queue, WordDictionaryHash words)
        {
            HashSet<string> candidates = GetCandidates(seed, words);

            if (!candidates.Any())
            {
                return Tuple.Create(false, queue);
            }

            if (candidates.Contains(target))
            {
                queue.Enqueue(target);
                return Tuple.Create(true, queue);
            }

            for (int i = 0; i < target.Length; i++)
            {
                var test = seed.ToCharArray();
        
                test[i] = target[i];
                if (candidates.Contains(test.ToString()))
                {
                    candidates.Remove(test.ToString());
                    queue.Enqueue(test.ToString());
                    var result = FindTarget(test.ToString(), target, queue, words);
                    if (result.Item1)
                    {
                        return result;
                    }
                    queue.Dequeue();
                }
                foreach (var candidate in candidates)
                {
                    queue.Enqueue(candidate);
                    var result = FindTarget(test.ToString(), target, queue, words);
                    if (result.Item1)
                    {
                        return result;
                    }
                    queue.Dequeue();
                }
            }
            return Tuple.Create(false, queue);
        }

        private HashSet<string> GetCandidates(string seed, WordDictionaryHash words)
        {
            WordTest wordTest = new WordTest(seed);
            return wordTest.DoTest(words[seed.Length]);
            return new HashSet<string>();
        }
    }
}
