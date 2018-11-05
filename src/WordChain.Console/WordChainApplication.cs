using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using McMaster.Extensions.CommandLineUtils;
using KataDataLoader;
using WordChain.Commands;

namespace WordChain
{
    public class WordChainApplication
    {
        private ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IWordChainConfiguration _configuration;

        public WordChainApplication(ILogger logger, IMediator mediator, IWordChainConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
        }

        [Argument(0, "Command To Execute", "Command")]
        public string Command { get; set; }

        [Option("-S|--Start")]
        public string StartWord { get; set; }

        [Option("-E|--EndWord")]
        public string EndWord { get; set; }


        public async Task OnExecute()
        {
            switch (Command.ToLower())
            {
                case "loaddata":
                    var response = await _mediator.Send(new LoadTestDataCommand
                    {
                        BaseUrl = _configuration.KataDataBaseAddress,
                        FilePath = _configuration.FilePath,
                        FileName = _configuration.WordList
                    });
                    _logger.LogInformation("File Downloaded");
                    break;
                case "wordchain":
                    await _mediator.Send(new WordChainCommand { Start = StartWord, End = EndWord });
                    break;
                default:
                    _logger.LogError($"Invalid Command: {Command}");
                    break;
            }

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
