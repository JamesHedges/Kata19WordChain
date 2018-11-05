using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace WordChain.Commands
{
    public class WordChainCommand : IRequest<WordChainResponse>
    {
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class WordChainResponse
    {
        public List<string> WordChain { get; set; }
    }

    public class WordChainCommandHandler : IRequestHandler<WordChainCommand, WordChainResponse>
    {
        readonly IWordChainConfiguration _configuration;

        public WordChainCommandHandler(IWordChainConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<WordChainResponse> Handle(WordChainCommand request, CancellationToken cancellationToken)
        {
            string path = $@"{_configuration.FilePath}\{_configuration.WordList}";
            var words = await LoadWords(path);
            WordChainProcessor processor = new WordChainProcessor();
            var chain = processor.ProcessChain(request.Start, request.End, words);
            return new WordChainResponse() { WordChain = chain };
        }

        private static async Task<WordDictionaryHash> LoadWords(string path)
        {
            using (var stream = new StreamReader(path))
            {
                WordDictionaryHash words = new WordDictionaryHash();
                string line;
                while ((line = await stream.ReadLineAsync()) != null)
                {
                    words.Add(line);
                }
                return words;
            }
        }
    }
}
