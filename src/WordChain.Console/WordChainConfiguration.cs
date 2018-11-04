using System.Security;
using Microsoft.Extensions.Configuration;

namespace WordChain
{
    public interface IWordChainConfiguration
    {
        string KataDataBaseAddress { get; }
        string WordList { get; }
        string FilePath { get; }
    }

    public class WordChainConfiguration : IWordChainConfiguration
    {
        private readonly IConfiguration _configuration;

        public WordChainConfiguration()
        {
            _configuration = LoadConfiguration();
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("applicationsettings.json", false, true);
            return builder.Build();
        }

        public string KataDataBaseAddress => _configuration?.GetValue<string>("appSettings:kataDataUrl");
        public string FilePath => _configuration?.GetValue<string>("appSettings:filePath");
        public string WordList => _configuration?.GetValue<string>("appSettings:words");
    }
}
